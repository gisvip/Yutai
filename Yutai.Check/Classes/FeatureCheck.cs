using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class FeatureCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        public FeatureCheck(IDataCheck dataCheck)
        {
            _dataCheck = dataCheck;
        }

        public List<FeatureItem> Check()
        {
            List<FeatureItem> list = new List<FeatureItem>();

            foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
            {
                if (_dataCheck.CheckPipelineList.Contains(pipelineLayer.Code))
                {
                    IBasicLayerInfo pointLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                    IBasicLayerInfo lineLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (pointLayerInfo == null || lineLayerInfo == null || pointLayerInfo.FeatureClass == null || lineLayerInfo.FeatureClass == null)
                        continue;
                    list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, lineLayerInfo.FeatureClass, pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.TZW)));
                }
            }

            return list;
        }

        public List<FeatureItem> Check(string pipelineName, IFeatureClass pointFeatureClass, IFeatureClass lineFeatureClass, string tzwFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            int idxTzw = pointFeatureClass.FindField(tzwFieldName);
            if (idxTzw < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = pointFeatureClass.AliasName,
                    CheckItem = "特征点子类型检查",
                    ErrDesc = "字段配置错误 字段 " + tzwFieldName + " 不存在",
                });
                return list;
            }

            IFeatureCursor featureCursor = pointFeatureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                string tzw = CommonHelper.ConvertToString(feature.Value[idxTzw]);
                if (string.IsNullOrWhiteSpace(tzw))
                    continue;
                int count = GetLinkCount(feature.Shape as IPoint, lineFeatureClass);
                if (_dataCheck.DataCheckConfig.StraightPnt.Contains(tzw))
                {
                    if (count != 2)
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = pointFeatureClass.AliasName,
                            CheckItem = "特征点子类型检查",
                            ErrDesc = "与 " + count + " 条管线相连",
                        });
                    }
                }
                else if (_dataCheck.DataCheckConfig.ThreeConnect.Contains(tzw))
                {
                    if (count != 3)
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = pointFeatureClass.AliasName,
                            CheckItem = "特征点子类型检查",
                            ErrDesc = "与 " + count + " 条管线相连",
                        });
                    }
                }
                else if (_dataCheck.DataCheckConfig.FourConnect.Contains(tzw))
                {
                    if (count != 4)
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = pointFeatureClass.AliasName,
                            CheckItem = "特征点子类型检查",
                            ErrDesc = "与 " + count + " 条管线相连",
                        });
                    }
                }
                else if (_dataCheck.DataCheckConfig.MultiConnect.Contains(tzw))
                {
                    if (count < 5)
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = pointFeatureClass.AliasName,
                            CheckItem = "特征点子类型检查",
                            ErrDesc = "与 " + count + " 条管线相连",
                        });
                    }
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return list;
        }

        public int GetLinkCount(IPoint point, IFeatureClass featureClass)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IDataStatistics dataStatistics = new DataStatistics()
            {
                Field = featureClass.OIDFieldName,
                Cursor = featureCursor as ICursor
            };
            int count = dataStatistics.Statistics.Count;
            Marshal.ReleaseComObject(featureCursor);
            return count;
        }
    }
}

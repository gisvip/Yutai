using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Check.Enums;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class ElevationCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        public ElevationCheck(IDataCheck dataCheck)
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
                    //list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, PipeConfigWordHelper.PointWords.DMZP));
                    //list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, PipeConfigWordHelper.PointWords.FSWZP));
                }
            }

            return list;
        }

        public List<FeatureItem> Check(string pipelineName, IFeatureClass featureClass, string fieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            int idx = featureClass.FindField(fieldName);
            if (idx < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "高程检查",
                    ErrDesc = "字段配置错误 字段 " + fieldName + " 不存在",
                });
                return list;
            }
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                IPoint point = feature.Shape as IPoint;
                if (point == null || point.IsEmpty)
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "高程检查",
                        ErrDesc = "位置不存在",
                    });
                    continue;
                }
                double z;
                if (_dataCheck.DataCheckConfig.ElevationCheckType == EnumElevationCheckType.UseAttribute)
                {
                    object value = feature.Value[idx];
                    if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "高程检查",
                            ErrDesc = "属性为空",
                        });
                        continue;
                    }
                    if (double.TryParse(value.ToString(), out z) == false)
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "高程检查",
                            ErrDesc = "属性填写错误",
                        });
                        continue;
                    }
                }
                else
                {
                    z = point.Z;
                }

                if (double.IsNaN(z))
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "高程检查",
                        ErrDesc = "高程为空",
                    });
                    continue;
                }
                if (IsUnusual(featureClass, point, z, feature.OID, idx))
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "高程检查",
                        ErrDesc = "高程异常",
                    });
                }
            }

            return list;
        }

        public bool IsUnusual(IFeatureClass featureClass, IPoint point, double z, int oid, int idx)
        {
            ITopologicalOperator topologicalOperator = point as ITopologicalOperator;
            if (topologicalOperator == null)
                return true;
            IPolygon polygon = topologicalOperator.Buffer(_dataCheck.DataCheckConfig.CompareRadius) as IPolygon;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = polygon;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            bool isUnusual = false;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (feature.OID == oid)
                    continue;
                double elevation;
                if (_dataCheck.DataCheckConfig.ElevationCheckType == EnumElevationCheckType.UseAttribute)
                {
                    object value = feature.Value[idx];
                    if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        continue;
                    }

                    if (double.TryParse(value.ToString(), out elevation) == false)
                    {
                        continue;
                    }
                }
                else
                {
                    IPoint tempPoint = feature.Shape as IPoint;
                    if (tempPoint == null || tempPoint.IsEmpty)
                    {
                        continue;
                    }
                    elevation = tempPoint.Z;
                }
                if (double.IsNaN(elevation))
                {
                    continue;
                }
                if (Math.Abs(elevation - z) <= _dataCheck.DataCheckConfig.CompareLimit)
                    continue;
                else
                {
                    isUnusual = true;
                    break;
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return isUnusual;
        }
    }
}

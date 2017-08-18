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

namespace Yutai.Check.Classes
{
    class RelationCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        public RelationCheck(IDataCheck dataCheck)
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
                    list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, lineLayerInfo.FeatureClass, PipeConfigWordHelper.PointWords.GDBH, PipeConfigWordHelper.LineWords.QDBH, PipeConfigWordHelper.LineWords.ZDBH));
                }
            }

            return list;
        }

        public List<FeatureItem> Check(string pipelineName, IFeatureClass pointFeatureClass, IFeatureClass lineFeatureClass,
            string gdbhFieldName, string qdbhFieldName, string zdbhFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            int idxGdbh = pointFeatureClass.FindField(gdbhFieldName);
            if (idxGdbh < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = pointFeatureClass.AliasName,
                    CheckItem = "点线关联检查",
                    ErrDesc = "字段配置错误 字段 " + gdbhFieldName + " 不存在",
                });
                return list;
            }
            int idxQdbh = lineFeatureClass.FindField(qdbhFieldName);
            int idxZdbh = lineFeatureClass.FindField(zdbhFieldName);
            if (idxQdbh < 0 || idxZdbh < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = lineFeatureClass.AliasName,
                    CheckItem = "坐标信息检查",
                    ErrDesc = "字段配置错误 字段 " + (idxQdbh < 0 ? qdbhFieldName + " " : "") + (idxZdbh < 0 ? zdbhFieldName + " " : "") + "不存在",
                });
                return list;
            }

            IFeatureCursor lineFeatureCursor = lineFeatureClass.Search(null, false);
            IFeature lineFeature;
            while ((lineFeature = lineFeatureCursor.NextFeature()) != null)
            {
                IPolyline polyline = lineFeature.Shape as IPolyline;
                if (polyline == null || polyline.IsEmpty)
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "点线关联检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }

                object objQdbh = lineFeature.Value[idxQdbh];
                object objZdbh = lineFeature.Value[idxZdbh];
                if (objQdbh == null || objQdbh is DBNull || objZdbh == null || objZdbh is DBNull)
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "坐标信息检查",
                        ErrDesc = "字段属性错误 字段 " + (objQdbh == null || objQdbh is DBNull ? qdbhFieldName + " " : "") + (objZdbh == null || objZdbh is DBNull ? zdbhFieldName + " " : "") + "属性为空",
                    });
                    return list;
                }
                bool hasQd = HasRelatePoint(pointFeatureClass, polyline.FromPoint, objQdbh.ToString(), idxGdbh);
                bool hasZd = HasRelatePoint(pointFeatureClass, polyline.ToPoint, objZdbh.ToString(), idxGdbh);
                if (hasQd == false || hasZd == false)
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "坐标信息检查",
                        ErrDesc = "线段 " + (hasQd ? "" : "起点属性 ") + (hasZd ? "" : "终点属性 ") + "与实际点号不一致",
                    });
                    return list;
                }
            }
            return list;
        }

        public bool HasRelatePoint(IFeatureClass featureClass, IPoint point, string value, int idxGdbh)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.OutputSpatialReference[featureClass.ShapeFieldName] =
                _dataCheck.AppContext.FocusMap.SpatialReference;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            bool has = false;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                object objGdbh = feature.Value[idxGdbh];
                if (objGdbh == null || objGdbh is DBNull)
                    continue;
                if (objGdbh.ToString() == value)
                    has = true;
                break;
            }
            Marshal.ReleaseComObject(featureCursor);
            return has;
        }
    }
}

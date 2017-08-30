using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class CoordCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        public CoordCheck(IDataCheck dataCheck)
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
                    List<IBasicLayerInfo> layerInfos = pipelineLayer.GetLayers(enumPipelineDataType.Point);
                    foreach (IBasicLayerInfo basicLayerInfo in layerInfos)
                    {
                        if (basicLayerInfo.FeatureClass == null)
                            continue;
                        list.AddRange(Check(basicLayerInfo.FeatureClass, pipelineLayer.Name, basicLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.XZB), basicLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.YZB), basicLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.DMGC)));
                    }

                }
            }

            return list;
        }

        public List<FeatureItem> Check(IFeatureClass featureClass, string pipelineName, string xFieldName, string yFieldName, string zFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            int xIdx = featureClass.FindField(xFieldName);
            int yIdx = featureClass.FindField(yFieldName);
            int zIdx = featureClass.FindField(zFieldName);
            if (xIdx < 0 || yIdx < 0 || zIdx < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "坐标信息检查",
                    ErrDesc = "字段配置错误 字段 " + (xIdx < 0 ? xFieldName + " " : "") + (yIdx < 0 ? yFieldName + " " : "") + (zIdx < 0 ? zFieldName + " " : "") + "不存在",
                });
                return list;
            }
            bool hasZ = FeatureClassUtil.CheckHasZ(featureClass);
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
                        CheckItem = "坐标信息检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }

                object xValue = feature.Value[xIdx];
                object yValue = feature.Value[yIdx];
                object zValue = feature.Value[zIdx];
                double x = 0, y = 0, z = 0;
                bool isX = true, isY = true, isZ = true;
                if (xValue == null || xValue is DBNull || !double.TryParse(xValue.ToString(), out x))
                {
                    isX = false;
                }
                if (yValue == null || yValue is DBNull || !double.TryParse(yValue.ToString(), out y))
                {
                    isY = false;
                }
                if (zValue == null || zValue is DBNull || !double.TryParse(zValue.ToString(), out z))
                {
                    isZ = false;
                }
                if (!isX || !isY || !isZ)
                {

                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "坐标信息检查",
                        ErrDesc = "字段 " + (isX ? "" : xFieldName + " ") + (isY ? "" : yFieldName + " ") + (isZ ? "" : zFieldName + " ") + "属性错误或为空",
                    });
                    continue;
                }
                IPoint virtualPoint = new PointClass();
                virtualPoint.PutCoords(x, y);
                string errDesc = "";
                if (GeometryHelper.GetDistance(point, virtualPoint) > _dataCheck.DataCheckConfig.SurfaceTolerance)
                    errDesc += "平面误差超过容差值 ";
                if (hasZ)
                {
                    if (Math.Abs(point.Z - z) > _dataCheck.DataCheckConfig.ElevationTolerance)
                        errDesc += "高程误差超过容差值";
                }
                list.Add(new FeatureItem(feature)
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "坐标信息检查",
                    ErrDesc = errDesc,
                });
            }

            return list;
        }
    }
}

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
    class MinimumSpacingCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        public MinimumSpacingCheck(IDataCheck dataCheck)
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
                    IBasicLayerInfo pointBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                    if (pointBasicLayerInfo?.FeatureClass == null)
                        continue;
                    list.AddRange(CheckPoint(pipelineLayer.Name, pointBasicLayerInfo.FeatureClass));
                    IBasicLayerInfo lineBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (lineBasicLayerInfo?.FeatureClass == null)
                        continue;
                    list.AddRange(CheckLine(pipelineLayer.Name, lineBasicLayerInfo.FeatureClass));
                }
            }

            return list;
        }

        public List<FeatureItem> CheckPoint(string pipelineName, IFeatureClass featureClass)
        {
            List<FeatureItem> list = new List<FeatureItem>();

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
                        CheckItem = "最小间距检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }
                foreach (IPipelineLayer checkPipelineLayer in _dataCheck.PipelineLayers)
                {
                    if (_dataCheck.CheckPipelineList.Contains(checkPipelineLayer.Code))
                    {
                        IBasicLayerInfo pointBasicLayerInfo = checkPipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                        if (pointBasicLayerInfo?.FeatureClass == null)
                            continue;
                        string msg = CheckPoint(checkPipelineLayer.Name, point, pointBasicLayerInfo.FeatureClass);
                        if (string.IsNullOrWhiteSpace(msg))
                            continue;
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "最小间距检查",
                            ErrDesc = msg
                        });
                    }
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return list;
        }

        public List<FeatureItem> CheckLine(string pipelineName, IFeatureClass featureClass)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                IPolyline polyline = feature.Shape as IPolyline;
                if (polyline == null || polyline.IsEmpty)
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "最小间距检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }
                foreach (IPipelineLayer checkPipelineLayer in _dataCheck.PipelineLayers)
                {
                    if (_dataCheck.CheckPipelineList.Contains(checkPipelineLayer.Code))
                    {
                        IBasicLayerInfo lineBasicLayerInfo = checkPipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                        if (lineBasicLayerInfo?.FeatureClass == null)
                            continue;
                        string msg = CheckLine(checkPipelineLayer.Name, polyline, lineBasicLayerInfo.FeatureClass);
                        if (string.IsNullOrWhiteSpace(msg))
                            continue;
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "最小间距检查",
                            ErrDesc = msg
                        });
                    }
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return list;
        }

        public string CheckPoint(string pipelineName, IPoint point, IFeatureClass featureClass)
        {
            string message = "";
            ITopologicalOperator topologicalOperator = point as ITopologicalOperator;
            IPolygon polygon = topologicalOperator.Buffer(_dataCheck.DataCheckConfig.PointMinimumSpacing) as IPolygon;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = polygon;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                IPoint tempPoint = feature.Shape as IPoint;
                if (tempPoint == null || tempPoint.IsEmpty)
                {
                    continue;
                }
                double d = GeometryHelper.GetDistance(point, tempPoint);
                if (d < _dataCheck.DataCheckConfig.PointMinimumSpacing)
                {
                    message += $"{pipelineName}:与 {feature.OID} 的距离为 {d}；";
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return message;
        }
        public string CheckLine(string pipelineName, IPolyline polyline, IFeatureClass featureClass)
        {
            string message = "";
            ITopologicalOperator topologicalOperator = polyline as ITopologicalOperator;
            IPolygon polygon = topologicalOperator.Buffer(_dataCheck.DataCheckConfig.LineMinimumSpacing) as IPolygon;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = polygon;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                IPolyline tempPolyline = feature.Shape as IPolyline;
                if (tempPolyline == null || tempPolyline.IsEmpty)
                {
                    continue;
                }
                IProximityOperator proximityOperator = polyline as IProximityOperator;
                double d = proximityOperator.ReturnDistance(tempPolyline);
                if (d < _dataCheck.DataCheckConfig.LineMinimumSpacing)
                {
                    message += $"{pipelineName}:与 {feature.OID} 的距离为 {d}；";
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return message;
        }
    }
}

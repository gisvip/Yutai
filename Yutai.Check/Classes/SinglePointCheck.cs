using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class SinglePointCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public SinglePointCheck(IDataCheck dataCheck)
        {
            _dataCheck = dataCheck;
        }

        public BackgroundWorker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public List<FeatureItem> Check()
        {
            List<FeatureItem> list = new List<FeatureItem>();

            foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                if (_dataCheck.CheckPipelineList.Contains(pipelineLayer.Code))
                {
                    IBasicLayerInfo pointLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                    IBasicLayerInfo lineLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (pointLayerInfo == null || lineLayerInfo == null || pointLayerInfo.FeatureClass == null || lineLayerInfo.FeatureClass == null)
                        continue;
                    list.AddRange(Check(pointLayerInfo.FeatureClass, lineLayerInfo.FeatureClass, pipelineLayer.Name));
                }
            }

            return list;
        }

        private List<FeatureItem> Check(IFeatureClass pointFeatureClass, IFeatureClass lineFeatureClass, string pipelineName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IFeatureCursor lineFeatureCursor = lineFeatureClass.Search(null, false);
            IFeature lineFeature;

            while ((lineFeature = lineFeatureCursor.NextFeature()) != null)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                if (lineFeature.Shape.IsEmpty)
                    continue;
                IPolyline polyline = lineFeature.Shape as IPolyline;
                if (polyline == null)
                    continue;
                bool hasFromPoint = HasRelationPoint(polyline.FromPoint, pointFeatureClass);
                bool hasToPoint = HasRelationPoint(polyline.ToPoint, pointFeatureClass);
                string errDesc = "";
                if (hasFromPoint == false && hasToPoint == false)
                {
                    errDesc = "该管线为孤立线";
                }
                else if (hasFromPoint == false)
                {
                    errDesc = "该管线缺少起点";
                }
                else if (hasToPoint == false)
                {
                    errDesc = "该管线缺少终点";
                }
                if (string.IsNullOrWhiteSpace(errDesc))
                    continue;
                list.Add(new FeatureItem(lineFeature)
                {
                    PipelineName = pipelineName,
                    PipeLayerName = pointFeatureClass.AliasName,
                    CheckItem = "孤立点检查",
                    ErrDesc = errDesc,
                });
            }
            Marshal.ReleaseComObject(lineFeatureCursor);
            return list;
        }

        private bool HasRelationPoint(IPoint point, IFeatureClass poingFeatureClass)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = poingFeatureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.OutputSpatialReference[poingFeatureClass.ShapeFieldName] =
                _dataCheck.AppContext.FocusMap.SpatialReference;
            IFeatureCursor featureCursor = poingFeatureClass.Search(spatialFilter, false);
            return featureCursor.NextFeature() != null;
        }
    }
}

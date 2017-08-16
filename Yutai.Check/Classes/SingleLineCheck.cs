using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class SingleLineCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        public SingleLineCheck(IDataCheck dataCheck)
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
                    list.AddRange(Check(pointLayerInfo.FeatureClass, lineLayerInfo.FeatureClass, pipelineLayer.Name));
                }
            }

            return list;
        }

        private List<FeatureItem> Check(IFeatureClass pointFeatureClass, IFeatureClass lineFeatureClass, string pipelineName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IFeatureCursor pointFeatureCursor = pointFeatureClass.Search(null, false);
            IFeature pointFeature;

            while ((pointFeature = pointFeatureCursor.NextFeature()) != null)
            {
                if (pointFeature.Shape.IsEmpty)
                    continue;
                if (HasRelationLine(pointFeature.Shape as IPoint, lineFeatureClass))
                    continue;
                list.Add(new FeatureItem(pointFeature)
                {
                    PipelineName = pipelineName,
                    PipeLayerName = pointFeatureClass.AliasName,
                    CheckItem = "孤立点检查",
                    ErrDesc = "该管点为孤立点",
                });
            }
            Marshal.ReleaseComObject(pointFeatureCursor);
            return list;
        }

        private bool HasRelationLine(IPoint point, IFeatureClass lineFeatureClass)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = lineFeatureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.OutputSpatialReference[lineFeatureClass.ShapeFieldName] =
                _dataCheck.AppContext.FocusMap.SpatialReference;
            IFeatureCursor featureCursor = lineFeatureClass.Search(spatialFilter, false);
            return featureCursor.NextFeature() != null;
        }
    }
}

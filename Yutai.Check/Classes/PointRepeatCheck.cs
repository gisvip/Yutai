using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class PointRepeatCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;

        public PointRepeatCheck(IDataCheck dataCheck)
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
                        list.AddRange(Check(basicLayerInfo.FeatureClass, pipelineLayer.Name));
                    }

                }
            }

            return list;
        }

        public List<FeatureItem> Check(IFeatureClass featureClass, string pipelineName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            List<int> allOids = new List<int>();
            IDictionary<int, List<int>> repeatDictionary = new Dictionary<int, List<int>>();
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                allOids.Add(feature.OID);
            }
            Marshal.ReleaseComObject(featureCursor);
            featureCursor = null;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.OutputSpatialReference[featureClass.ShapeFieldName] =
                _dataCheck.AppContext.FocusMap.SpatialReference;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            for (int i = 0; i < allOids.Count; i++)
            {
                feature = featureClass.GetFeature(allOids[i]);
                if (feature.Shape.IsEmpty)
                    continue;
                spatialFilter.Geometry = feature.Shape;
                featureCursor = featureClass.Search(spatialFilter, false);

                while ((feature = featureCursor.NextFeature()) != null)
                {
                    if (allOids[i] == feature.OID)
                        continue;
                    allOids.Remove(feature.OID);
                    if (repeatDictionary.ContainsKey(allOids[i]) == false)
                        repeatDictionary.Add(allOids[i], new List<int>());
                    repeatDictionary[allOids[i]].Add(feature.OID);
                }
                Marshal.ReleaseComObject(featureCursor);
            }

            foreach (KeyValuePair<int, List<int>> keyValuePair in repeatDictionary)
            {
                feature = featureClass.GetFeature(keyValuePair.Key);
                list.Add(new FeatureItem(feature)
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "重复点检查",
                    ErrDesc = $"管点 {keyValuePair.Key} 和 {string.Join(",", keyValuePair.Value)} 重复",
                });
            }

            return list;
        }
    }
}

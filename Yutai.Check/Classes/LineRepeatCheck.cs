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
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class LineRepeatCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public LineRepeatCheck(IDataCheck dataCheck)
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
                    List<IBasicLayerInfo> layerInfos = pipelineLayer.GetLayers(enumPipelineDataType.Line);
                    foreach (IBasicLayerInfo basicLayerInfo in layerInfos)
                    {
                        if (_worker != null && _worker.CancellationPending)
                            return list;
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
                if (_worker != null && _worker.CancellationPending)
                    return list;
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
                if (_worker != null && _worker.CancellationPending)
                    return list;
                feature = featureClass.GetFeature(allOids[i]);
                if (feature.Shape.IsEmpty)
                    continue;
                IPolyline polyline = feature.Shape as IPolyline;
                spatialFilter.Geometry = polyline;
                featureCursor = featureClass.Search(spatialFilter, false);
                IFeature tempFeature;
                while ((tempFeature = featureCursor.NextFeature()) != null)
                {
                    if (allOids[i] == tempFeature.OID)
                        continue;
                    IPolyline tempPolyline = tempFeature.Shape as IPolyline;
                    if (GeometryHelper.IsSame(polyline, tempPolyline))
                    {
                        allOids.Remove(tempFeature.OID);
                        if (repeatDictionary.ContainsKey(allOids[i]) == false)
                            repeatDictionary.Add(allOids[i], new List<int>());
                        repeatDictionary[allOids[i]].Add(tempFeature.OID);
                    }
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
                    CheckItem = "重复线检查",
                    ErrDesc = $"管线 {keyValuePair.Key} 和 {string.Join(",", keyValuePair.Value)} 重复",
                });
            }

            return list;
        }
    }
}

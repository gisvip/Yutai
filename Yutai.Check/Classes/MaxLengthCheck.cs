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
    class MaxLengthCheck:IAttributeCheck
    {
        private IDataCheck _dataCheck;
        public MaxLengthCheck(IDataCheck dataCheck)
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
                    IBasicLayerInfo lineBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (lineBasicLayerInfo?.FeatureClass == null)
                        continue;
                    list.AddRange(Check(pipelineLayer.Name, lineBasicLayerInfo.FeatureClass));
                }
            }

            return list;
        }

        public List<FeatureItem> Check(string pipelineName, IFeatureClass featureClass)
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
                        CheckItem = "最大长度检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }
                if (polyline.Length > _dataCheck.DataCheckConfig.MaxLength)
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "最大长度检查",
                        ErrDesc = $"长度为 {polyline.Length}，超过允许最大长度"
                    });
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return list;
        }
    }
}

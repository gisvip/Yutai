using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class HylinkCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        public HylinkCheck(IDataCheck dataCheck)
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
                    list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, PipeConfigWordHelper.PointWords.DMZP));
                    list.AddRange(Check(pipelineLayer.Name, pointLayerInfo.FeatureClass, PipeConfigWordHelper.PointWords.FSWZP));
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
                    CheckItem = "超链接检查",
                    ErrDesc = "字段配置错误 字段 " + fieldName + " 不存在",
                });
                return list;
            }
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                object value = feature.Value[idx];
                if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "超链接检查",
                        ErrDesc = "属性为空",
                    });
                    continue;
                }
                if (File.Exists(value.ToString()))
                    continue;
                list.Add(new FeatureItem(feature)
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "超链接检查",
                    ErrDesc = "文件不存在",
                });
            }

            return list;
        }
    }
}

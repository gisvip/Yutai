using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Check.Classes
{
    class FieldRepeatCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        public FieldRepeatCheck(IDataCheck dataCheck)
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
                    foreach (IBasicLayerInfo basicLayerInfo in pipelineLayer.Layers)
                    {
                        if (basicLayerInfo.FeatureClass == null)
                            continue;
                        switch (basicLayerInfo.DataType)
                        {
                            case enumPipelineDataType.Point:
                                list.AddRange(Check(basicLayerInfo, pipelineLayer.Name, PipeConfigWordHelper.PointWords.GDBH));
                                break;
                            case enumPipelineDataType.Line:
                                break;
                            case enumPipelineDataType.Network:
                                break;
                            case enumPipelineDataType.Junction:
                                break;
                            case enumPipelineDataType.Point3D:
                                break;
                            case enumPipelineDataType.Line3D:
                                break;
                            case enumPipelineDataType.AssPoint:
                                break;
                            case enumPipelineDataType.AssLine:
                                break;
                            case enumPipelineDataType.AnnoPoint:
                                break;
                            case enumPipelineDataType.AnnoLine:
                                break;
                            case enumPipelineDataType.Annotation:
                                break;
                            case enumPipelineDataType.Other:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            return list;
        }

        private List<FeatureItem> Check(IBasicLayerInfo basicLayerInfo, string pipelineName, string keyName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            int keyIndex = basicLayerInfo.FeatureClass.FindField(keyName);
            if (keyIndex < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = basicLayerInfo.FeatureClass.AliasName,
                    CheckItem = "字段重复值检查",
                    ErrDesc = $"字段唯一值字段配置错误，字段 {keyName} 不存在",
                });
                return list;
            }
            IDictionary<string, IFeature> uniqueDictionary = new Dictionary<string, IFeature>();
            IDictionary<string, int> repeatDictionary = new Dictionary<string, int>();
            IFeatureCursor featureCursor = basicLayerInfo.FeatureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                object curValue = feature.Value[keyIndex];

                if (curValue == null || curValue is DBNull || string.IsNullOrWhiteSpace(curValue.ToString()))
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = basicLayerInfo.FeatureClass.AliasName,
                        CheckItem = "字段重复值检查",
                        ErrDesc = "字段 " + keyName + " 为空值",
                    });
                }
                else
                {
                    if (uniqueDictionary.ContainsKey(curValue.ToString()))
                        if (repeatDictionary.ContainsKey(curValue.ToString()))
                            repeatDictionary[curValue.ToString()]++;
                        else
                            repeatDictionary.Add(curValue.ToString(), 2);
                    else
                        uniqueDictionary.Add(curValue.ToString(), feature);
                }
            }
            foreach (KeyValuePair<string, int> keyValuePair in repeatDictionary)
            {
                list.Add(new FeatureItem(uniqueDictionary[keyValuePair.Key])
                {
                    PipelineName = pipelineName,
                    PipeLayerName = basicLayerInfo.FeatureClass.AliasName,
                    CheckItem = "字段重复值检查",
                    ErrDesc = "字段 " + keyName + " 的值：" + keyValuePair.Key + " 有 " + keyValuePair.Value + " 处重复",
                });
            }
            return list;
        }
    }
}

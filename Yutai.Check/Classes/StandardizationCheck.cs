using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class StandardizationCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        public StandardizationCheck(IDataCheck dataCheck)
        {
            _dataCheck = dataCheck;
        }

        public List<FeatureItem> Check()
        {
            List<FeatureItem> list = new List<FeatureItem>();

            foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
            {
                if (_dataCheck.CheckPipelineList.Contains(pipelineLayer.Code))
                    foreach (IBasicLayerInfo basicLayerInfo in pipelineLayer.Layers)
                    {
                        if (basicLayerInfo.FeatureClass == null)
                            continue;
                        if (basicLayerInfo.DataType != enumPipelineDataType.Point && basicLayerInfo.DataType != enumPipelineDataType.Line)
                            continue;
                        list.AddRange(Check(pipelineLayer.Name, basicLayerInfo.FeatureClass));
                    }
            }

            return list;
        }

        private List<FeatureItem> Check(string pipelineName, IFeatureClass featureClass)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IDictionary<int, DomainItem> fieldDictionary = new Dictionary<int, DomainItem>();
            int fieldCount = featureClass.Fields.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                IField field = featureClass.Fields.Field[i];
                DomainItem item = _dataCheck.DataCheckConfig.DomainItems.FirstOrDefault(c => c.FieldName == field.Name);
                if (item == null)
                    continue;
                fieldDictionary.Add(i, item);
            }
            if (fieldDictionary.Count <= 0)
                return list;
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                foreach (KeyValuePair<int, DomainItem> keyValuePair in fieldDictionary)
                {
                    string value = CommonHelper.ConvertToString(feature.Value[keyValuePair.Key]);
                    if (keyValuePair.Value.ValueList.Contains(value))
                        continue;
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "字段标准化检查",
                        ErrDesc = "字段 " + keyValuePair.Value.FieldName + " 的值 " + value + " 填写有误"
                    });
                }
            }
            return list;
        }
    }
}

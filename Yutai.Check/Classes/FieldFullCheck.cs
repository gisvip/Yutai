using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Check.Enums;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Classes
{
    class FieldFullCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public FieldFullCheck(IDataCheck dataCheck)
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
                    foreach (IBasicLayerInfo basicLayerInfo in pipelineLayer.Layers)
                    {
                        if (basicLayerInfo.FeatureClass == null)
                            continue;
                        list.AddRange(Check(basicLayerInfo, pipelineLayer.Name));
                    }
            }

            return list;
        }

        private List<FeatureItem> Check(IBasicLayerInfo basicLayerInfo, string pipelineName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IDictionary<int, IYTField> fieldDictionary = new Dictionary<int, IYTField>();
            foreach (IYTField ytField in basicLayerInfo.Fields)
            {
                if (ytField.AllowNull)
                    continue;
                IFields fields = basicLayerInfo.FeatureClass.Fields;
                if (string.IsNullOrWhiteSpace(ytField.Name))
                    continue;
                int idx = fields.FindField(ytField.Name);
                if (idx < 0)
                    continue;
                fieldDictionary.Add(idx, ytField);
            }

            IFeatureCursor featureCursor = basicLayerInfo.FeatureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                foreach (KeyValuePair<int, IYTField> keyValuePair in fieldDictionary)
                {
                    object curValue = feature.Value[keyValuePair.Key];
                    if (curValue == null || curValue is DBNull || string.IsNullOrWhiteSpace(curValue.ToString()))
                    {
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = basicLayerInfo.FeatureClass.AliasName,
                            CheckItem = "字段完整性检查",
                            ErrDesc = "字段 " + keyValuePair.Value.Name + " 为空值",
                        });
                    }
                }
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class DepthCheck : IAttributeCheck
    {
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public DepthCheck(IDataCheck dataCheck)
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
                    IBasicLayerInfo lineLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (lineLayerInfo != null && lineLayerInfo.FeatureClass != null)
                        list.AddRange(Check(pipelineLayer.Name, lineLayerInfo.FeatureClass, lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS), lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS)));
                }
            }

            return list;
        }

        private List<FeatureItem> Check(string pipelineName, IFeatureClass featureClass, string qdmsFieldName, string zdmsFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            int idxQdms = featureClass.FindField(qdmsFieldName);
            int idxZdms = featureClass.FindField(zdmsFieldName);

            if (idxQdms < 0 || idxZdms < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = featureClass.AliasName,
                    CheckItem = "地下埋深取值检查",
                    ErrDesc = "字段配置错误",
                });
                return list;
            }

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                double qdms = CommonHelper.ConvertToDouble(feature.Value[idxQdms]);
                double zdms = CommonHelper.ConvertToDouble(feature.Value[idxZdms]);
                if (double.IsNaN(qdms))
                {
                    list.Add(new FeatureItem()
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "地下埋深取值检查",
                        ErrDesc = "起点埋深属性填写异常",
                    });
                }
                else
                {
                    if (qdms < _dataCheck.DataCheckConfig.DepthMin || qdms > _dataCheck.DataCheckConfig.DepthMax)
                    {
                        list.Add(new FeatureItem()
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "地下埋深取值检查",
                            ErrDesc = "起点埋深值超过范围",
                        });
                    }
                }
                if (double.IsNaN(zdms))
                {
                    list.Add(new FeatureItem()
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "地下埋深取值检查",
                        ErrDesc = "终点埋深属性填写异常",
                    });
                }
                else
                {
                    if (zdms < _dataCheck.DataCheckConfig.DepthMin || zdms > _dataCheck.DataCheckConfig.DepthMax)
                    {
                        list.Add(new FeatureItem()
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "地下埋深取值检查",
                            ErrDesc = "终点埋深值超过范围",
                        });
                    }
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return list;
        }
    }
}

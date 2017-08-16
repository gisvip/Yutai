using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Check.Enums;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Classes
{
    public class DataCheckBase : IDataCheck
    {
        private readonly IAppContext _appContext;
        private readonly IPipelineConfig _pipelineConfig;
        private List<string> _checkPipelineList;

        public DataCheckBase(IAppContext context, IPipelineConfig pipelineConfig)
        {
            _appContext = context;
            _pipelineConfig = pipelineConfig;
            _pipelineConfig.OrganizeMap(_appContext.FocusMap);
        }

        public IAppContext AppContext => _appContext;

        public IPipelineConfig PipelineConfig => _pipelineConfig;

        public List<FeatureItem> Check(List<EnumCheckItem> items)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            IAttributeCheck attributeCheck;
            IGeometryCheck geometryCheck;
            foreach (EnumCheckItem checkItem in items)
            {
                switch (checkItem)
                {
                    case EnumCheckItem.P_FieldFull:
                        OnProgressChanged("正在进行 字段完整性检查");
                        attributeCheck = new FieldFullCheck(this);
                        list.AddRange(attributeCheck.Check());
                        break;
                    case EnumCheckItem.P_FieldRepeat:
                        OnProgressChanged("正在进行 字段重复值检查");
                        attributeCheck = new FieldRepeatCheck(this);
                        list.AddRange(attributeCheck.Check());
                        break;
                    case EnumCheckItem.G_SinglePoint:
                        OnProgressChanged("正在进行 孤立点检查");
                        geometryCheck = new SinglePointCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_SingleLine:
                        OnProgressChanged("正在进行 孤立线检查");
                        geometryCheck = new SingleLineCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return list;
        }

        public List<IPipelineLayer> PipelineLayers => _pipelineConfig.Layers;

        public List<string> CheckPipelineList
        {
            get { return _checkPipelineList; }
            set { _checkPipelineList = value; }
        }

        public event EventHandler<string> ProgressChanged;

        protected virtual void OnProgressChanged(string e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}

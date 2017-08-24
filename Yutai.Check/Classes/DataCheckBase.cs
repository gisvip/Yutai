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
        private IDataCheckConfig _dataCheckConfig;
        private List<string> _checkPipelineList;

        public DataCheckBase(IAppContext context, IPipelineConfig pipelineConfig, IDataCheckConfig dataCheckConfig)
        {
            _appContext = context;
            _pipelineConfig = pipelineConfig;
            _dataCheckConfig = dataCheckConfig;
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
                    case EnumCheckItem.P_Hylink:
                        OnProgressChanged("正在进行 超链接检查");
                        attributeCheck = new HylinkCheck(this);
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
                    case EnumCheckItem.G_PointRepeat:
                        OnProgressChanged("正在进行 重复点检查");
                        geometryCheck = new PointRepeatCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_LineRepeat:
                        OnProgressChanged("正在进行 重复线检查");
                        geometryCheck = new LineRepeatCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Coord:
                        OnProgressChanged("正在进行 坐标信息检查");
                        geometryCheck = new CoordCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Relation:
                        OnProgressChanged("正在进行 点线关联检查");
                        geometryCheck = new RelationCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Feature:
                        OnProgressChanged("正在进行 特征点类型检查");
                        geometryCheck = new FeatureCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Elevation:
                        OnProgressChanged("正在进行 高程检查");
                        geometryCheck = new ElevationCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Flow:
                        OnProgressChanged("正在进行 流向检查");
                        geometryCheck = new FlowCheck(this);
                        list.AddRange(geometryCheck.Check());
                        break;
                    case EnumCheckItem.G_Intersect:
                        OnProgressChanged("正在进行 管线交叉检查");
                        geometryCheck = new IntersectCheck(this);
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

        public IDataCheckConfig DataCheckConfig
        {
            get { return _dataCheckConfig; }
        }

        public event EventHandler<string> ProgressChanged;

        protected virtual void OnProgressChanged(string e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}

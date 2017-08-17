using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Check.Classes
{
    public class FeatureItem
    {
        private int _oid;
        private IFeature _mainFeature;
        private List<FeatureItem> _subFeatureItems;
        private string _remarks;
        private string _pipelineName;
        private string _pipeLayerName;
        private string _checkItem;
        private string _errDesc;
        private IFeatureLayer _featureLayer;

        public FeatureItem(IFeature feature)
        {
            _oid = feature.OID;
            _mainFeature = feature;
            _subFeatureItems = new List<FeatureItem>();
        }
        public FeatureItem()
        {
        }
        /// <summary>
        /// 图层组名称
        /// </summary>
        public string PipelineName
        {
            get { return _pipelineName; }
            set { _pipelineName = value; }
        }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string PipeLayerName
        {
            get { return _pipeLayerName; }
            set { _pipeLayerName = value; }
        }
        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckItem
        {
            get { return _checkItem; }
            set { _checkItem = value; }
        }

        public int OID
        {
            get { return _oid; }
            set { _oid = value; }
        }

        public IFeature MainFeature
        {
            get { return _mainFeature; }
            set { _mainFeature = value; }
        }

        public List<FeatureItem> SubFeatureItems
        {
            get { return _subFeatureItems; }
            set { _subFeatureItems = value; }
        }
        
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set { _remarks = value; }
        }

        public string ErrDesc
        {
            get { return _errDesc; }
            set { _errDesc = value; }
        }
    }
}

﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Api.Enums;
using Yutai.Plugins.Identifer.Enums;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Identifer.Views
{
    internal class LayerFilterProperties
    {
        private string layerFilterName;
        //private Image                   headerImage;
        private string layerCategory;
        private LayerFeatureType layerFeatureType;
        private int layerFilterIndex;
        private ILayer targetLayer;
        //private MapWindow               mapWindow;
        //////////////////////////////////////////////////////

        #region < 上述私有变量的属性 >

        /// <summary>
        /// 获取或设置过滤器对应的图层对象
        /// </summary>
        public ILayer TargetLayer
        {
            get { return targetLayer; }
            set { targetLayer = value; }
        }

        /// <summary>
        /// 获取或设置图层在下拉列表中次序
        /// </summary>
        public int LayerFilterIndex
        {
            get { return layerFilterIndex; }
            set { layerFilterIndex = value; }
        }

        /// <summary>
        /// 获取或设置对应的图层显示名称
        /// </summary>
        public string LayerFilterName
        {
            get { return layerFilterName; }
            set { layerFilterName = value; }
        }

        /// <summary>
        /// 获取或设置对应的图层的显示图标
        /// </summary>
        //public Image HeaderImage
        //{
        //    get { return headerImage; }
        //    set { headerImage = value; }
        //}
        /// <summary>
        /// 获取或设置对应的图层的分级结构
        /// </summary>
        public string LayerCategory
        {
            get { return layerCategory; }
            set { layerCategory = value; }
        }

        /// <summary>
        /// 获取或设置对应的图层的要素类型
        /// </summary>
        public LayerFeatureType LayerFeatureType
        {
            get { return layerFeatureType; }
            set { layerFeatureType = value; }
        }

        /// <summary>
        /// 设置查询所在的地图窗口
        /// </summary>
        //public MapWindow MapWindow 
        //{
        //    set { mapWindow = value; }
        //}

        #endregion
    }

    public interface IIdentifierView : IMenuProvider
    {
        IdentifierMode Mode { get; }
        bool ZoomToShape { get; }
        void Clear();
        event Action ModeChanged;
        event Action ItemSelected;
        void UpdateView();

        void Identify(IEnvelope envelope);
        void ClearActiveViewEvents();

        void InitializeActiveViewEvents();
    }
}
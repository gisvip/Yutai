using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Setting
{
    class CmdReloadLayer:YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdReloadLayer(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _plugin.EditLayers = null;
            List<object> items = new List<object>();
            foreach (IPipelineLayer pipelineLayer in _plugin.PipeConfig.Layers)
            {
                items.Add(new LayerItem(pipelineLayer.Name, pipelineLayer));
            }
            _plugin.EditLayers = items;
            _context.View.Update();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "刷新图层";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_reload;
            base.m_name = "PipelineEditor_ReloadLayer";
            base._key = "PipelineEditor_ReloadLayer";
            base.m_toolTip = "刷新图层";
            base.m_message = "刷新图层";
            base._itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    return false;
                if (_plugin.PipeConfig.Layers.Count <= 0)
                    return false;
                return true;
            }
        }

    }
}

using System;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline3D.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class Cmd3DBuilder : YutaiCommand
    {
        private PipelineAnalysisPlugin _plugin;

        public Cmd3DBuilder(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        
        public override void OnClick(object sender, EventArgs args)
        {
            //frm3DBuilder frm = new frm3DBuilder(_context, _plugin.PipeConfig);
            //frm.Show();
            Frm3DBuilder builder = new Frm3DBuilder(_plugin.PipeConfig);
            builder.Show();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "三维生成";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_pipeline_3d;
            base.m_name = "PipeAnalysis_3DBuilder";
            base._key = "PipeAnalysis_3DBuilder";
            base.m_toolTip = "三维生成";
            base.m_checked = false;
            base.m_message = "三维生成";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

            CommonUtils.AppContext = _context;
        }
    }
}
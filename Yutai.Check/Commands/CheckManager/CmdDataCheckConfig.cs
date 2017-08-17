using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Check.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Commands.CheckManager
{
    class CmdDataCheckConfig : YutaiTool
    {
        private CheckPlugin _plugin;

        public CmdDataCheckConfig(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "数据检查设置";
            base.m_category = "Check_Pipeline";
            //base.m_bitmap = Properties.Resources.icon_pipe_cqsz;
            base.m_name = "Check_Pipeline_DataCheckConfig";
            base._key = "Check_Pipeline_DataCheckConfig";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_plugin.DataCheckConfig == null)
                _plugin.DataCheckConfig = new FrmDataCheckConfig();
            _plugin.DataCheckConfig.ShowDialog();
        }
    }
}

using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Data
{
    public class CmdExit : YutaiCommand
    {

        
        public CmdExit(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            _context.Close();

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "退出系统";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_exitsys;
            base.m_name = "File_ExitSys";
            base._key = "File_ExitSys";
            base.m_toolTip = "退出系统";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
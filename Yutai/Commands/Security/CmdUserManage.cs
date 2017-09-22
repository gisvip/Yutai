
using System;
using System.Windows.Forms;
using EnvDTE;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Commands.Security;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Security
{
	public class CmdUserManage : YutaiCommand
    {
        public CmdUserManage(IAppContext context)
        {
            OnCreate(context);
        }
      
        public override void OnCreate(object context)
        {
            _context = context as IAppContext;
            this.m_caption = "角色管理";
            base.m_category = "Security";
            base.m_bitmap = Properties.Resources.icon_user;
            base.m_name = "Security_UserManageCommand";
            base._key = "Security_UserManageCommand";
            base.m_toolTip = "用户管理";
            base.m_message = "对用户进行操作，包括增加、修改、删除等";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }


        public override void OnClick()
		{
			//if (AppConfigInfo.UserID == "admin")
			//{
			//	frmUserManage frmUserManage = new frmUserManage();
			//	frmUserManage.ShowDialog();
			//}
			//else
			//{
			//	frmLogin frmLogin = new frmLogin();
			//	if (frmLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			//	{
			//		frmUserManage frmUserManage = new frmUserManage();
			//		frmUserManage.ShowDialog();
			//	}
			//}
		}

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}

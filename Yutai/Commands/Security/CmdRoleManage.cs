using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Security
{
	public class CmdRoleManage : YutaiCommand
	{
		public CmdRoleManage(IAppContext context)
        {
            OnCreate(context);
        }
			
		

		public override void OnCreate(object context)
		{
		    _context = context as IAppContext;
            this.m_caption = "角色管理";
            base.m_category = "Security";
            base.m_bitmap = Properties.Resources.icon_role;
            base.m_name = "Security_RoleManageCommand";
            base._key = "Security_RoleManageCommand";
            base.m_toolTip = "角色管理";
		    base.m_message = "对角色进行操作，包括增加、修改、删除等";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

		public override void OnClick()
		{
			//if (_context.UserID == "admin")
			//{
			//	frmRoleManager frmRoleManager = new frmRoleManager();
			//	frmRoleManager.ShowDialog();
			//}
			//else
			//{
			//	frmLogin frmLogin = new frmLogin();
			//	if (frmLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			//	{
			//		frmRoleManager frmRoleManager = new frmRoleManager();
			//		frmRoleManager.ShowDialog();
			//	}
			//}
		}

	    public override void OnClick(object sender, EventArgs args)
	    {
	        OnClick();
	    }
	}
}

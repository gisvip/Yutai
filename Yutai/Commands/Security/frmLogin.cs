
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Commands.Security
{
	public partial class frmLogin : System.Windows.Forms.Form
	{
        
        private IList ilist_0;
        private IList ilist_1;
        private IList ilist_2;
        
		public frmLogin()
		{
			this.InitializeComponent();
		}

	private void frmLogin_Load(object sender, EventArgs e)
		{
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.txtUser.Text.Trim().Length == 0)
			{
				System.Windows.Forms.MessageBox.Show("请输入登录用户名!");
			}
			else
			{
				//ORGStaffHelper oRGStaffHelper = new ORGStaffHelper("ORGSTAFF");
				//if (oRGStaffHelper.ValidePassword(this.txtUser.Text.Trim(), this.txtPassword.Text))
				//{
				//	base.DialogResult = System.Windows.Forms.DialogResult.OK;
				//	base.Close();
				//}
				//else
				//{
				//	System.Windows.Forms.MessageBox.Show("密码错误!");
				//}
			}
		}
	}
}


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Security;

namespace Yutai.Commands.Security
{
    public partial class frmLogin : XtraForm
    {
        private IAppContext _context;
        private IList ilist_0;
        private IList ilist_1;
        private IList ilist_2;

        public frmLogin(IApplicationContainer container)
        {
            this.InitializeComponent();
            _context = container.GetSingleton<IAppContext>();
        }
        public frmLogin(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
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
                ORGStaffHelper oRGStaffHelper = new ORGStaffHelper("ORGSTAFF");
                if (oRGStaffHelper.ValidePassword(this.txtUser.Text.Trim(), this.txtPassword.Text))
                {
                    _context.UserID = this.txtUser.Text.Trim();
                    base.DialogResult = System.Windows.Forms.DialogResult.OK;
                    base.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("密码错误!");
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

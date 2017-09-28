using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    public partial class frmEditStaff : Form
    {
        private Staff _staff;

        internal Staff Staff
        {
            get
            {
                return this._staff;
            }
            set
            {
                this._staff = value;
            }
        }

        public frmEditStaff()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtOldPSW.Text.Length > 0)
            {
                if (this.txtOldPSW.Text != this._staff.Password)
                {
                    MessageBox.Show("原密码错误,无法更改密码!");
                    return;
                }
                else if (this.txtPassword.Text != this.txtValidePassword.Text)
                {
                    MessageBox.Show("前后两次密码输入不一致!");
                    return;
                }
            }
            if (this.txtChineseName.Text.Trim().Length != 0)
            {
                if (this._staff.RealName != this.txtChineseName.Text.Trim())
                {
                    this._staff.RealName = this.txtChineseName.Text.Trim();
                }
            }
            if (this.txtOldPSW.Text.Length > 0)
            {
                this._staff.Password = this.txtPassword.Text;
            }
            (new ORGStaffHelper()).Update(this._staff);
            base.DialogResult = System.Windows.Forms.DialogResult.OK;
            base.Close();
        }

        private void frmEditStaff_Load(object sender, EventArgs e)
        {
            this.txtChineseName.Text = this._staff.RealName;
            this.txtUserName.Text = this._staff.StaffID;
        }
    }
}

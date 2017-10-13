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
    public partial class frmAddStaff : Form
    {
        private ORGStaffHelper sh = new ORGStaffHelper();

        private int _oid;

        private string _id = "";

        private string _chinesename = "";

        private string _password = "";

        public string ChineseName
        {
            get
            {
                return this._chinesename;
            }
        }

        public string LoginID
        {
            get
            {
                return this._id;
            }
        }

        public int OID
        {
            get
            {
                return this._oid;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
        }

        public Staff Staff
        {
            get;
            set;
        }

        public frmAddStaff()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = this.txtUserName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请输入登录名!");
            }
            else if (this.txtPassword.Text != this.txtValidePassword.Text)
            {
                MessageBox.Show("前后两次密码输入不一致!");
            }
            else if (!this.sh.HasExist(str))
            {
                this._chinesename = this.txtChineseName.Text.Trim();
                if (this._chinesename.Length == 0)
                {
                    this._chinesename = this._id;
                }
                this._password = this.txtPassword.Text;
                this.Staff = new Staff()
                {
                    StaffID = str,
                    RealName = this._chinesename,
                    Password = this._password
                };
                this.sh.Add(this.Staff);
                base.DialogResult = System.Windows.Forms.DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageBox.Show("该登录名已存在!");
            }
        }
    }
}

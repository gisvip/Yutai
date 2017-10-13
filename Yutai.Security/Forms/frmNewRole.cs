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
    public partial class frmNewRole : Form
    {

        private ORGRoleHelper sh = new ORGRoleHelper();

        private int _oid;

        private string _name;

        private string _descript;

        public ORGRole Role
        {
            get;
            set;
        }

        public string RoleDescript
        {
            get
            {
                return this._descript;
            }
        }

        public string RoleName
        {
            get
            {
                return this._name;
            }
        }

        internal int RoleOID
        {
            get
            {
                return this._oid;
            }
        }

        public frmNewRole()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = this.txtName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请输入角色名!");
            }
            else if (!this.sh.HasExist(str))
            {
                this._name = str;
                this._descript = this.txtDes.Text.Trim();
                this.Role = new ORGRole()
                {
                    RoleName = this._name,
                    DESCRIPT = this._descript,
                    RoleID = Guid.NewGuid().ToString()
                };
                this.sh.Add(this.Role);
                base.DialogResult = System.Windows.Forms.DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageBox.Show("该角色已存在!");
            }
        }
    }
}

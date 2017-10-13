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
    public partial class frmAddRoles : Form
    {
        private object _staffID;

        private List<ORGRole> _roles = new List<ORGRole>();

        internal List<ORGRole> Roles
        {
            get
            {
                return this._roles;
            }
        }

        public frmAddRoles(object staffID)
        {
            this.InitializeComponent();
            this._staffID = staffID;
        }

        private void lvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.lvRoles.SelectedIndices.Count > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lvRoles.SelectedIndices.Count > 0)
            {
                ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
                for (int i = 0; i < this.lvRoles.SelectedIndices.Count; i++)
                {
                    ORGRole tag = this.lvRoles.SelectedItems[i].Tag as ORGRole;
                    this._roles.Add(tag);
                    oRGStaffRoleHelper.Add(tag.RoleID, this._staffID.ToString());
                }
            }
        }

        private void frmAddRoles_Load(object sender, EventArgs e)
        {
            string[] roleName = new string[2];
            List<string> roleIDs = (new ORGStaffRoleHelper()).GetRoleIDs(this._staffID.ToString());
            foreach (ORGRole oRGRole in (new ORGRoleHelper()).Load())
            {
                roleName[0] = oRGRole.RoleName;
                roleName[1] = oRGRole.DESCRIPT;
                if (roleIDs.IndexOf(oRGRole.RoleID) == -1)
                {
                    ListViewItem listViewItem = new ListViewItem(roleName)
                    {
                        Tag = oRGRole
                    };
                    this.lvRoles.Items.Add(listViewItem);
                }
            }
        }
    }
}

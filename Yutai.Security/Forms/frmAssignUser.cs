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
    public partial class frmAssignUser : Form
    {
        private string _RoleID;
        private List<Staff> _staffs = new List<Staff>();

        public string RoleID
        {
            set
            {
                this._RoleID = value;
            }
        }

        internal List<Staff> Staffs
        {
            get
            {
                return this._staffs;
            }
        }

        public frmAssignUser(string roleid)
        {
            this.InitializeComponent();
            this._RoleID = roleid;
        }

        private void lvStaffRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.lvStaffRole.SelectedIndices.Count > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
            for (int i = 0; i < this.lvStaffRole.SelectedIndices.Count; i++)
            {
                ListViewItem item = this.lvStaffRole.SelectedItems[i];
                Staff tag = item.Tag as Staff;
                tag.RealName = item.Text;
                tag.StaffID = item.SubItems[1].Text;
                this._staffs.Add(tag);
                oRGStaffRoleHelper.Add(this._RoleID, tag.StaffID);
            }
        }

        private void frmAssignUser_Load(object sender, EventArgs e)
        {
            string[] realName = new string[2];
            List<string> staffIDs = (new ORGStaffRoleHelper()).GetStaffIDs(this._RoleID);
            foreach (Staff staff in (new ORGStaffHelper()).Load())
            {
                if (staffIDs.IndexOf(staff.StaffID) == -1)
                {
                    realName[0] = staff.RealName;
                    realName[1] = staff.LoginName;
                    ListViewItem listViewItem = new ListViewItem(realName)
                    {
                        Tag = staff
                    };
                    this.lvStaffRole.Items.Add(listViewItem);
                }
            }
        }
    }
}

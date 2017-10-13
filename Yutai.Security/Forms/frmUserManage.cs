using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Priviliges;

namespace Yutai.Security.Forms
{
    public partial class frmUserManage : Form
    {
        private string[] PrivilegeDes = new string[] { "无权限", "浏览权限", "编辑权限" };

        private bool m_IsAll = false;

        public frmUserManage()
        {
            InitializeComponent();
        }

        private void lvLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteLayers.Enabled = false;
            this.btnEditGrant.Enabled = false;
            if (this.lvLayers.SelectedItems.Count > 0)
            {
                int count = this.lvLayers.SelectedItems.Count - 1;
                while (count >= 0)
                {
                    if (this.lvLayers.SelectedItems[count].Group != this.lvLayers.Groups[0])
                    {
                        count--;
                    }
                    else
                    {
                        this.btnDeleteLayers.Enabled = true;
                        this.btnEditGrant.Enabled = true;
                        break;
                    }
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                this.lvRoles.Items.Clear();
                this.lvFunctions.Items.Clear();
                this.lvLayers.Items.Clear();
                this.DeleteStaff((Staff)this.lvStaff.SelectedItems[0].Tag);
                this.lvStaff.Items.Remove(this.lvStaff.SelectedItems[0]);
            }
        }

        private void DeleteStaff(Staff staff)
        {
            (new ORGStaffHelper()).Delete(staff.StaffID);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddStaff _frmAddStaff = new frmAddStaff();
            if (_frmAddStaff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] realName = new string[] { _frmAddStaff.Staff.RealName, _frmAddStaff.Staff.StaffID };
                ListViewItem listViewItem = new ListViewItem(realName)
                {
                    Tag = _frmAddStaff.Staff
                };
                this.lvStaff.Items.Add(listViewItem);
            }
        }

        private void lvStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lvRoles.Items.Clear();
            this.lvLayers.Items.Clear();
            this.lvFunctions.Items.Clear();
            if (this.lvStaff.SelectedIndices.Count <= 0)
            {
                this.btnAddLayers.Enabled = false;
                this.btnAddMenu.Enabled = false;
                this.btnDeleteUser.Enabled = false;
                this.btnDeleteRole.Enabled = false;
                this.btnNewRole.Enabled = false;
                this.btnEditGrant.Enabled = false;
            }
            else
            {
                this.ReadStaffRoleInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSLayerInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSLayerInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSFunctionInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSFunctionInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.btnAddLayers.Enabled = true;
                this.btnAddMenu.Enabled = true;
                this.btnDeleteUser.Enabled = true;
                this.btnDeleteRole.Enabled = true;
                this.btnNewRole.Enabled = true;
                this.btnEditGrant.Enabled = this.lvLayers.SelectedIndices.Count > 0;
            }
        }

        private void ReadStaffRoleInfo(object userID)
        {
            string[] roleName = new string[2];
            ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
            ORGRoleHelper oRGRoleHelper = new ORGRoleHelper();
            foreach (string roleID in oRGStaffRoleHelper.GetRoleIDs(userID.ToString()))
            {
                ORGRole role = oRGRoleHelper.GetRole(roleID);
                if (role != null)
                {
                    roleName[0] = role.RoleName;
                    roleName[1] = role.DESCRIPT;
                    ListViewItem listViewItem = new ListViewItem(roleName)
                    {
                        Tag = role
                    };
                    this.lvRoles.Items.Add(listViewItem);
                }
            }
        }

        private void ReadSYSGRANTSLayerInfo(object userID)
        {
            ListViewItem listViewItem;
            int i;
            DataRow item;
            IRow row;
            string str;
            SysGrants sysGrant = new SysGrants();
            DataTable staffObjectPri = sysGrant.GetStaffObjectPri(userID.ToString(), "gisLayer");
            ITable table = CommonClass.OpenTable("LayerConfig");
            if (table == null)
                return;
            int num = table.FindField("Name");
            int num1 = table.FindField("FeatureClassName");
            string[] privilegeDes = new string[2];
            for (i = 0; i < staffObjectPri.Rows.Count; i++)
            {
                item = staffObjectPri.Rows[i];
                try
                {
                    row = table.GetRow(Convert.ToInt32(item["GRANTOBJECT"]));
                    str = row.Value[num1].ToString();
                    privilegeDes[0] = row.Value[num].ToString();
                    privilegeDes[1] = this.PrivilegeDes[Convert.ToInt32(item["privilegeFlag"])];
                    listViewItem = new ListViewItem(privilegeDes)
                    {
                        Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisLayer"),
                        Group = this.lvLayers.Groups[0]
                    };
                    this.lvLayers.Items.Add(listViewItem);
                }
                catch
                {
                }
            }
            table = CommonClass.OpenTable("DATASETTABLE");
            num = table.FindField("Name");
            num1 = table.FindField("RealName");
            staffObjectPri = sysGrant.GetStaffObjectPri(userID.ToString(), "gisDataset");
            for (i = 0; i < staffObjectPri.Rows.Count; i++)
            {
                item = staffObjectPri.Rows[i];
                try
                {
                    row = table.GetRow(Convert.ToInt32(item["GRANTOBJECT"]));
                    str = row.Value[num1].ToString();
                    privilegeDes[0] = row.Value[num].ToString();
                    privilegeDes[1] = this.PrivilegeDes[Convert.ToInt32(item["privilegeFlag"])];
                    listViewItem = new ListViewItem(privilegeDes)
                    {
                        Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisDataset"),
                        Group = this.lvLayers.Groups[0]
                    };
                    this.lvLayers.Items.Add(listViewItem);
                }
                catch
                {
                }
            }
        }

        private void ReadSYSGRANTSLayerInfoRromRole(object userID)
        {
            ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
            ORGRoleHelper oRGRoleHelper = new ORGRoleHelper();
            foreach (string roleID in oRGStaffRoleHelper.GetRoleIDs(userID.ToString()))
            {
                this.ReadLayerInfo(roleID);
            }
        }

        private void ReadLayerInfo(string RoleID)
        {
            ListViewItem listViewItem;
            int i;
            DataRow item;
            IRow row;
            string str;
            SysGrants sysGrant = new SysGrants();
            DataTable rolesObjectPri = sysGrant.GetRolesObjectPri(RoleID.ToString(), "gisLayer");
            ITable table = CommonClass.OpenTable("LayerConfig");
            if (table == null)
                return;
            int num = table.FindField("Name");
            int num1 = table.FindField("FeatureClassName");
            string[] privilegeDes = new string[2];
            for (i = 0; i < rolesObjectPri.Rows.Count; i++)
            {
                item = rolesObjectPri.Rows[i];
                try
                {
                    row = table.GetRow(Convert.ToInt32(item["GRANTOBJECT"]));
                    str = row.Value[num1].ToString();
                    privilegeDes[0] = row.Value[num].ToString();
                    privilegeDes[1] = this.PrivilegeDes[Convert.ToInt32(item["privilegeFlag"])];
                    listViewItem = new ListViewItem(privilegeDes)
                    {
                        Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisLayer"),
                        Group = this.lvLayers.Groups[1]
                    };
                    this.lvLayers.Items.Add(listViewItem);
                }
                catch
                {
                }
            }
            table = CommonClass.OpenTable("DATASETTABLE");
            num = table.FindField("Name");
            num1 = table.FindField("RealName");
            rolesObjectPri = sysGrant.GetRolesObjectPri(RoleID.ToString(), "gisDataset");
            for (i = 0; i < rolesObjectPri.Rows.Count; i++)
            {
                item = rolesObjectPri.Rows[i];
                try
                {
                    row = table.GetRow(Convert.ToInt32(item["GRANTOBJECT"]));
                    privilegeDes[0] = row.Value[num].ToString();
                    privilegeDes[1] = this.PrivilegeDes[Convert.ToInt32(item["privilegeFlag"])];
                    str = row.Value[num1].ToString();
                    listViewItem = new ListViewItem(privilegeDes)
                    {
                        Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisDataset"),
                        Group = this.lvLayers.Groups[1]
                    };
                    this.lvLayers.Items.Add(listViewItem);
                }
                catch
                {
                }
            }
        }

        private void ReadSYSGRANTSFunctionInfo(object RoleID)
        {
            MenuInfoHelper menuInfoHelper = new MenuInfoHelper();
            DataTable staffMenuPri = (new SysGrants()).GetStaffMenuPri(RoleID.ToString());
            for (int i = 0; i < staffMenuPri.Rows.Count; i++)
            {
                DataRow item = staffMenuPri.Rows[i];
                string str = Convert.ToString(item["GRANTOBJECT"]);
                MenuInfo menuInfo = menuInfoHelper.Load(str);
                if (menuInfo != null)
                {
                    string str1 = menuInfo.CAPTION.Trim();
                    if (str1.Length == 0)
                    {
                        str1 = menuInfo.NAME.Trim();
                    }
                    ListViewItem listViewItem = new ListViewItem(str1)
                    {
                        Tag = menuInfo,
                        Group = this.lvFunctions.Groups[0]
                    };
                    this.lvFunctions.Items.Add(listViewItem);
                }
            }
        }

        private void ReadSYSGRANTSFunctionInfoRromRole(object userID)
        {
            ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
            ORGRoleHelper oRGRoleHelper = new ORGRoleHelper();
            foreach (string roleID in oRGStaffRoleHelper.GetRoleIDs(userID.ToString()))
            {
                this.ReadFunctionInfo(roleID);
            }
        }

        private void ReadFunctionInfo(string RoleID)
        {
            MenuInfoHelper menuInfoHelper = new MenuInfoHelper();
            DataTable rolesMenuPri = (new SysGrants()).GetRolesMenuPri(RoleID.ToString());
            for (int i = 0; i < rolesMenuPri.Rows.Count; i++)
            {
                DataRow item = rolesMenuPri.Rows[i];
                string str = Convert.ToString(item["GRANTOBJECT"]);
                MenuInfo menuInfo = menuInfoHelper.Load(str);
                if (menuInfo != null)
                {
                    string str1 = menuInfo.CAPTION.Trim();
                    if (str1.Length == 0)
                    {
                        str1 = menuInfo.NAME.Trim();
                    }
                    ListViewItem listViewItem = new ListViewItem(str1)
                    {
                        Tag = menuInfo,
                        Group = this.lvFunctions.Groups[1]
                    };
                    this.lvFunctions.Items.Add(listViewItem);
                }
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            ORGStaffRoleHelper oRGStaffRoleHelper = new ORGStaffRoleHelper();
            for (int i = this.lvRoles.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = this.lvRoles.SelectedItems[i];
                oRGStaffRoleHelper.DeleteByRoleID((item.Tag as ORGRole).RoleID);
                this.lvRoles.Items.Remove(item);
            }
            this.lvLayers.Items.Clear();
            this.lvFunctions.Items.Clear();
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                this.ReadSYSGRANTSLayerInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSLayerInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSFunctionInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                this.ReadSYSGRANTSFunctionInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
            }
        }

        private void btnNewRole_Click(object sender, EventArgs e)
        {
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                frmAddRoles frmAddRole = new frmAddRoles((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                if (frmAddRole.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] roleName = new string[2];
                    for (int i = 0; i < frmAddRole.Roles.Count; i++)
                    {
                        ORGRole item = frmAddRole.Roles[i];
                        roleName[0] = item.RoleName;
                        roleName[1] = item.DESCRIPT;
                        ListViewItem listViewItem = new ListViewItem(roleName)
                        {
                            Tag = item
                        };
                        this.lvRoles.Items.Add(listViewItem);
                    }
                    this.lvLayers.Items.Clear();
                    this.lvFunctions.Items.Clear();
                    if (this.lvStaff.SelectedIndices.Count > 0)
                    {
                        this.ReadSYSGRANTSLayerInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                        this.ReadSYSGRANTSLayerInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                        this.ReadSYSGRANTSFunctionInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                        this.ReadSYSGRANTSFunctionInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                    }
                }
            }
        }

        private void lvFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteMenu.Enabled = false;
            if (this.lvFunctions.SelectedItems.Count > 0)
            {
                int count = this.lvFunctions.SelectedItems.Count - 1;
                while (count >= 0)
                {
                    if (this.lvFunctions.SelectedItems[count].Group != this.lvFunctions.Groups[0])
                    {
                        count--;
                    }
                    else
                    {
                        this.btnDeleteMenu.Enabled = true;
                        break;
                    }
                }
            }
        }

        private void btnDeleteLayers_Click(object sender, EventArgs e)
        {
            if (this.lvLayers.SelectedItems.Count > 0)
            {
                SysGrants sysGrant = new SysGrants();
                for (int i = this.lvLayers.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = this.lvLayers.SelectedItems[i];
                    if (item.Group == this.lvLayers.Groups[0])
                    {
                        LayerClass tag = item.Tag as LayerClass;
                        string staffID = (this.lvStaff.SelectedItems[0].Tag as Staff).StaffID;
                        int oID = tag.OID;
                        sysGrant.DeleteGrant(staffID, "Staff", oID.ToString(), tag.DataType);
                        this.lvLayers.Items.Remove(item);
                    }
                }
            }
        }

        private void btnAddLayers_Click(object sender, EventArgs e)
        {
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                if ((new frmAddLayer(this.lvStaff.SelectedItems[0].Tag as Staff)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.lvLayers.Items.Clear();
                    this.ReadSYSGRANTSLayerInfo((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                    this.ReadSYSGRANTSLayerInfoRromRole((this.lvStaff.SelectedItems[0].Tag as Staff).StaffID);
                }
            }
        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (this.lvFunctions.SelectedItems.Count > 0)
            {
                Staff tag = this.lvStaff.SelectedItems[0].Tag as Staff;
                SysGrants sysGrant = new SysGrants();
                for (int i = this.lvFunctions.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = this.lvFunctions.SelectedItems[i];
                    if (item.Group == this.lvFunctions.Groups[0])
                    {
                        MenuInfo menuInfo = item.Tag as MenuInfo;
                        sysGrant.DeleteGrant(tag.StaffID, "Staff", menuInfo.MenuID, "gisPluge");
                    }
                    this.lvFunctions.Items.Remove(item);
                }
            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                frmAddFunction _frmAddFunction = new frmAddFunction(this.lvStaff.SelectedItems[0].Tag as Staff);
                if (_frmAddFunction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < _frmAddFunction.Functions.Count; i++)
                    {
                        MenuInfo item = _frmAddFunction.Functions[i];
                        ListViewItem listViewItem = new ListViewItem(item.ToString())
                        {
                            Group = this.lvFunctions.Groups[0],
                            Tag = item
                        };
                        this.lvFunctions.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (this.lvLayers.Items.Count > 0)
            {
                this.m_IsAll = true;
                this.contextMenuStrip1.Show(this.btnAll, 0, this.btnAll.Height);
            }
        }

        private void btnEditGrant_Click(object sender, EventArgs e)
        {
            this.m_IsAll = false;
            this.contextMenuStrip1.Show(this.btnEditGrant, 0, this.btnEditGrant.Height);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.lvStaff.SelectedIndices.Count > 0)
            {
                frmEditStaff _frmEditStaff = new frmEditStaff()
                {
                    Staff = (Staff)this.lvStaff.SelectedItems[0].Tag
                };
                if (_frmEditStaff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.lvStaff.SelectedItems[0].Text = _frmEditStaff.Staff.RealName;
                }
            }
        }

        private void 无ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(0);
        }

        private void UpdateLayerGRANTS(int newval)
        {
            int i;
            ListViewItem item;
            if (this.lvStaff.SelectedItems.Count != 0)
            {
                string staffID = (this.lvStaff.SelectedItems[0].Tag as Staff).StaffID;
                SysGrants sysGrant = new SysGrants();
                if (!this.m_IsAll)
                {
                    for (i = 0; i < this.lvLayers.SelectedItems.Count; i++)
                    {
                        item = this.lvLayers.SelectedItems[i];
                        if (item.Group == this.lvLayers.Groups[0])
                        {
                            this.UpdateLayerGRANTS(sysGrant, staffID, item.Tag as LayerClass, newval.ToString());
                            item.SubItems[1].Text = this.PrivilegeDes[newval];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < this.lvLayers.Items.Count; i++)
                    {
                        item = this.lvLayers.Items[i];
                        if (item.Group == this.lvLayers.Groups[0])
                        {
                            this.UpdateLayerGRANTS(sysGrant, staffID, item.Tag as LayerClass, newval.ToString());
                            item.SubItems[1].Text = this.PrivilegeDes[newval];
                        }
                    }
                }
            }
        }

        private void UpdateLayerGRANTS(SysGrants sys, string userID, LayerClass layer, string newval)
        {
            int oID = layer.OID;
            sys.UpdateGrant(userID, "Staff", oID.ToString(), layer.DataType, newval);
        }
        
        private void 浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(1);
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(2);
        }

        private void frmUserManage_Load(object sender, EventArgs e)
        {
            this.btnAddLayers.Enabled = false;
            this.btnAddMenu.Enabled = false;
            this.btnDeleteUser.Enabled = false;
            this.btnDeleteRole.Enabled = false;
            this.btnNewRole.Enabled = false;
            List<Staff> staffs = (new ORGStaffHelper()).Load();
            string[] realName = new string[2];
            foreach (Staff staff in staffs)
            {
                realName[0] = staff.RealName;
                realName[1] = staff.StaffID;
                ListViewItem listViewItem = new ListViewItem(realName)
                {
                    Tag = staff
                };
                this.lvStaff.Items.Add(listViewItem);
            }
        }

        private void lvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteRole.Enabled = this.lvRoles.SelectedItems.Count > 0;
        }
    }
}

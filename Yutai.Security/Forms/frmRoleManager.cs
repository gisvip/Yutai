using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Priviliges;

namespace Yutai.Security.Forms
{
    public partial class frmRoleManager : Form
    {
        private bool m_IsAll = false;

        private string[] PrivilegeDes = new string[] { "无权限", "浏览权限", "编辑权限" };

        public frmRoleManager()
        {
            InitializeComponent();
        }

        private void frmRoleManager_Load(object sender, EventArgs e)
        {
            this.btnAssignUser.Enabled = false;
            this.btnAddLayers.Enabled = false;
            this.btnAddMenu.Enabled = false;
            this.btnDeleteAssignUser.Enabled = false;
            List<ORGRole> oRGRoles = (new ORGRoleHelper()).Load();
            string[] roleName = new string[2];
            foreach (ORGRole oRGRole in oRGRoles)
            {
                roleName[0] = oRGRole.RoleName;
                roleName[1] = "";
                ListViewItem listViewItem = new ListViewItem(roleName)
                {
                    Tag = oRGRole
                };
                this.lvRoles.Items.Add(listViewItem);
            }
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(2);
        }

        private void UpdateLayerGRANTS(int newval)
        {
            int i;
            ListViewItem item;
            if (this.lvRoles.SelectedItems.Count != 0)
            {
                string roleID = (this.lvRoles.SelectedItems[0].Tag as ORGRole).RoleID;
                SysGrants sysGrant = new SysGrants();
                if (!this.m_IsAll)
                {
                    for (i = 0; i < this.lvLayers.SelectedItems.Count; i++)
                    {
                        item = this.lvLayers.SelectedItems[i];
                        this.UpdateLayerGRANTS(sysGrant, roleID, item.Tag as LayerClass, newval.ToString());
                        item.SubItems[1].Text = this.PrivilegeDes[newval];
                    }
                }
                else
                {
                    for (i = 0; i < this.lvLayers.Items.Count; i++)
                    {
                        item = this.lvLayers.Items[i];
                        this.UpdateLayerGRANTS(sysGrant, roleID, item.Tag as LayerClass, newval.ToString());
                        item.SubItems[1].Text = this.PrivilegeDes[newval];
                    }
                }
            }
        }

        private void UpdateLayerGRANTS(SysGrants sys, string userID, LayerClass layer, string newval)
        {
            int oID = layer.OID;
            sys.UpdateGrant(userID, "Roles", oID.ToString(), layer.DataType, newval);
        }
        
        private void 浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(1);
        }

        private void 无ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateLayerGRANTS(0);
        }

        private void btnEditGrant_Click(object sender, EventArgs e)
        {
            this.m_IsAll = false;
            this.contextMenuStrip1.Show(this.btnEditGrant, 0, this.btnEditGrant.Height);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (this.lvLayers.Items.Count > 0)
            {
                this.m_IsAll = true;
                this.contextMenuStrip1.Show(this.btnAll, 0, this.btnEditGrant.Height);
            }
        }

        private void lvFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteMenu.Enabled = this.lvFunctions.SelectedIndices.Count > 0;
        }

        private void lvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lvStaffRole.Items.Clear();
            this.lvFunctions.Items.Clear();
            this.lvLayers.Items.Clear();
            if (this.lvRoles.SelectedIndices.Count <= 0)
            {
                this.btnAddLayers.Enabled = false;
                this.btnAddMenu.Enabled = false;
                this.btnDeleteAssignUser.Enabled = false;
                this.btnDeleteRole.Enabled = false;
                this.btnAssignUser.Enabled = false;
                this.btnEditGrant.Enabled = false;
            }
            else
            {
                ORGRole tag = this.lvRoles.SelectedItems[0].Tag as ORGRole;
                this.ReadStaffRoleInfo(tag.RoleID);
                this.ReadSYSGRANTSLayerInfo(tag.RoleID);
                this.ReadSYSGRANTSFunctionInfo(tag.RoleID);
                this.btnAddLayers.Enabled = true;
                this.btnAddMenu.Enabled = true;
                this.btnDeleteAssignUser.Enabled = true;
                this.btnDeleteRole.Enabled = true;
                this.btnAssignUser.Enabled = true;
                this.btnEditGrant.Enabled = this.lvLayers.SelectedIndices.Count > 0;
            }
        }

        private void ReadStaffRoleInfo(object RoleID)
        {
            ORGStaffHelper oRGStaffHelper = new ORGStaffHelper();
            List<string> staffIDs = (new ORGStaffRoleHelper()).GetStaffIDs(RoleID.ToString());
            string[] realName = new string[2];
            foreach (string staffID in staffIDs)
            {
                Staff staff = oRGStaffHelper.Load(staffID);
                if (staff != null)
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

        private void ReadSYSGRANTSFunctionInfo(object RoleID)
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
                        Tag = menuInfo
                    };
                    this.lvFunctions.Items.Add(listViewItem);
                }
            }
        }

        private void ReadSYSGRANTSLayerInfo(object RoleID)
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
                        Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisLayer")
                    };
                    this.lvLayers.Items.Add(listViewItem);
                }
                catch
                {
                }
            }
            table = CommonClass.OpenTable("DATASETTABLE");
            if (table != null)
            {
                num = table.FindField("Name");
                num1 = table.FindField("RealName");
                rolesObjectPri = sysGrant.GetRolesObjectPri(RoleID.ToString(), "gisDataset");
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
                            Tag = new LayerClass(row.OID, privilegeDes[0], str, Convert.ToInt32(item["privilegeFlag"]), true, "gisDataset")
                        };
                        this.lvLayers.Items.Add(listViewItem);
                    }
                    catch
                    {
                    }
                }
            }
        }


        private void lvLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteLayers.Enabled = this.lvLayers.SelectedIndices.Count > 0;
            this.btnEditGrant.Enabled = this.lvLayers.SelectedIndices.Count > 0;
        }

        private void btnAddLayers_Click(object sender, EventArgs e)
        {
            if (this.lvRoles.SelectedItems.Count != 0)
            {
                frmAddLayer _frmAddLayer = new frmAddLayer(this.lvRoles.SelectedItems[0].Tag);
                if (_frmAddLayer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.lvLayers.Items.Clear();
                    string[] layerName = new string[2];
                    for (int i = 0; i < _frmAddLayer.Layers.Count; i++)
                    {
                        LayerClass item = _frmAddLayer.Layers[i];
                        if (item.Privilege != 0)
                        {
                            layerName[0] = item.LayerName;
                            layerName[1] = this.PrivilegeDes[item.Privilege];
                            ListViewItem listViewItem = new ListViewItem(layerName)
                            {
                                Tag = item.OID
                            };
                            this.lvLayers.Items.Add(listViewItem);
                        }
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
                    LayerClass tag = item.Tag as LayerClass;
                    string roleID = (this.lvRoles.SelectedItems[0].Tag as ORGRole).RoleID;
                    int oID = tag.OID;
                    sysGrant.DeleteGrant(roleID, "Roles", oID.ToString(), tag.DataType);
                    this.lvLayers.Items.Remove(item);
                }
            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            if (this.lvRoles.SelectedItems.Count != 0)
            {
                frmAddFunction _frmAddFunction = new frmAddFunction(this.lvRoles.SelectedItems[0].Tag);
                if (_frmAddFunction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < _frmAddFunction.Functions.Count; i++)
                    {
                        MenuInfo item = _frmAddFunction.Functions[i];
                        ListViewItem listViewItem = new ListViewItem(item.ToString())
                        {
                            Tag = item
                        };
                        this.lvFunctions.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (this.lvFunctions.SelectedItems.Count > 0)
            {
                ORGRole tag = this.lvRoles.SelectedItems[0].Tag as ORGRole;
                SysGrants sysGrant = new SysGrants();
                for (int i = this.lvFunctions.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = this.lvFunctions.SelectedItems[i];
                    MenuInfo menuInfo = item.Tag as MenuInfo;
                    sysGrant.DeleteGrant(tag.RoleID, "Roles", menuInfo.MenuID, "gisPluge");
                    this.lvFunctions.Items.Remove(item);
                }
            }
        }

        private void btnDeleteAssignUser_Click(object sender, EventArgs e)
        {
            ITable table = CommonClass.OpenTable("JLK_STAFFRoles");
            string str = string.Concat("RoleID = ", this.lvRoles.SelectedItems[0].Tag.ToString(), " and staffid = '");
            IQueryFilter queryFilterClass = new QueryFilterClass();
            for (int i = this.lvStaffRole.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = this.lvStaffRole.SelectedItems[i];
                queryFilterClass.WhereClause = string.Concat(str, item.SubItems[1].Text, "'");
                ICursor cursor = table.Search(queryFilterClass, false);
                for (IRow j = cursor.NextRow(); j != null; j = cursor.NextRow())
                {
                    j.Delete();
                }
                Marshal.ReleaseComObject(cursor);
                cursor = null;
                this.lvStaffRole.Items.Remove(item);
            }
        }

        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            if (this.lvRoles.SelectedIndices.Count > 0)
            {
                ORGRole tag = this.lvRoles.SelectedItems[0].Tag as ORGRole;
                frmAssignUser _frmAssignUser = new frmAssignUser(tag.RoleID);
                if (_frmAssignUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] realName = new string[2];
                    for (int i = 0; i < _frmAssignUser.Staffs.Count; i++)
                    {
                        Staff item = _frmAssignUser.Staffs[i];
                        realName[0] = item.RealName;
                        realName[1] = item.StaffID;
                        ListViewItem listViewItem = new ListViewItem(realName)
                        {
                            Tag = item
                        };
                        this.lvStaffRole.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void lvStaffRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteAssignUser.Enabled = this.lvStaffRole.SelectedIndices.Count > 0;
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            if (this.lvRoles.SelectedIndices.Count > 0)
            {
                this.lvStaffRole.Items.Clear();
                this.lvFunctions.Items.Clear();
                this.lvLayers.Items.Clear();
                this.DeleteRole((this.lvRoles.SelectedItems[0].Tag as ORGRole).RoleID);
                this.lvRoles.Items.Remove(this.lvRoles.SelectedItems[0]);
            }
        }

        private void DeleteRole(string RoleID)
        {
            (new ORGRoleHelper()).Delete(RoleID);
        }

        private void btnNewRole_Click(object sender, EventArgs e)
        {
            frmNewRole _frmNewRole = new frmNewRole();
            if (_frmNewRole.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] roleName = new string[] { _frmNewRole.Role.RoleName, _frmNewRole.Role.DESCRIPT };
                ListViewItem listViewItem = new ListViewItem(roleName)
                {
                    Tag = _frmNewRole.Role
                };
                this.lvRoles.Items.Add(listViewItem);
            }
        }
    }
}

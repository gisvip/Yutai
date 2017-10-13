using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Priviliges;

namespace Yutai.Security.Forms
{
    public partial class frmAddFunction : Form
    {
        private List<MenuInfo> _functions = new List<MenuInfo>();

        private SysGrants sysgrants = new SysGrants();

        private object _id;

        private int _type;

        internal List<MenuInfo> Functions
        {
            get
            {
                return this._functions;
            }
        }

        public frmAddFunction(object id)
        {
            this.InitializeComponent();
            if (id is ORGRole)
            {
                this._type = 0;
                this._id = (id as ORGRole).RoleID;
            }
            else if (!(id is Staff))
            {
                this._id = id;
            }
            else
            {
                this._type = 1;
                this._id = (id as Staff).StaffID;
            }
        }
        private void clbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.clbFunctions.CheckedItems.Count > 0;
        }

        private void clbFunctions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFunctions.Items.Count; i++)
            {
                this.clbFunctions.SetItemChecked(i, true);
            }
            this.btnOK.Enabled = this.clbFunctions.CheckedItems.Count > 0;
        }

        private void btnInvertSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFunctions.Items.Count; i++)
            {
                bool itemChecked = this.clbFunctions.GetItemChecked(i);
                this.clbFunctions.SetItemChecked(i, !itemChecked);
            }
            this.btnOK.Enabled = this.clbFunctions.CheckedItems.Count > 0;
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFunctions.Items.Count; i++)
            {
                this.clbFunctions.SetItemChecked(i, false);
            }
            this.btnOK.Enabled = this.clbFunctions.CheckedItems.Count > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFunctions.CheckedItems.Count; i++)
            {
                MenuInfo item = this.clbFunctions.CheckedItems[i] as MenuInfo;
                this._functions.Add(item);
                this.sysgrants.AddGrant(this._id.ToString(), (this._type == 0 ? "Roles" : "Staff"), "admin", item.MenuID, "gisPluge", "1");
            }
        }

        private void frmAddFunction_Load(object sender, EventArgs e)
        {
            bool flag;
            DataTable dataTable = null;
            dataTable = (this._type != 0 ? this.sysgrants.GetStaffObjectPri(this._id.ToString(), "gisPluge") : this.sysgrants.GetRolesObjectPri(this._id.ToString(), "gisPluge"));
            List<string> strs = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow item = dataTable.Rows[i];
                strs.Add(Convert.ToString(item["GRANTOBJECT"]));
            }
            foreach (MenuInfo menuInfo in (new MenuInfoHelper()).Load())
            {
                if (strs.IndexOf(menuInfo.MenuID) == -1)
                {
                    string nAME = menuInfo.NAME;
                    string cAPTION = menuInfo.CAPTION;
                    object cLASSNAME = menuInfo.CLASSNAME;
                    object pROGID = menuInfo.PROGID;
                    if (cLASSNAME is DBNull || cLASSNAME.ToString().Trim().Length == 0)
                    {
                        flag = (pROGID is DBNull ? false : pROGID.ToString().Trim().Length != 0);
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        if (cAPTION.Trim().Length == 0)
                        {
                            nAME.Trim();
                        }
                        this.clbFunctions.Items.Add(menuInfo);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}

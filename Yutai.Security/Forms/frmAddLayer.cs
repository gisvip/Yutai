using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Security.Forms
{
    public partial class frmAddLayer : Form
    {
        private SysGrants sysgrants = new SysGrants();

        private object _id;

        private int _type;

        private List<LayerClass> _layers = new List<LayerClass>();

        private bool m_IsAll = false;

        internal List<LayerClass> Layers
        {
            get
            {
                return this._layers;
            }
        }

        public frmAddLayer(object id)
        {
            InitializeComponent();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            int oID;
            string str;
            string str1;
            string str2;
            SysGrants sysGrant = new SysGrants();
            for (int i = 0; i < this._layers.Count; i++)
            {
                LayerClass item = this._layers[i];
                string str3 = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                if (str3 == "无")
                {
                    item.Privilege = 0;
                }
                else if (str3 == "可见")
                {
                    item.Privilege = 1;
                }
                else if (str3 == "可编辑")
                {
                    item.Privilege = 2;
                }
                if (item.Privilege == 0)
                {
                    if (item.Change)
                    {
                        SysGrants sysGrant1 = sysGrant;
                        string str4 = this._id.ToString();
                        str = (this._type == 0 ? "Roles" : "Staff");
                        oID = item.OID;
                        sysGrant1.DeleteGrant(str4, str, oID.ToString(), item.DataType);
                    }
                }
                else if (!item.Exist)
                {
                    SysGrants sysGrant2 = sysGrant;
                    string str5 = this._id.ToString();
                    str2 = (this._type == 0 ? "Roles" : "Staff");
                    string str6 = item.OID.ToString();
                    string dataType = item.DataType;
                    oID = item.Privilege;
                    sysGrant2.AddGrant(str5, str2, "admin", str6, dataType, oID.ToString());
                }
                else if (item.Change)
                {
                    SysGrants sysGrant3 = sysGrant;
                    string str7 = this._id.ToString();
                    str1 = (this._type == 0 ? "Roles" : "Staff");
                    string str8 = item.OID.ToString();
                    string dataType1 = item.DataType;
                    oID = item.Privilege;
                    sysGrant3.UpdateGrant(str7, str1, str8, dataType1, oID.ToString());
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.btnEditGrant.Enabled = this.dataGridView1.SelectedCells.Count > 0;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.m_IsAll = true;
            this.contextMenuStrip1.Show(this.btnAll, 0, this.btnAll.Height);
        }

        private void btnEditGrant_Click(object sender, EventArgs e)
        {
            this.m_IsAll = false;
            this.contextMenuStrip1.Show(this.btnEditGrant, 0, this.btnEditGrant.Height);
        }

        private void 无ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.m_IsAll)
            {
                this.UpdateSelect("无");
            }
            else
            {
                this.UpdateAll("无");
            }
        }

        private void UpdateAll(string ss)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells[1].Value = ss;
            }
        }

        private void UpdateSelect(string ss)
        {
            for (int i = 0; i < this.dataGridView1.SelectedCells.Count; i++)
            {
                this.dataGridView1.Rows[this.dataGridView1.SelectedCells[i].RowIndex].Cells[1].Value = ss;
            }
        }

        private void 浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.m_IsAll)
            {
                this.UpdateSelect("可见");
            }
            else
            {
                this.UpdateAll("可见");
            }
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.m_IsAll)
            {
                this.UpdateSelect("可编辑");
            }
            else
            {
                this.UpdateAll("可编辑");
            }
        }

        private void frmAddLayer_Load(object sender, EventArgs e)
        {
            LayerClass layerClass;
            int i;
            DataRow item;
            IRow row;
            string str;
            string str1;
            int count;
            int oID;
            string str2;
            object value;
            List<string> strs = new List<string>();
            List<string> strs1 = new List<string>();
            DataTable dataTable = null;
            dataTable = (this._type != 0 ? this.sysgrants.GetStaffObjectPri(this._id.ToString(), "gisLayer") : this.sysgrants.GetRolesObjectPri(this._id.ToString(), "gisLayer"));
            ITable table = CommonClass.OpenTable("LayerConfig");
            if (table == null)
                return;
            int num = table.FindField("Name");
            int num1 = table.FindField("FeatureClassName");
            int num2 = 0;
            for (i = 0; i < dataTable.Rows.Count; i++)
            {
                item = dataTable.Rows[i];
                string str3 = Convert.ToString(item["GRANTOBJECT"]);
                strs.Add(str3);
                num2 = int.Parse(Convert.ToString(item["privilege"]));
                row = table.GetRow(Convert.ToInt32(str3));
                str = row.Value[num].ToString();
                str1 = row.Value[num1].ToString();
                strs.Add(str3);
                layerClass = new LayerClass(Convert.ToInt32(str3), str, str1, num2, true);
                this._layers.Add(layerClass);
                this.dataGridView1.Rows.Add();
                count = this.dataGridView1.Rows.Count;
                this.dataGridView1.Rows[count - 1].Cells[0].Value = str;
                if (num2 == 0)
                {
                    this.dataGridView1.Rows[count - 1].Cells[1].Value = "无";
                }
                else if (num2 == 1)
                {
                    this.dataGridView1.Rows[count - 1].Cells[1].Value = "可见";
                }
                else if (num2 == 2)
                {
                    this.dataGridView1.Rows[count - 1].Cells[1].Value = "可编辑";
                }
            }
            ITable table1 = CommonClass.OpenTable("DATASETTABLE");
            if (table1 != null)
            {
                dataTable = (this._type != 0 ? this.sysgrants.GetStaffObjectPri(this._id.ToString(), "gisDataset") : this.sysgrants.GetRolesObjectPri(this._id.ToString(), "gisDataset"));
                int num3 = table1.FindField("Name");
                int num4 = table1.FindField("RealName");
                for (i = 0; i < dataTable.Rows.Count; i++)
                {
                    item = dataTable.Rows[i];
                    strs1.Add(Convert.ToString(item["GRANTOBJECT"]));
                    oID = int.Parse(Convert.ToString(item["GRANTOBJECT"]));
                    num2 = int.Parse(Convert.ToString(item["privilege"]));
                    row = table1.GetRow(oID);
                    str = row.Value[num3].ToString();
                    str1 = row.Value[num4].ToString();
                    strs1.Add(oID.ToString());
                    layerClass = new LayerClass(oID, str, str1, num2, true, "gisDataset");
                    this._layers.Add(layerClass);
                    this.dataGridView1.Rows.Add();
                    count = this.dataGridView1.Rows.Count;
                    this.dataGridView1.Rows[count - 1].Cells[0].Value = str;
                    if (num2 == 0)
                    {
                        this.dataGridView1.Rows[count - 1].Cells[1].Value = "无";
                    }
                    else if (num2 == 1)
                    {
                        this.dataGridView1.Rows[count - 1].Cells[1].Value = "可见";
                    }
                    else if (num2 == 2)
                    {
                        this.dataGridView1.Rows[count - 1].Cells[1].Value = "可编辑";
                    }
                }
            }
            ICursor cursor = table.Search(null, false);
            IRow row1 = cursor.NextRow();
            while (row1 != null)
            {
                oID = row1.OID;
                if (strs.IndexOf(oID.ToString()) == -1)
                {
                    str2 = row1.Value[num].ToString();
                    value = row1.Value[num1];
                    if ((!(value is DBNull) && value.ToString().Trim().Length != 0))
                    {
                        layerClass = new LayerClass(oID, str2, value.ToString(), 0, false);
                        this.dataGridView1.Rows.Add();
                        count = this.dataGridView1.Rows.Count;
                        this.dataGridView1.Rows[count - 1].Cells[0].Value = str2;
                        this.dataGridView1.Rows[count - 1].Cells[1].Value = "无";
                        this._layers.Add(layerClass);
                    }
                    else
                    {
                        row1 = cursor.NextRow();
                        continue;
                    }
                }
                row1 = cursor.NextRow();
            }
            Marshal.ReleaseComObject(cursor);
            if (table1 != null)
            {
                IQueryFilter queryFilterClass = new QueryFilterClass()
                {
                    WhereClause = "DataType = 5"
                };
                num = table1.FindField("Name");
                num1 = table1.FindField("RealName");
                cursor = table1.Search(queryFilterClass, false);
                row1 = cursor.NextRow();
                while (row1 != null)
                {
                    oID = row1.OID;
                    if (strs1.IndexOf(oID.ToString()) == -1)
                    {
                        str2 = row1.Value[num].ToString();
                        value = row1.Value[num1];
                        if ((!(value is DBNull) && value.ToString().Trim().Length != 0))
                        {
                            layerClass = new LayerClass(oID, str2, value.ToString(), 0, false, "gisDataset");
                            this.dataGridView1.Rows.Add();
                            count = this.dataGridView1.Rows.Count;
                            this.dataGridView1.Rows[count - 1].Cells[0].Value = str2;
                            this.dataGridView1.Rows[count - 1].Cells[1].Value = "无";
                            this._layers.Add(layerClass);
                        }
                        else
                        {
                            row1 = cursor.NextRow();
                            continue;
                        }
                    }
                    row1 = cursor.NextRow();
                }
                Marshal.ReleaseComObject(cursor);
            }
        }
    }
}

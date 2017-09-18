using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.TableEditor.Controls;
using Cursor = System.Windows.Forms.Cursor;
using IGxObject = Yutai.ArcGIS.Catalog.IGxObject;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class JoinTable : Form
    {
        private IFeatureClass _featureClass;
        private JoinModel _model;

        public JoinTable()
        {
            InitializeComponent();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
                selectFields1.SelectAll();
            else
                selectFields1.SelectClear();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            _featureClass = SelectFeatureClassDialog();
            txtDatasource.Text = _featureClass.AliasName;
            cboCurrent.Fields = _featureClass.Fields;
            cboExternal.Fields = _featureClass.Fields;
            selectFields1.Fields = _featureClass.Fields;
        }

        public static IFeatureClass SelectFeatureClassDialog()
        {
            frmOpenFile frm = new frmOpenFile()
            {
                Text = @"添加数据",
                AllowMultiSelect = false
            };
            frm.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (frm.DoModalOpen() == DialogResult.OK)
            {
                IGxObject gxObject = frm.Items.get_Element(0) as IGxObject;
                IFeatureClassName className = gxObject.InternalObjectName as IFeatureClassName;
                IFeatureClass featureClass = ((IName) className).Open() as IFeatureClass;
                return featureClass;
            }
            return null;
        }

        public JoinModel Model
        {
            get { return _model; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _model = null;
            if (_featureClass == null)
                return;
            if (cboCurrent.Field == null)
                return;
            if (cboExternal.Field == null)
                return;
            string fields = selectFields1.GetSelectedFields(',');
            if (string.IsNullOrWhiteSpace(fields))
                return;
            if (fields.Contains(cboExternal.Field.Name) == false)
                fields += $",{cboExternal.Field.Name}";
            _model = new JoinModel()
            {
                Table = _featureClass as ITable,
                Name = _featureClass.AliasName,
                FromField = cboCurrent.Field.Name,
                ToField = cboExternal.Field.Name,
                Fields = fields
            };
        }
    }
}
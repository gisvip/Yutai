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
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Pipeline3D.Forms
{
    public partial class Frm3DBuilder : Form, I3DBuilder
    {
        private IPipelineConfig _config;
        private IWorkspace _saveWorkspace;
        private List<I3DItem> _items;
        private I3DItem _currentItem;

        public Frm3DBuilder(IPipelineConfig config)
        {
            InitializeComponent();
            _config = config;

            _items = new List<I3DItem>();
            foreach (IPipelineLayer pipelineLayer in _config.Layers)
            {
                _items.Add(new Pipeline3DItem(this, pipelineLayer));
            }

            this.checkedListBoxPipelineLayers.DataSource = _items;
            checkedListBoxPipelineLayers.DisplayMember = "Name";

            Register();
        }

        public int Division => (int) numDivision.Value;

        public string NameSuf => _nameSuf.Text;

        public IWorkspace SaveWorkspace
        {
            get { return _saveWorkspace; }
        }

        public List<I3DItem> Items
        {
            get { return _items; }
        }

        public List<string> GetCheckPipeline()
        {
            List<string> list = new List<string>();
            foreach (object checkedItem in this.checkedListBoxPipelineLayers.CheckedItems)
            {
                I3DItem pipelineLayer = checkedItem as I3DItem;
                if (pipelineLayer == null)
                    continue;
                list.Add(pipelineLayer.Name);
            }
            return list;
        }

        private void checkedListBoxPipelineLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentItem = checkedListBoxPipelineLayers.SelectedItem as I3DItem;
            if (_currentItem == null)
                return;
            InitFields(_currentItem);
        }

        private void btnSelectWorkspace_Click(object sender, EventArgs e)
        {
            frmOpenFile frmOpenFile = new frmOpenFile()
            {
                Text = "保存位置",
                AllowMultiSelect = false
            };
            frmOpenFile.AddFilter(new MyGxFilterGeoDatabases(), true);
            if (frmOpenFile.DoModalSaveLocation() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.txtSaveAt.Text = frmOpenFile.SelectedItems[0].ToString();
                _saveWorkspace = ((IGxObject) frmOpenFile.SelectedItems[0]).InternalObjectName.Open() as IWorkspace;
                Cursor.Current = Cursors.Default;
            }
        }

        private void InitFields(I3DItem item)
        {
            if (item.HeightType == enumPipelineHeightType.Top)
                cmbHeightType.SelectedItem = "管顶";
            else if (item.HeightType == enumPipelineHeightType.Middle)
                cmbHeightType.SelectedItem = "管中";
            else if (item.HeightType == enumPipelineHeightType.Bottom)
                cmbHeightType.SelectedItem = "管底";
            else
                cmbHeightType.SelectedItem = null;

            if (item.SectionType == enumPipeSectionType.HeightAndWidth)
                cmbSectionType.SelectedItem = "高*宽";
            else if (item.SectionType == enumPipeSectionType.WidthAndHeight)
                cmbSectionType.SelectedItem = "宽*高";
            else
                cmbSectionType.SelectedItem = null;

            if (item.RotationAngleType == enumRotationAngleType.Angle)
                cmbRotationAngleType.SelectedItem = "角度";
            else if (item.RotationAngleType == enumRotationAngleType.Radian)
                cmbRotationAngleType.SelectedItem = "弧度";
            else
                cmbRotationAngleType.SelectedItem = null;

            AddItemsFromFields(item.PointLayerInfo.FeatureClass.Fields, cmbDmgcField,
                string.IsNullOrEmpty(item.DmgcFieldName)
                    ? item.PointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.DMGC)
                    : item.DmgcFieldName);
            AddItemsFromFields(item.PointLayerInfo.FeatureClass.Fields, cmbJgggField,
                string.IsNullOrEmpty(item.JgggFieldName)
                    ? item.PointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.JGGG)
                    : item.JgggFieldName);
            AddItemsFromFields(item.PointLayerInfo.FeatureClass.Fields, cmbJdsdField,
                string.IsNullOrEmpty(item.JdsdFieldName)
                    ? item.PointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.JDSD)
                    : item.JdsdFieldName);
            AddItemsFromFields(item.PointLayerInfo.FeatureClass.Fields, cmbXzjdField,
                string.IsNullOrEmpty(item.XzjdFieldName)
                    ? item.PointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.XZJD)
                    : item.XzjdFieldName);
            AddItemsFromFields(item.PointLayerInfo.FeatureClass.Fields, cmbFswField,
                string.IsNullOrEmpty(item.FswFieldName)
                    ? item.PointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.FSW)
                    : item.FswFieldName);

            AddItemsFromFields(item.LineLayerInfo.FeatureClass.Fields, cmbQdgcField,
                string.IsNullOrEmpty(item.QdgcFieldName)
                    ? item.LineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC)
                    : item.QdgcFieldName);
            AddItemsFromFields(item.LineLayerInfo.FeatureClass.Fields, cmbZdgcField,
                string.IsNullOrEmpty(item.ZdgcFieldName)
                    ? item.LineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC)
                    : item.ZdgcFieldName);
            AddItemsFromFields(item.LineLayerInfo.FeatureClass.Fields, cmbQdmsField,
                string.IsNullOrEmpty(item.QdmsFieldName)
                    ? item.LineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS)
                    : item.QdmsFieldName);
            AddItemsFromFields(item.LineLayerInfo.FeatureClass.Fields, cmbZdmsField,
                string.IsNullOrEmpty(item.ZdmsFieldName)
                    ? item.LineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS)
                    : item.ZdmsFieldName);
            AddItemsFromFields(item.LineLayerInfo.FeatureClass.Fields, cmbGjField,
                string.IsNullOrEmpty(item.GjFieldName)
                    ? item.LineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ)
                    : item.GjFieldName);

            txtCylinderSubs.Text = string.Join(";", item.CylinderSubs);
            txtSquareSubs.Text = string.Join(";", item.SquareSubs);
            txtSphereSubs.Text = string.Join(";", item.SphereSubs);
        }

        private void Register()
        {
            cmbHeightType.SelectedIndexChanged += CmbHeightTypeOnSelectedIndexChanged;
            cmbSectionType.SelectedIndexChanged += CmbSectionTypeOnSelectedIndexChanged;
            cmbRotationAngleType.SelectedIndexChanged += CmbRotationAngleTypeOnSelectedIndexChanged;

            cmbDmgcField.SelectedIndexChanged += CmbDmgcFieldOnSelectedIndexChanged;
            cmbJgggField.SelectedIndexChanged += CmbJgggFieldOnSelectedIndexChanged;
            cmbJdsdField.SelectedIndexChanged += CmbJdsdFieldOnSelectedIndexChanged;
            cmbXzjdField.SelectedIndexChanged += CmbXzjdFieldOnSelectedIndexChanged;
            cmbFswField.SelectedIndexChanged += CmbFswFieldOnSelectedIndexChanged;

            cmbQdgcField.SelectedIndexChanged += CmbQdgcFieldOnSelectedIndexChanged;
            cmbZdgcField.SelectedIndexChanged += CmbZdgcFieldOnSelectedIndexChanged;
            cmbQdmsField.SelectedIndexChanged += CmbQdmsFieldOnSelectedIndexChanged;
            cmbZdmsField.SelectedIndexChanged += CmbZdmsFieldOnSelectedIndexChanged;
            cmbGjField.SelectedIndexChanged += CmbGjFieldOnSelectedIndexChanged;
        }

        private void CmbRotationAngleTypeOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((ComboBox)sender).SelectedItem.ToString() == "角度")
            {
                _currentItem.RotationAngleType = enumRotationAngleType.Angle;
            }
            if (((ComboBox)sender).SelectedItem.ToString() == "弧度")
            {
                _currentItem.RotationAngleType = enumRotationAngleType.Radian;
            }
        }

        private void UnRegister()
        {
            cmbHeightType.SelectedIndexChanged -= CmbHeightTypeOnSelectedIndexChanged;
            cmbSectionType.SelectedIndexChanged -= CmbSectionTypeOnSelectedIndexChanged;
            cmbRotationAngleType.SelectedIndexChanged -= CmbRotationAngleTypeOnSelectedIndexChanged;

            cmbDmgcField.SelectedIndexChanged -= CmbDmgcFieldOnSelectedIndexChanged;
            cmbJgggField.SelectedIndexChanged -= CmbJgggFieldOnSelectedIndexChanged;
            cmbJdsdField.SelectedIndexChanged -= CmbJdsdFieldOnSelectedIndexChanged;
            cmbXzjdField.SelectedIndexChanged -= CmbXzjdFieldOnSelectedIndexChanged;
            cmbFswField.SelectedIndexChanged -= CmbFswFieldOnSelectedIndexChanged;

            cmbQdgcField.SelectedIndexChanged -= CmbQdgcFieldOnSelectedIndexChanged;
            cmbZdgcField.SelectedIndexChanged -= CmbZdgcFieldOnSelectedIndexChanged;
            cmbQdmsField.SelectedIndexChanged -= CmbQdmsFieldOnSelectedIndexChanged;
            cmbZdmsField.SelectedIndexChanged -= CmbZdmsFieldOnSelectedIndexChanged;
            cmbGjField.SelectedIndexChanged -= CmbGjFieldOnSelectedIndexChanged;
        }

        private void CmbSectionTypeOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((ComboBox) sender).SelectedItem.ToString() == "高*宽")
            {
                _currentItem.SectionType = enumPipeSectionType.HeightAndWidth;
            }
            if (((ComboBox) sender).SelectedItem.ToString() == "宽*高")
            {
                _currentItem.SectionType = enumPipeSectionType.WidthAndHeight;
            }
        }

        private void CmbFswFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.FswFieldName = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void CmbHeightTypeOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (((ComboBox) sender).SelectedItem.ToString() == "管顶")
            {
                _currentItem.HeightType = enumPipelineHeightType.Top;
            }
            if (((ComboBox) sender).SelectedItem.ToString() == "管中")
            {
                _currentItem.HeightType = enumPipelineHeightType.Middle;
            }
            if (((ComboBox) sender).SelectedItem.ToString() == "管底")
            {
                _currentItem.HeightType = enumPipelineHeightType.Bottom;
            }
        }

        private void CmbGjFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.GjFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbZdmsFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.ZdmsFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbQdmsFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.QdmsFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbZdgcFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.ZdgcFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbQdgcFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.QdgcFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbXzjdFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.XzjdFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbJdsdFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.JdsdFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbJgggFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.JgggFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void CmbDmgcFieldOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            _currentItem.DmgcFieldName = ((ComboBox) sender).SelectedItem.ToString();
        }

        public void AddItemsFromFields(IFields fields, ComboBox comboBox, string defaultValue = null)
        {
            if (comboBox.Items.Count > 0)
                comboBox.Items.Clear();
            int count = fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField field = fields.Field[i];
                if (field != null)
                {
                    comboBox.Items.Add(field.Name);
                }
            }
            if (string.IsNullOrWhiteSpace(defaultValue) == false)
            {
                if (comboBox.Items.Contains(defaultValue))
                    comboBox.SelectedItem = defaultValue;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Pipeline3DBuilder builder = new Pipeline3DBuilder(this);
                builder.Build();
                MessageBox.Show("执行完成！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnSelectCylinders_Click(object sender, EventArgs e)
        {
            if (_currentItem.FswValueList == null || _currentItem.FswValueList.Count <= 0)
                return;
            FrmValueSelect frm = new FrmValueSelect(_currentItem.FswValueList.Where(c=>_currentItem.CylinderSubs.Contains(c)==false && _currentItem.SquareSubs.Contains(c) == false && _currentItem.SphereSubs.Contains(c) == false).ToList(), _currentItem.CylinderSubs);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _currentItem.CylinderSubs = frm.GetSelectedFsw();
                txtCylinderSubs.Text = string.Join(";",_currentItem.CylinderSubs);
            }
        }

        private void btnSelectSquares_Click(object sender, EventArgs e)
        {
            if (_currentItem.FswValueList == null || _currentItem.FswValueList.Count <= 0)
                return;
            FrmValueSelect frm = new FrmValueSelect(_currentItem.FswValueList.Where(c=>_currentItem.CylinderSubs.Contains(c) == false && _currentItem.SquareSubs.Contains(c) == false && _currentItem.SphereSubs.Contains(c) == false).ToList(), _currentItem.SquareSubs);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _currentItem.SquareSubs = frm.GetSelectedFsw();
                txtSquareSubs.Text = string.Join(";", _currentItem.SquareSubs);
            }
        }

        private void btnSelectSpheres_Click(object sender, EventArgs e)
        {
            if (_currentItem.FswValueList == null || _currentItem.FswValueList.Count <= 0)
                return;
            FrmValueSelect frm = new FrmValueSelect(_currentItem.FswValueList.Where(c => _currentItem.CylinderSubs.Contains(c) == false && _currentItem.SquareSubs.Contains(c) == false && _currentItem.SphereSubs.Contains(c) == false).ToList(), _currentItem.SphereSubs);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _currentItem.SphereSubs = frm.GetSelectedFsw();
                txtSphereSubs.Text = string.Join(";", _currentItem.SphereSubs);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline3D;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class frm3DBuilder : Form
    {
        private IAppContext _context;
        private IPipelineConfig _config;
        private DataTable _sourceTable;
        private bool _isBasic;
        private int _parentID = -1;
        private int _subID = -1;

        public frm3DBuilder(IAppContext context, IPipelineConfig config)
        {
            this.InitializeComponent();
            _context = context;
            _config = config;
            BuildDataTable();
        }

        private void BuildDataTable()
        {
            _sourceTable = new DataTable();
            DataColumn KeyID = new DataColumn("KeyFieldID", Type.GetType("System.Int32"));
            DataColumn ParentID = new DataColumn("ParentID", Type.GetType("System.Int32"));
            DataColumn Selected = new DataColumn("Selected", Type.GetType("System.Boolean"));
            DataColumn NodeName = new DataColumn("LayerName", Type.GetType("System.String"));
            DataColumn SubID = new DataColumn("SubID", Type.GetType("System.Int32"));
            NodeName.Caption = "图层名称";
            _sourceTable.Columns.Add(KeyID);
            KeyID.SetOrdinal(0); //设置为第一列
            _sourceTable.Columns.Add(ParentID);
            ParentID.SetOrdinal(1); //设置为第二列
            _sourceTable.Columns.Add(Selected);
            Selected.SetOrdinal(2); //设置为第二列
            NodeName.ReadOnly = true;
            _sourceTable.Columns.Add(NodeName);
            NodeName.SetOrdinal(3); //设置为第三列
            _sourceTable.Columns.Add(SubID);
            SubID.SetOrdinal(4); //设置为第三列
            treeList1.DataSource = _sourceTable;
            treeList1.ExpandAll();
            treeList1.RefreshDataSource();
            treeList1.Columns["SubID"].Visible = false;
        }

        private void frm3DBuilder_Load(object sender, EventArgs e)
        {
            cmbDefaultDepthType.SelectedIndex = 0;
            cmbDefaultSectionType.SelectedIndex = 0;
            cmbDivision.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            chkJXJ.Checked = true;
            chkLJD.Checked = true;
            txtNameSuf.Text = "_3D";
            //! 依据管线配置进行设置

            int parentCount = _config.Layers.Count;
            int startIndex = 0;

            for (int i = 0; i < _config.Layers.Count; i++)
            {
                startIndex = i + 1;
                DataRow row = _sourceTable.NewRow();
                row["KeyFieldID"] = startIndex;
                row["LayerName"] = _config.Layers[i].Name;
                _sourceTable.Rows.Add(row);
            }

            for (int i = 0; i < _config.Layers.Count; i++)
            {
                startIndex = i + 1;
                IPipelineLayer layer = _config.Layers[i];
                for (int j = 0; j < layer.Layers.Count; j++)
                {
                    if (layer.Layers[j].DataType != enumPipelineDataType.Line &&
                        layer.Layers[j].DataType != enumPipelineDataType.Point) continue;
                    DataRow row = _sourceTable.NewRow();
                    row["KeyFieldID"] = _sourceTable.Rows.Count + 1;
                    row["ParentID"] = startIndex;
                    row["SubID"] = j;
                    row["LayerName"] = layer.Layers[j].Name;
                    _sourceTable.Rows.Add(row);
                }

            }
            treeList1.ExpandAll();
            treeList1.RefreshDataSource();

        }

        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void SetCheckedParentNodes(TreeListNode eNode, CheckState nodeCheckState)
        {
            if (eNode.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < eNode.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)eNode.ParentNode.Nodes[i].CheckState;
                    if (!nodeCheckState.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                eNode.ParentNode.CheckState = b ? CheckState.Indeterminate : nodeCheckState;
                SetCheckedParentNodes(eNode.ParentNode, nodeCheckState);
            }
        }

        private void SetCheckedChildNodes(TreeListNode eNode, CheckState nodeCheckState)
        {
            for (int i = 0; i < eNode.Nodes.Count; i++)
            {
                eNode.Nodes[i].CheckState = nodeCheckState;
                SetCheckedChildNodes(eNode.Nodes[i], nodeCheckState);
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null || node.Level == 0)
            {
                _isBasic = false;
                return;
            }
            _isBasic = true;
            _parentID = Convert.ToInt32(node["ParentID"]);
            _subID = Convert.ToInt32(node["SubID"]);
            IBasicLayerInfo layer = _config.Layers[_parentID].Layers[_subID];
            cmbDepthType.SelectedIndex = (int)layer.DepthType;
            cmbHeightType.SelectedIndex = (int)layer.HeightType;
            cmbSectionType.SelectedIndex = (int)layer.SectionType;
        }

        private void cmbHeightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isBasic == false) return;

            IBasicLayerInfo layer = _config.Layers[_parentID].Layers[_subID];
            layer.HeightType = (enumPipelineHeightType)cmbHeightType.SelectedIndex;
        }

        private void cmbSectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isBasic == false) return;

            IBasicLayerInfo layer = _config.Layers[_parentID].Layers[_subID];
            layer.SectionType = (enumPipeSectionType)cmbSectionType.SelectedIndex;
        }

        private void cmbDepthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isBasic == false) return;
            IBasicLayerInfo layer = _config.Layers[_parentID].Layers[_subID];
            layer.DepthType = (enumPipelineDepthType)cmbDepthType.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSaveAt.Enabled = comboBox1.SelectedIndex == 1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmOpenFile _frmOpenFile = new frmOpenFile()
            {
                Text = "保存位置",
                AllowMultiSelect = false
            };
            _frmOpenFile.AddFilter(new MyGxFilterGeoDatabases(), true);
            if (_frmOpenFile.DoModalSaveLocation() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.txtSaveAt.Text = _frmOpenFile.SelectedItems[0].ToString();
                this.txtSaveAt.Tag =
                    ((IGxObject)_frmOpenFile.SelectedItems[0]).InternalObjectName.Open() as IWorkspace;
                Cursor.Current = Cursors.Default;
            }

            //IGxDialog dialog = new GxDialogClass();
            //IGxObjectFilterCollection filterCollection = dialog as IGxObjectFilterCollection;
            //filterCollection.AddFilter(new GxFilterWorkspacesClass(), true);
            //filterCollection.AddFilter(new GxFilterDatasetsClass(), false);
            //dialog.AllowMultiSelect = false;
            //dialog.ButtonCaption = "选择";
            //dialog.RememberLocation = true;
            //dialog.Title = "选择输出位置";
            //IEnumGxObject selection = null;
            //if (dialog.DoModalOpen(0, out selection))
            //{
            //    if (selection == null)
            //        return;
            //    IGxObject gxObject = selection.Next();
            //    if (gxObject is IGxDatabase)
            //    {
            //        IGxDatabase gxDatabase = gxObject as IGxDatabase;
            //        _dataset = null;
            //        _workspace = gxDatabase.Workspace;
            //        _saveType = EnumSaveType.Workspace;
            //    }
            //    else if (gxObject is IGxDataset)
            //    {
            //        IGxDataset gxDataset = gxObject as IGxDataset;
            //        _dataset = gxDataset.Dataset;
            //        _workspace = _dataset.Workspace;
            //        _saveType = EnumSaveType.Dataset;
            //    }
            //    txtPath.Text = gxObject.FullName;

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Pipeline3DBuilderProperty property = new Pipeline3DBuilderProperty();
            property.Division = Convert.ToInt32(cmbDivision.Text);
            property.IsExtentOnly = chkExtent.Checked;
            property.Envelope = _context.ActiveView.Extent;
            property.NameSuf = txtNameSuf.Text.Trim();
            property.SaveType = (enumMultiSaveType)comboBox1.SelectedIndex;
            property.DefaultDepthType = (enumPipelineDepthType)cmbDefaultDepthType.SelectedIndex;
            property.DefaultSectionType = (enumPipeSectionType)cmbDefaultSectionType.SelectedIndex;
            property.IsCreateJXJ = chkJXJ.Checked;
            property.IsCreateLJD = chkLJD.Checked;
            property.SaveWorkspace = txtSaveAt.Tag as IWorkspace;
            
            //! 开始遍历选择的对象
            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {
                TreeListNode pipelineNode = treeList1.Nodes[i];
                if (pipelineNode.Checked == false) continue;
                int startIndex = Convert.ToInt32(pipelineNode["KeyFieldID"]);
                IPipelineLayer layer = new PipelineLayer(_config.Layers[startIndex - 1], true);
                for (int j = layer.Layers.Count - 1; j >= 0; j--)
                {
                    bool bFind = false;
                    for (int k = 0; k < pipelineNode.Nodes.Count; k++)
                    {
                        int subID = Convert.ToInt32(pipelineNode.Nodes[k]["SubID"]);
                        if (subID == j)
                        {
                            bFind = true;
                            if (pipelineNode.Nodes[k].Checked == false)
                            {
                                layer.Layers.RemoveAt(j);
                                break;
                            }
                        }
                    }
                    if (bFind == false) layer.Layers.RemoveAt(j);
                }
                property.BuilderItems.Add(new Pipeline3DBuilderItem(property, layer));
            }
            
            //Pipeline3DBuilder builder = new Pipeline3DBuilder();
            //builder.BuildProperty = property;
            //for (int i = 0; i < pipelineLayers.Count; i++)
            //{
            //    builder.PipelineLayer = pipelineLayers[i];
            //    builder.Build();
            //}
            try
            {
                Pipeline3DBuilder builder = new Pipeline3DBuilder();
                builder.BuilderPropertie = property;
                builder.Build();
                MessageBox.Show(@"执行完成！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}

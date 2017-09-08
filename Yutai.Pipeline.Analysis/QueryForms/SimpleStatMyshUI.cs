using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.ConfigForms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleStatMyshUI : Form
    {
        private int _idxQdms = -1;
        private int _idxZdms = -1;
        private WaitForm _waitForm;
        private IDictionary<double, double> values;
        private List<IFeatureLayer> _featureLayers;

        private partial class LayerboxItem
        {
            public IFeatureLayer m_pPipeLayer;

            public override string ToString()
            {
                return this.m_pPipeLayer.Name;
            }
        }

        public IGeometry m_ipGeo;

        public IAppContext m_context;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        private IList<IPoint> DXArray = new List<IPoint>();

        private DataTable Sumtable = new DataTable();

        public bool SelectGeometry
        {
            get { return this.GeometrySet.Checked; }
            set { this.GeometrySet.Checked = value; }
        }

        public SimpleStatMyshUI()
        {
            this.InitializeComponent();
        }

        private void SimpleStatMyshUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            this.checkedListBox1.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.Layer[i];
                this.AddLayer(ipLay);
            }
            if (this.checkedListBox1.Items.Count > 0)
            {
                this.checkedListBox1.SelectedIndex = 0;
            }
        }

        public void FillValue()
        {
            if (this.myfields != null)
            {
                IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(featureClass);
                int num = this.myfields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                if (num > 0)
                {
                    this.myfield = this.myfields.Field[num];
                    this.listBox1.Items.Clear();
                    List<string> values = new List<string>();
                    CommonHelper.GetUniqueValues((ITable)featureClass,
                        layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS), values);
                    this.listBox1.Items.AddRange(values.ToArray());
                }
            }
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer)ipLay);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer)ipLay);
            }
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer ipLay = compositeLayer.Layer[i];
                    this.AddLayer(ipLay);
                }
            }
        }

        private void AddFeatureLayer(IFeatureLayer iFLayer)
        {
            if (iFLayer != null)
            {
                string aliasName = iFLayer.FeatureClass.AliasName;
                if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    SimpleStatMyshUI.LayerboxItem layerboxItem = new SimpleStatMyshUI.LayerboxItem();
                    layerboxItem.m_pPipeLayer = iFLayer;
                    this.checkedListBox1.Items.Add(layerboxItem);
                }
            }
        }

        private bool ColumnEqual(object A, object B)
        {
            return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
        }

        private void CalButton_Click(object sender, EventArgs e)
        {
            if (_idxQdms < 0 || _idxZdms < 0)
                return;
            _featureLayers = new List<IFeatureLayer>();
            foreach (object checkedItem in checkedListBox1.CheckedItems)
            {
                LayerboxItem item = checkedItem as LayerboxItem;
                if (item == null)
                    continue;
                _featureLayers.Add(item.m_pPipeLayer);
            }
            if (_featureLayers == null || _featureLayers.Count <= 0)
            {
                MessageBox.Show(@"请选定需要统计的管线");
                return;
            }
            int rowCount = this.dataGridView1.RowCount;
            if (rowCount <= 0)
            {
                MessageBox.Show(@"请确定上下限的值，其值不能为空");
                return;
            }
            if (this.dataGridView1[0, 0].Value == null && this.dataGridView1[1, 0].Value == null)
            {
                MessageBox.Show(@"没有确定埋深的范围");
                return;
            }
            values = new Dictionary<double, double>();
            int count2 = this.dataGridView1.Rows.Count;

            for (int j = 0; j < count2; j++)
            {
                this.DXArray.Clear();
                string text = (string)this.dataGridView1[0, j].Value;
                string text2 = (string)this.dataGridView1[1, j].Value;
                if (text == null || text2 == null)
                {
                    MessageBox.Show(@"请确定上下限的值，其值不能为空");
                    return;
                }
                double num = 0.0;
                double num2 = 0.0;
                try
                {
                    num = Convert.ToDouble(text);
                    num2 = Convert.ToDouble(text2);
                    values.Add(num, num2);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"请确定上下限的值是否输入有误");
                    return;
                }
            }

            if (values == null || values.Count <= 0)
                return;

            if (_waitForm == null || _waitForm.IsDisposed)
            {
                _waitForm = new WaitForm()
                {
                    VisibleBackButton = true,
                    VisibleprogressBarControl = true,
                    Description = "状态:准备统计，请稍等...",
                    TopMost = true
                };
                _waitForm.Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                _waitForm.Worker.DoWork += Worker_DoWork;
            }
            _waitForm.Show();
            while (_waitForm.Worker.IsBusy)
            {
                System.Threading.Thread.Sleep(1000);
                _waitForm.Description = "状态:任务正忙，请稍等...";
            }
            CalButton.Enabled = false;
            _waitForm.Worker.RunWorkerAsync();

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _waitForm.Worker.ReportProgress(0, "状态:正在创建数据表，请稍等...");
                _waitForm.Count = values.Count * _featureLayers.Count;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Clear();
                if (!dataTable.Columns.Contains("层名"))
                {
                    dataTable.Columns.Add("层名", typeof(string));
                }
                if (!dataTable.Columns.Contains("统计范围"))
                {
                    dataTable.Columns.Add("统计范围", typeof(string));
                }
                if (!dataTable.Columns.Contains("个数"))
                {
                    dataTable.Columns.Add("个数", typeof(int));
                }
                
                _waitForm.Worker.ReportProgress(0, "状态:开始统计，请稍等...");
                int i = 0;
                foreach (IFeatureLayer featureLayer in _featureLayers)
                {
                    IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(featureLayer.FeatureClass.AliasName);
                    if (layerInfo == null)
                    {
                        _waitForm.Worker.ReportProgress(i++, $"状态:正在统计 {featureLayer.Name}，请稍等...");
                        continue;
                    }

                    ISpatialFilter spatialFilter = new SpatialFilter();
                    if (this.GeometrySet.Checked)
                    {
                        if (this.m_ipGeo != null)
                        {
                            spatialFilter.Geometry = (this.m_ipGeo);
                        }
                        spatialFilter.SpatialRel = (esriSpatialRelEnum)(1);
                    }
                    string qdmsFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS);
                    string zdmsFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS);
                    if (featureLayer.FeatureClass.FindField(qdmsFieldName) < 0 || featureLayer.FeatureClass.FindField(zdmsFieldName) < 0)
                    {
                        _waitForm.Worker.ReportProgress(i++, $"状态:正在统计 {featureLayer.Name}，请稍等...");
                        continue;
                    }
                    foreach (KeyValuePair<double, double> pair in values)
                    {
                        _waitForm.Worker.ReportProgress(i++, $"状态:正在统计 {featureLayer.Name}，请稍等...");
                        spatialFilter.WhereClause = $"( {qdmsFieldName} > {pair.Key} AND {zdmsFieldName} < {pair.Value}) OR ( {qdmsFieldName} > {pair.Key} AND {zdmsFieldName} < {pair.Value})";
                        IFeatureCursor featureCursor = featureLayer.FeatureClass.Search(spatialFilter, false);
                        IDataStatistics dataStatistics = new DataStatisticsClass();
                        dataStatistics.Field = featureLayer.FeatureClass.OIDFieldName;
                        dataStatistics.Cursor = featureCursor as ICursor;

                        object obj = pair.Key + "-" + pair.Value;
                        dataTable.Rows.Add(new object[]
                        {
                            featureLayer.Name,
                            obj,
                            dataStatistics.Statistics.Count
                        });
                        Marshal.ReleaseComObject(featureCursor);
                    }
                }
                e.Result = dataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CalButton.Enabled = true;
            DataTable dataTable = e.Result as DataTable;
            if (dataTable == null)
            {
                MessageBox.Show(@"统计失败！");
            }
            new ClassCollectResultForm
            {
                nType = 0,
                ResultTable = dataTable,
                TopMost = true
            }.Show();
        }

        private void AllBut_Click(object sender, EventArgs e)
        {
            this.minNum = 1000.0;
            this.maxNum = -1000.0;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
            this.listBox1.Items.Clear();
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    IFeatureLayer pPipeLayer =
                        ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
                    this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
                }
            }
            this.all();
        }

        public void all()
        {
            this.listBox1.Items.Clear();
            double num = Convert.ToDouble(this.numericUpDown1.Value);
            if (this.maxNum - this.minNum < 0.0001)
            {
                this.listBox1.Items.Add(this.maxNum);
            }
            else
            {
                double num2 = (this.maxNum - this.minNum) / num;
                int num3 = 0;
                while ((double)num3 < num + 1.0)
                {
                    string text = (this.minNum + (double)num3 * num2).ToString("f2");
                    if (!this.listBox1.Items.Contains(text))
                    {
                        this.listBox1.Items.Add(text);
                    }
                    num3++;
                }
            }
        }

        private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, ListBox lbVal)
        {
            IFeatureClass featureClass = pFeaLay.FeatureClass;
            IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(featureClass);
            IQueryFilter queryFilter = new QueryFilter();
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
            IFeature feature;
            _idxQdms = featureClass.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
            _idxZdms = featureClass.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
            if (_idxQdms != -1 && _idxZdms != -1)
            {
                double num3 = 2147483647.0;
                double num4 = -2147483648.0;
                this.minNum = 2147483647.0;
                this.maxNum = -2147483648.0;
                while ((feature = featureCursor.NextFeature()) != null)
                {
                    try
                    {
                        double num5 = ConvertToDouble(feature.Value[_idxQdms]);
                        double num6 = ConvertToDouble(feature.Value[_idxZdms]);
                        if (double.IsNaN(num5) || double.IsNaN(num6))
                            continue;
                        num3 = ((num3 < num5) ? num3 : num5);
                        num3 = ((num3 < num6) ? num3 : num6);
                        num4 = ((num4 > num5) ? num4 : num5);
                        num4 = ((num4 > num6) ? num4 : num6);
                    }
                    catch (Exception exception)
                    {
                        continue;
                    }
                    if (num3 < this.minNum)
                    {
                        this.minNum = num3;
                    }
                    if (num4 > this.maxNum)
                    {
                        this.maxNum = num4;
                    }
                }
                this.all();
            }
        }
        public double ConvertToDouble(object obj)
        {
            double value;
            if (obj == null || obj is DBNull || double.TryParse(obj.ToString(), out value) == false)
            {
                value = Double.NaN;
            }
            return value;
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
            this.listBox1.Items.Clear();
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
                else
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            this.listBox1.Items.Clear();
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    IFeatureLayer pPipeLayer =
                        ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
                    this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(@"行数已为空");
            }
            else
            {
                DataGridViewRow dataGridViewRow = this.dataGridView1.CurrentRow;
                if (dataGridViewRow != null)
                    this.dataGridView1.Rows.RemoveAt(dataGridViewRow.Index);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show(@"请选定要统计的管线");
            }
            else if (this.listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"请选定需要的埋深");
            }
            else
            {
                int count = this.dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show(@"请用户添加行");
                }
                else
                {
                    string value = this.listBox1.SelectedItem.ToString();
                    double num = Convert.ToDouble(value);
                    DataGridViewRow dataGridViewRow = this.dataGridView1.CurrentRow;
                    if (dataGridViewRow != null)
                    {
                        int index = dataGridViewRow.Index;
                        double num2 = Convert.ToDouble(this.dataGridView1[0, index].Value);
                        if (num2 > num)
                        {
                            MessageBox.Show(@"下限值不应大于上限值");
                        }
                        else
                        {
                            dataGridViewRow.Cells[1].Value = value;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show(@"请选定要统计的管线");
            }
            else if (this.listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"请选定需要的埋深");
            }
            else
            {
                int count = this.dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show(@"请用户添加行");
                }
                else
                {
                    string value = this.listBox1.SelectedItem.ToString();
                    double num = Convert.ToDouble(value);
                    DataGridViewRow dataGridViewRow = this.dataGridView1.CurrentRow;
                    if (dataGridViewRow != null)
                    {
                        int index = dataGridViewRow.Index;
                        double num2 = Convert.ToDouble(this.dataGridView1[1, index].Value);
                        if (num2 < num && Math.Abs(num2) > 0)
                        {
                            MessageBox.Show(@"下限值不应大于上限值");
                        }
                        else
                        {
                            dataGridViewRow.Cells[0].Value = value;
                        }
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            SimpleStatMyshUI.LayerboxItem item = checkedListBox1.SelectedItem as SimpleStatMyshUI.LayerboxItem;
            if (item == null)
                return;
            IFeatureLayer featureLayer = item.m_pPipeLayer;
            this.FillFieldValuesToListBox(featureLayer, this.listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.GeometrySet.Checked = false;
            base.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                this.dataGridView1.Rows.Add();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.all();
        }

        private void InsertBut_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                this.dataGridView1.Rows.Add();
            }
            else
            {
                DataGridViewRow dataGridViewRow = this.dataGridView1.CurrentRow;
                if (dataGridViewRow != null)
                    this.dataGridView1.Rows.Insert(dataGridViewRow.Index, new object[0]);
            }
        }

        private void SimpleStatMyshUI_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw += AxMapControlOnOnAfterDraw;
            }
            else
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            }
        }

        private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
        {
            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }

        private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            this.OnClosed(e);
        }

        public void DrawSelGeometry()
        {
            if (this.m_ipGeo != null)
            {
                IRgbColor rgbColor = new RgbColor();
                IColor selectionCorlor = this.m_context.Config.SelectionEnvironment.DefaultColor;
                rgbColor.RGB = selectionCorlor.RGB;
                rgbColor.Transparency = selectionCorlor.Transparency;

                object obj = null;
                int selectionBufferInPixels = this.m_context.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
                switch ((int)this.m_ipGeo.GeometryType)
                {
                    case 1:
                        {
                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                            symbol = (ISymbol)simpleMarkerSymbol;
                            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                            simpleMarkerSymbol.Color = (rgbColor);
                            simpleMarkerSymbol.Size =
                                ((double)(selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels));
                            break;
                        }
                    case 3:
                        {
                            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                            symbol = (ISymbol)simpleLineSymbol;
                            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                            simpleLineSymbol.Color = rgbColor;
                            simpleLineSymbol.Color.Transparency = 1;
                            simpleLineSymbol.Width = ((double)selectionBufferInPixels);
                            break;
                        }
                    case 4:
                    case 5:
                        {
                            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                            symbol = (ISymbol)simpleFillSymbol;
                            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                            simpleFillSymbol.Color = rgbColor;
                            simpleFillSymbol.Color.Transparency = 1;
                            break;
                        }
                }
                obj = symbol;
                this.MapControl.DrawShape(this.m_ipGeo, ref obj);
            }
        }

        private void GeometrySet_Click(object sender, EventArgs e)
        {
            if (!this.GeometrySet.Checked)
            {
                this.m_ipGeo = null;
                m_context.ActiveView.Refresh();
            }
        }

        private void SimpleStatMyshUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "分段统计";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}
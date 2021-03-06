﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.ConfigForms;
using Yutai.Pipeline.Analysis.QueryForms;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Pipeline.Analysis.Views
{
    public partial class QueryResultView : DockPanelControlBase, IQueryResultView
    {
        private IEnumerable<ToolStripItemCollection> _toolStrips;
        private IEnumerable<Control> _buttons;
        private IAppContext _context;
        private IFeatureCursor _cursor;
        private IFeatureSelection _selection;
        private DataSet pDataSet = new DataSet("总表");
        private DataTable stable = new DataTable();
        private DataTable Geotable = new DataTable();
        private int nGeoType = -1;
        private bool bFitWidth = true;
        private bool bShowGeo = true;
        private IGeometry AllGeo;
        private bool bControlEvent;
        private int OidField;
        // private CustomColumnChooser customColumnChooserDialog;
        private string TopBandText;
        private Color TopBandColor;
        private bool bHaveTop;
        private PipelineAnalysisPlugin _plugin;
        private char[] char_widthheight = new char[] {'x', 'X', 'Х'};
        private IBasicLayerInfo _layerInfo;
        private IPipelineConfig _config;
        private ISpatialFilter _spatialFilter;
        private WaitForm _waitForm;

        public QueryResultView()
        {
            InitializeComponent();
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }


        public override Bitmap Image
        {
            get { return Properties.Resources.icon_excel; }
        }

        public override string Caption
        {
            get { return "查询结果"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Bottom; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "PipelineAnalysis_QueryResult";

        public IFeatureCursor Cursor
        {
            get { return this._cursor; }
            set { this._cursor = value; }
        }

        public void Initialize(IAppContext context)
        {
            _context = context;
        }

        public void SetResult(IFeatureCursor cursor, IFeatureSelection selection, ISpatialFilter spatialFilter)
        {
            _cursor = cursor;
            _selection = selection;
            _spatialFilter = spatialFilter;
            InitCombos();
        }

        private void InitCombos()
        {
            if (_plugin == null)
            {
                _plugin = _context.Container.GetSingleton<PipelineAnalysisPlugin>();
            }
            this.cmbStatWay.SelectedIndex = 0;
            this.bControlEvent = true;
            this.UpdateGrid();
            this.bControlEvent = false;
        }

        private void UpdateGrid()
        {
            if (_waitForm == null || _waitForm.IsDisposed)
            {
                _waitForm = new WaitForm();
                _waitForm.Worker.DoWork += Worker_DoWork;
                _waitForm.Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            }
            _waitForm.TopMost = true;
            _waitForm.Show();
            try
            {
                this.pDataSet = new DataSet("总表");
                this.stable = new DataTable("属性表");
                this.Geotable = new DataTable("空间");
                this.gridControl1.DataSource = (null);
                this.cmbStatField.Items.Clear();

                if (AddItems() == false)
                {
                    _waitForm.Close();
                    return;
                }

                IFeatureCursor pCursor = _layerInfo.FeatureClass.Search(_spatialFilter, false);
                IDataStatistics dataStatistics = new DataStatisticsClass();
                dataStatistics.Field = _layerInfo.FeatureClass.OIDFieldName;
                dataStatistics.Cursor = pCursor as ICursor;
                _waitForm.Count = dataStatistics.Statistics.Count;
                Marshal.ReleaseComObject(pCursor);

                _waitForm.Worker.RunWorkerAsync();
            }
            catch(Exception exception)
            {
                _waitForm.Close();
                MessageBox.Show(exception.Message);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool) e.Result)
            {
                this.pDataSet.Tables.Add(this.stable);
                this.pDataSet.Tables.Add(this.Geotable);
                DataRelation relation = new DataRelation("空间表", this.stable.Columns[0], this.Geotable.Columns[0]);
                this.pDataSet.Relations.Add(relation);
                this.gridControl1.DataSource = (this.pDataSet.Tables[0]);
                for (int i = 0; i < this.mainGridView.Columns.Count; i++)
                {
                    this.mainGridView.Columns[i].BestFit();
                    this.mainGridView.Columns[i].Width = ((int) ((double) this.mainGridView.Columns[i].Width*1.4));
                    string key = this.mainGridView.Columns[i].FieldName;
                    if (i == this.OidField) continue;
                    if (_layerInfo != null)
                    {
                        IYTField pField = _layerInfo.Fields.FirstOrDefault(c => c.Name == key);
                        if (pField == null)
                        {
                            this.mainGridView.Columns[i].Visible = false;
                            continue;
                        }
                        this.mainGridView.Columns[i].Caption = pField.AliasName;
                        this.mainGridView.Columns[i].Visible = pField.Visible;
                    }
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = this.MakeData();
            }
            catch (Exception exception)
            {

                throw;
            }
        }

        private bool AddItems()
        {
            IFeature feature = _cursor?.NextFeature();
            if (feature == null)
                return false;
            _layerInfo = _plugin.PipeConfig.GetBasicLayerInfo(feature.Class.AliasName);
            if (_layerInfo == null)
                return false;
            IFields fields = this._cursor.Fields;
            int num = 0;
            this.OidField = 0;
            this.stable.Columns.Add("OID", typeof(string));
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                string name = field.Name;
                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    num = i;
                }
                if (field.Type == esriFieldType.esriFieldTypeOID)
                {
                    this.OidField = i;
                }
                else
                {
                    if (field.Type == esriFieldType.esriFieldTypeDouble ||
                        field.Type == esriFieldType.esriFieldTypeSingle ||
                        field.Type == esriFieldType.esriFieldTypeInteger ||
                        field.Type == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        this.cmbCalField.Items.Add(field.Name);
                    }
                    this.cmbStatField.Items.Add(field.Name);

                    if (!this.stable.Columns.Contains(fields.Field[i].Name))
                    {
                        IYTField pField = _layerInfo.Fields.FirstOrDefault(c => c.Name == field.Name);
                        DataColumn dataColumn;
                        switch (field.Type)
                        {
                            case esriFieldType.esriFieldTypeInteger:
                                dataColumn = this.stable.Columns.Add(name, typeof(int));
                                break;
                            case esriFieldType.esriFieldTypeSingle:
                                dataColumn = this.stable.Columns.Add(name, typeof(float));
                                break;
                            case esriFieldType.esriFieldTypeDouble:
                                dataColumn = this.stable.Columns.Add(name, typeof(double));
                                break;
                            case esriFieldType.esriFieldTypeOID:
                                dataColumn = this.stable.Columns.Add(name, typeof(string));
                                break;
                            default:
                                dataColumn = this.stable.Columns.Add(name, typeof(string));
                                break;
                        }
                        if (pField != null)
                        {
                            dataColumn.Caption = pField.AliasName;
                        }
                    }
                }
            }
            if (this.cmbCalField.Items.Count > 0)
            {
                this.cmbCalField.SelectedIndex = 0;
            }


            if (feature.FeatureType == esriFeatureType.esriFTComplexEdge ||
                feature.FeatureType == esriFeatureType.esriFTSimpleEdge)
            {
                this.cmbStatWay.Items.Add("统计长度");
            }
            return true;
        }

        private bool MakeData()
        {
            try
            {
                //_waitForm.Worker.ReportProgress(0, "状态:正在统计,请稍后...");
                bool result;

                _waitForm.Worker.ReportProgress(0, "状态:创建临时表,请稍候...");
                _cursor = _layerInfo.FeatureClass.Search(_spatialFilter, false);
                IFeature feature = this._cursor.NextFeature();
                IFields fields = this._cursor.Fields;
                int num = 0;
                this.OidField = 0;

                bool flag = false;

                bool isNUsing = false;
                IMAware mAware = feature.Shape as IMAware;
                isNUsing = mAware.MAware;
                if (feature.FeatureType == esriFeatureType.esriFTComplexEdge ||
                    feature.FeatureType == esriFeatureType.esriFTSimpleEdge)
                {
                    if (!this.stable.Columns.Contains("GD管线长度"))
                    {
                        this.stable.Columns.Add("GD管线长度", typeof(double));
                    }
                    flag = true;
                    this.nGeoType = 1;
                }
                else if (feature.FeatureType == esriFeatureType.esriFTSimpleJunction ||
                         feature.FeatureType == esriFeatureType.esriFTComplexJunction)
                {
                    this.nGeoType = 0;
                }
                else if (feature.FeatureType == esriFeatureType.esriFTSimple)
                {
                    if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        this.nGeoType = 0;
                    }
                    if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPath ||
                        feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        this.nGeoType = 3;
                    }
                    if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        this.nGeoType = 2;
                    }
                    if (feature.Shape.GeometryType == esriGeometryType.esriGeometryNull)
                    {
                        this.nGeoType = -1;
                    }
                }
                if (feature.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    this.nGeoType = 4;
                }
                if (this.nGeoType == -1)
                {
                    result = false;
                }
                else
                {
                    if (this.nGeoType > 0)
                    {
                        if (!this.Geotable.Columns.Contains("LINECODE"))
                        {
                            this.Geotable.Columns.Add("LINECODE", typeof(string));
                        }
                        if (!this.Geotable.Columns.Contains("序号"))
                        {
                            this.Geotable.Columns.Add("序号", typeof(int));
                        }
                        if (!this.Geotable.Columns.Contains("X"))
                        {
                            this.Geotable.Columns.Add("X", typeof(double));
                        }
                        if (!this.Geotable.Columns.Contains("Y"))
                        {
                            this.Geotable.Columns.Add("Y", typeof(double));
                        }
                        if (!this.Geotable.Columns.Contains("Z"))
                        {
                            this.Geotable.Columns.Add("Z", typeof(double));
                        }
                        if (!this.Geotable.Columns.Contains("M"))
                        {
                            this.Geotable.Columns.Add("M", typeof(double));
                        }
                    }
                    else
                    {
                        if (!this.Geotable.Columns.Contains("PONTCODE"))
                        {
                            this.Geotable.Columns.Add("PONTCODE", typeof(string));
                        }
                        if (!this.Geotable.Columns.Contains("X"))
                        {
                            this.Geotable.Columns.Add("X", typeof(double));
                        }
                        if (!this.Geotable.Columns.Contains("Y"))
                        {
                            this.Geotable.Columns.Add("Y", typeof(double));
                        }
                        if (!this.Geotable.Columns.Contains("Z"))
                        {
                            this.Geotable.Columns.Add("Z", typeof(double));
                        }
                    }
                    object[] array;
                    object[] array2;
                    if (this.nGeoType > 0)
                    {
                        if (this.nGeoType == 1)
                        {
                            array = new object[fields.FieldCount + 1];
                        }
                        else
                        {
                            array = new object[fields.FieldCount];
                        }
                        array2 = new object[6];
                    }
                    else
                    {
                        array = new object[fields.FieldCount];
                        array2 = new object[4];
                    }
                    string text = feature.FeatureType.ToString();
                    this._selection.Clear();
                    _waitForm.Worker.ReportProgress(0, "状态:填充表单,请稍候...");

                    int qdgcIdx = -1;
                    int qdmsIdx = -1;
                    int zdgcIdx = -1;
                    int zdmsIdx = -1;
                    if (_layerInfo.DataType == enumPipelineDataType.Line)
                    {
                        qdgcIdx = feature.Fields.FindField(_layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                        qdmsIdx = feature.Fields.FindField(_layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                        zdgcIdx = feature.Fields.FindField(_layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                        zdmsIdx = feature.Fields.FindField(_layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
                    }
                    int i = 1;
                    while (feature != null)
                    {
                        if (_waitForm.Worker.CancellationPending)
                            break;
                        _waitForm.Worker.ReportProgress(i++, "状态:填充表单,请稍候...");
                        double num2 = 0.0;
                        string text2 = feature.Value[this.OidField].ToString();
                        if (feature.Shape == null || feature.Shape.IsEmpty)
                        {
                            feature = this._cursor.NextFeature();
                        }
                        else
                        {
                            int j;
                            for (j = 0; j < fields.FieldCount; j++)
                            {
                                if (j == this.OidField)
                                {
                                    array[0] = text2;
                                }
                                if (j == num)
                                {
                                    if (num > this.OidField)
                                    {
                                        array[j] = text;
                                    }
                                    else
                                    {
                                        array[j + 1] = text;
                                    }
                                    if (this.nGeoType == 1 || this.nGeoType == 3)
                                    {
                                        IPolyline polyline = (IPolyline)feature.Shape;
                                        IPointCollection pointCollection = (IPointCollection)polyline;
                                        IPoint point = null;
                                        IPoint point2 = null;
                                        if (isNUsing)
                                        {
                                            for (int k = 0; k < pointCollection.PointCount - 1; k++)
                                            {
                                                point = pointCollection.Point[k];
                                                array2[0] = text2;
                                                array2[1] = k + 1;
                                                array2[2] = point.X;
                                                array2[3] = point.Y;
                                                if (this.nGeoType == 3)
                                                {
                                                    array2[4] = point.Z;
                                                    array2[5] = 0;
                                                }
                                                else
                                                {
                                                    array2[4] = ((float)(point.Z - point.M)).ToString("f3");
                                                    array2[5] = point.M.ToString("f3");
                                                }
                                                this.Geotable.Rows.Add(array2);
                                                point2 = pointCollection.Point[k + 1];
                                                num2 +=
                                                    Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) +
                                                              Math.Pow(point.Y - point2.Y, 2.0) +
                                                              Math.Pow(point.Z - point.M - point2.Z + point2.M, 2.0));
                                            }
                                            array2[0] = text2;
                                            array2[1] = pointCollection.PointCount;
                                            array2[2] = point2.X;
                                            array2[3] = point2.Y;
                                            if (this.nGeoType == 3)
                                            {
                                                array2[4] = point2.Z;
                                                array2[5] = 0;
                                            }
                                            else
                                            {
                                                array2[4] = ((float)(point.Z - point.M)).ToString("f3");
                                                array2[5] = point2.M.ToString("f3");
                                            }
                                            this.Geotable.Rows.Add(array2);
                                        }
                                        else
                                        {
                                            point = pointCollection.Point[0];
                                            point2 = pointCollection.Point[pointCollection.PointCount - 1];
                                            array2[0] = text2;
                                            array2[1] = 1;
                                            array2[2] = point.X;
                                            array2[3] = point.Y;
                                            double height;
                                            array2[4] = qdgcIdx >= 0 ? GetDoubleValue(feature, qdgcIdx, out height) : 0;
                                            array2[5] = qdmsIdx >= 0 ? GetDoubleValue(feature, qdmsIdx, out height) : 0;
                                            this.Geotable.Rows.Add(array2);
                                            array2[0] = text2;
                                            array2[1] = 2;
                                            array2[2] = point2.X;
                                            array2[3] = point2.Y;

                                            array2[4] = zdgcIdx >= 0 ? GetDoubleValue(feature, zdgcIdx, out height) : 0;
                                            array2[5] = zdmsIdx >= 0 ? GetDoubleValue(feature, zdmsIdx, out height) : 0;
                                            this.Geotable.Rows.Add(array2);
                                        }
                                    }
                                    else if (this.nGeoType == 0)
                                    {
                                        IPoint point3 = (IPoint)feature.Shape;
                                        array2[0] = text2;
                                        array2[1] = point3.X;
                                        array2[2] = point3.Y;
                                        array2[3] = point3.Z;
                                        this.Geotable.Rows.Add(array2);
                                    }
                                    else if (this.nGeoType == 2 || this.nGeoType == 4)
                                    {
                                        IPolygon polygon = (IPolygon)feature.Shape;
                                        IPointCollection pointCollection2 = (IPointCollection)polygon;
                                        IPoint point4 = null;
                                        if (isNUsing)
                                        {
                                            for (int l = 0; l < pointCollection2.PointCount - 1; l++)
                                            {
                                                IPoint point5 = pointCollection2.Point[l];
                                                array2[0] = text2;
                                                array2[1] = l + 1;
                                                array2[2] = point5.X;
                                                array2[3] = point5.Y;
                                                array2[4] = point5.Z;
                                                array2[5] = point5.M;
                                                this.Geotable.Rows.Add(array2);
                                                point4 = pointCollection2.Point[l + 1];
                                                num2 +=
                                                    Math.Sqrt(Math.Pow(point5.X - point4.X, 2.0) +
                                                              Math.Pow(point5.Y - point4.Y, 2.0) +
                                                              Math.Pow(point5.Z - point5.M - point4.Z + point4.M, 2.0));
                                            }
                                            array2[0] = text2;
                                            array2[1] = pointCollection2.PointCount;
                                            array2[2] = point4.X;
                                            array2[3] = point4.Y;
                                            array2[4] = point4.Z;
                                            array2[5] = point4.M;
                                            this.Geotable.Rows.Add(array2);
                                        }
                                        else
                                        {
                                            IPoint point = pointCollection2.Point[0];
                                            IPoint point2 = pointCollection2.Point[pointCollection2.PointCount - 1];
                                            array2[0] = text2;
                                            array2[1] = 1;
                                            array2[2] = point.X;
                                            array2[3] = point.Y;
                                            double height;
                                            array2[4] = qdgcIdx >= 0 ? GetDoubleValue(feature, qdgcIdx, out height) : 0;
                                            array2[5] = qdmsIdx >= 0 ? GetDoubleValue(feature, qdmsIdx, out height) : 0;
                                            this.Geotable.Rows.Add(array2);
                                            array2[0] = text2;
                                            array2[1] = 2;
                                            array2[2] = point2.X;
                                            array2[3] = point2.Y;

                                            array2[4] = zdgcIdx >= 0 ? GetDoubleValue(feature, zdgcIdx, out height) : 0;
                                            array2[5] = zdmsIdx >= 0 ? GetDoubleValue(feature, zdmsIdx, out height) : 0;
                                            this.Geotable.Rows.Add(array2);
                                        }
                                    }
                                }
                                else
                                {
                                    IField field2 = feature.Fields.Field[j];
                                    object obj = feature.Value[j];
                                    if (j < this.OidField)
                                    {
                                        if (field2.Type == esriFieldType.esriFieldTypeDouble ||
                                            field2.Type == esriFieldType.esriFieldTypeSingle)
                                        {
                                            if (obj != DBNull.Value)
                                            {
                                                array[j + 1] = Math.Round(Convert.ToDouble(obj), 3);
                                            }
                                            else
                                            {
                                                array[j + 1] = obj;
                                            }
                                        }
                                        else if (field2.Type == esriFieldType.esriFieldTypeDate)
                                        {
                                            if (obj != DBNull.Value)
                                            {
                                                DateTime dateTime = Convert.ToDateTime(obj);
                                                array[j + 1] = dateTime.ToShortDateString();
                                            }
                                            else
                                            {
                                                array[j + 1] = "";
                                            }
                                        }
                                        else
                                        {
                                            array[j + 1] = obj;
                                        }
                                    }
                                    else if (field2.Type == esriFieldType.esriFieldTypeDouble ||
                                             field2.Type == esriFieldType.esriFieldTypeSingle)
                                    {
                                        if (obj != DBNull.Value)
                                        {
                                            array[j] = Math.Round(Convert.ToDouble(obj), 3);
                                        }
                                        else
                                        {
                                            array[j] = obj;
                                        }
                                    }
                                    else if (field2.Type == esriFieldType.esriFieldTypeDate)
                                    {
                                        if (obj != DBNull.Value)
                                        {
                                            array[j] = Convert.ToDateTime(obj).ToShortDateString();
                                        }
                                        else
                                        {
                                            array[j] = "";
                                        }
                                    }
                                    else
                                    {
                                        array[j] = obj;
                                    }
                                }
                            }
                            if (flag)
                            {
                                array[j] = num2.ToString(CultureInfo.InvariantCulture);
                            }
                            this.stable.Rows.Add(array);
                            this._selection.Add(feature);
                            feature = this._cursor.NextFeature();
                        }
                    }
                    this._selection.SelectionSet.Refresh();
                    IActiveView activeView = _context.ActiveView;
                    activeView.Refresh();
                    result = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private double GetDoubleValue(IFeature feature, int fldIdx, out double height)
        {
            double width = 0;
            height = 0;
            string str = "";
            try
            {
                if (fldIdx > 0)
                {
                    object value = feature.get_Value(fldIdx);
                    if (!Convert.IsDBNull(value))
                    {
                        str = Convert.ToString(value);
                    }
                    if ((str == null ? false : str.Length >= 1))
                    {
                        string[] strArrays = str.Split(this.char_widthheight);
                        if (strArrays[0].ToString().Trim() != "")
                        {
                            width = Convert.ToDouble(strArrays[0]);
                            if (strArrays.Length > 1)
                                height = Convert.ToDouble(strArrays[1]);
                        }
                        else
                        {
                            width = 0;
                            height = 0;
                        }
                    }
                }
                return width;
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }

        private void mainGridView_MasterRowGetLevelDefaultView(object sender,
            DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = this.detailGridView;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不转出EXCEL文件！");
            }
            else
            {
                this.saveFileDialog1.DefaultExt = "xls";
                this.saveFileDialog1.FileName = "Result.xls";
                this.saveFileDialog1.Filter = "Excel文件|*.xls";
                this.saveFileDialog1.OverwritePrompt = false;
                this.saveFileDialog1.Title = "保存";
                DialogResult dialogResult = this.saveFileDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    string fileName = this.saveFileDialog1.FileName;
                    if (!File.Exists(fileName))
                    {
                        DevExpress.XtraPrinting.XlsExportOptions options =
                            new DevExpress.XtraPrinting.XlsExportOptions();
                        gridControl1.ExportToXls(fileName);
                        DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("该文件已存在,返回!");
                    }
                }
            }
        }

        private void btnStatics_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不进行统计！");
            }
            else if (this.cmbStatField.SelectedIndex < 0)
            {
                MessageBox.Show("请用户先指定分类项！");
            }
            else
            {
                //new StatForm
                //{
                //    Owner = _context.MainView as Form,
                //    Form_StatField = this.cmbStatField.SelectedItem.ToString(),
                //    Form_StatWay = this.cmbStatWay.SelectedItem.ToString(),
                //    Form_CalField = this.cmbCalField.SelectedItem.ToString(),
                //    resultTable = this.stable
                //}.ShowDialog();
            }
        }

        private void cmbStatWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bControlEvent)
            {
                if (this.cmbStatWay.SelectedItem.ToString() == "计数" || this.cmbStatWay.SelectedItem.ToString() == "统计长度" ||
                    this.cmbStatWay.SelectedItem.ToString() == "统计面积")
                {
                    this.cmbCalField.Enabled = false;
                }
                else
                {
                    this.cmbCalField.Enabled = true;
                }
            }
        }

        private void cmbStatField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bControlEvent && this.stable.Rows.Count > 0)
            {
                this.mainGridView.ClearSorting();
                this.mainGridView.Columns[this.cmbStatField.SelectedItem.ToString()].SortMode = ColumnSortMode.Default;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不进行打印！");
            }
            else
            {
                try
                {
                    this.gridControl1.PrintDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while printing.\n" + ex.Message, "Error printing",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcSelectFile : UserControl
    {
        private string _filter;
        private string _fileName;
        private IWorkspace _workspace;

        public UcSelectFile()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("过滤"), Category("扩展"), DefaultValue(true), TypeConverter(typeof(FilterConverter))]
        public string Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public string FileName => _fileName;

        public IWorkspace Workspace => _workspace;



        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckGxObjectFilter(_filter))
            {
                _workspace = GxDialogHelper.SelectWorkspaceDialog();
                if (_workspace != null)
                {
                    _fileName = _workspace.PathName;
                    txtPath.Text = _fileName;
                }
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Title = @"请选择文件";
                dialog.Filter = _filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _fileName = dialog.FileName;
                    txtPath.Text = _fileName;
                }
            }
        }

        private bool CheckGxObjectFilter(string filter)
        {
            if (filter.ToUpper().Contains("个人地理数据库|*.mdb"))
            {
                return true;
            }
            if (filter.ToUpper().Contains("文件地理数据库|*.gdb"))
            {
                return true;
            }
            return false;
        }
    }

    public class FilterConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> filters = new List<string>();
            filters.Add("文本文件|*.txt");
            filters.Add("Excel文件|*.xlsx;*.xls");
            filters.Add("所有文件|*.*");
            filters.Add("个人地理数据库|*.mdb");
            filters.Add("文件地理数据库|*.gdb");

            return new StandardValuesCollection(filters);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}

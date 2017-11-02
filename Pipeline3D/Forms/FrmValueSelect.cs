using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Pipeline3D.Forms
{
    public partial class FrmValueSelect : Form
    {
        private BindingList<string> _allFsw;
        private BindingList<string> _selectedFsw;

        public FrmValueSelect(List<string> allFsw, List<string> selectedFsw)
        {
            InitializeComponent();
            _allFsw = new BindingList<string>(allFsw);
            _selectedFsw = new BindingList<string>(selectedFsw);

            this.listBoxFsw.DataSource = _allFsw;
            this.listBoxSelFsw.DataSource = _selectedFsw;
        }

        public BindingList<string> AllFsw
        {
            get { return _allFsw; }
            set { _allFsw = value; }
        }

        public BindingList<string> SelectedFsw
        {
            get { return _selectedFsw; }
            set { _selectedFsw = value; }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _allFsw.Count; i = 0)
            {
                string value = _allFsw[i];
                _allFsw.RemoveAt(i);
                _selectedFsw.Add(value);
            }
        }

        private void btnSelectSingle_Click(object sender, EventArgs e)
        {
            if (listBoxFsw.SelectedItem == null)
                return;
            string value = listBoxFsw.SelectedItem.ToString();
            if (_allFsw.Remove(value))
                _selectedFsw.Add(value);

        }

        private void btnUnSelectSingle_Click(object sender, EventArgs e)
        {
            if (listBoxSelFsw.SelectedItem == null)
                return;
            string value = listBoxSelFsw.SelectedItem.ToString();
            if (_selectedFsw.Remove(value))
                _allFsw.Add(value);
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _selectedFsw.Count; i = 0)
            {
                string value = _selectedFsw[i];
                _selectedFsw.RemoveAt(i);
                _allFsw.Add(value);
            }
        }

        public List<string> GetSelectedFsw()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < _selectedFsw.Count; i++)
            {
                string value = _selectedFsw[i];
                list.Add(value);
            }
            return list;
        }

        private void listBoxFsw_DoubleClick(object sender, EventArgs e)
        {
            btnSelectSingle_Click(null, null);
        }

        private void listBoxSelFsw_DoubleClick(object sender, EventArgs e)
        {
            btnUnSelectSingle_Click(null, null);
        }
    }
}

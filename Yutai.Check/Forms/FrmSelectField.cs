using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Check.Classes;
using Yutai.Pipeline.Config.Helpers;

namespace Yutai.Check.Forms
{
    public partial class FrmSelectField : Form, ISelectField
    {
        public FrmSelectField()
        {
            InitializeComponent();
            GetFields();
        }

        private void GetFields()
        {
            List<string> list = new List<string>();

            if (radioButtonPoint.Checked)
            {
                Type words = PipeConfigWordHelper.PointWords.GetType();
                foreach (PropertyInfo propertyInfo in words.GetProperties())
                {
                    list.Add(propertyInfo.GetValue(PipeConfigWordHelper.PointWords).ToString());
                }
            }
            else
            {
                Type words = PipeConfigWordHelper.LineWords.GetType();
                foreach (PropertyInfo propertyInfo in words.GetProperties())
                {
                    list.Add(propertyInfo.GetValue(PipeConfigWordHelper.LineWords).ToString());
                }
            }

            comboBoxField.DataSource = list;
        }


        public string FieldName
        {
            get
            {
                if (comboBoxField.SelectedItem != null)
                    return comboBoxField.SelectedItem.ToString();
                return null;
            }
        }

        private void radioButtonPoint_CheckedChanged(object sender, EventArgs e)
        {
            GetFields();
        }
    }
}

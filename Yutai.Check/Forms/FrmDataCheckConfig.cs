using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Check.Classes;
using Yutai.Check.Enums;

namespace Yutai.Check.Forms
{
    public partial class FrmDataCheckConfig : Form, IDataCheckConfig
    {
        public FrmDataCheckConfig()
        {
            InitializeComponent();
        }

        public double SurfaceTolerance
        {
            get { return (double)numSurfaceTolerance.Value; }
            set { numSurfaceTolerance.Value = (decimal)value; }
        }

        public double ElevationTolerance
        {
            get { return (double)numElevationTolerance.Value; }
            set { numElevationTolerance.Value = (decimal)value; }
        }

        public double CompareRadius
        {
            get { return (double)numCompareRadius.Value; }
            set { numCompareRadius.Value = (decimal)value; }
        }

        public double CompareLimit
        {
            get { return (double)numCompareLimit.Value; }
            set { numCompareLimit.Value = (decimal)value; }
        }

        public EnumElevationCheckType ElevationCheckType
        {
            get
            {
                return (EnumElevationCheckType)Enum.Parse(typeof(EnumElevationCheckType), radioGroupElevationCheckType.EditValue.ToString());
            }
            set
            {
                radioGroupElevationCheckType.EditValue = Enum.GetName(typeof(EnumElevationCheckType), value);
            }
        }

        public string ZxValue
        {
            get { return textBoxZX.Text; }
            set { textBoxZX.Text = value; }
        }

        public string NxValue
        {
            get { return textBoxNX.Text; }
            set { textBoxNX.Text = value; }
        }
    }
}

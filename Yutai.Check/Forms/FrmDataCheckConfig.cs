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
            get { return (double) numSurfaceTolerance.Value; }
            set { numSurfaceTolerance.Value = (decimal) value; }
        }

        public double ElevationTolerance
        {
            get { return (double) numElevationTolerance.Value; }
            set { numElevationTolerance.Value = (decimal) value; }
        }
    }
}

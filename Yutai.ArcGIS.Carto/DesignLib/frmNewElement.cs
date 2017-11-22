using System.ComponentModel;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class frmNewElement : Form
    {
        private IContainer components = null;

        public frmNewElement()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "frmNewElement";
        }
    }
}
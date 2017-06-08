﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class frmFlowArrowSet : Form
    {
        private Container components = null;
        private FlowArrorPropertyPage flowArrorPropertyPage1;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabPage1;

        public frmFlowArrowSet()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlowArrowSet));
            this.panel1 = new Panel();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.flowArrorPropertyPage1 = new FlowArrorPropertyPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xe9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x124, 40);
            this.panel1.TabIndex = 1;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x124, 0xe9);
            this.tabControl1.TabIndex = 2;
            this.tabPage1.Controls.Add(this.flowArrorPropertyPage1);
            this.tabPage1.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0x11c, 0xd0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "箭头符号";
            this.flowArrorPropertyPage1.Dock = DockStyle.Fill;
            this.flowArrorPropertyPage1.Location = new System.Drawing.Point(0, 0);
            this.flowArrorPropertyPage1.Name = "flowArrorPropertyPage1";
            this.flowArrorPropertyPage1.Size = new Size(0x11c, 0xd0);
            this.flowArrorPropertyPage1.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmFlowArrowSet";
            this.Text = "流向属性";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmPageLayoutCtrl
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageLayoutCtrl));
            this.axPageLayoutControl1 = new AxPageLayoutControl();
            this.axPageLayoutControl1.BeginInit();
            base.SuspendLayout();
            this.axPageLayoutControl1.Dock = DockStyle.Fill;
            this.axPageLayoutControl1.Location = new Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl1.OcxState");
            this.axPageLayoutControl1.Size = new Size(292, 271);
            this.axPageLayoutControl1.TabIndex = 0;
            base.ClientSize = new Size(292, 271);
            base.Controls.Add(this.axPageLayoutControl1);
            base.DockAreas = DockAreas.Document;
            base.Name = "frmPageLayoutCtrl";
            base.ShowHint = DockState.Document;
            base.Load += new EventHandler(this.frmPageLayoutCtrl_Load);
            this.axPageLayoutControl1.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private AxPageLayoutControl axPageLayoutControl1;
    }
}
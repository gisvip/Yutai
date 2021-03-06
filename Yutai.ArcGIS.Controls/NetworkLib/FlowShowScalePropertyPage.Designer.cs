﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    partial class FlowShowScalePropertyPage
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
            this.rdoNoScale = new RadioButton();
            this.rdoScaleSet = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtMinScale = new TextBox();
            this.txtMaxScale = new TextBox();
            base.SuspendLayout();
            this.rdoNoScale.Checked = true;
            this.rdoNoScale.Location = new System.Drawing.Point(16, 24);
            this.rdoNoScale.Name = "rdoNoScale";
            this.rdoNoScale.Size = new Size(136, 24);
            this.rdoNoScale.TabIndex = 0;
            this.rdoNoScale.TabStop = true;
            this.rdoNoScale.Text = "所有比例尺均显示";
            this.rdoNoScale.CheckedChanged += new EventHandler(this.rdoNoScale_CheckedChanged);
            this.rdoScaleSet.Location = new System.Drawing.Point(16, 48);
            this.rdoScaleSet.Name = "rdoScaleSet";
            this.rdoScaleSet.Size = new Size(120, 24);
            this.rdoScaleSet.TabIndex = 1;
            this.rdoScaleSet.Text = "不显示箭头，当";
            this.rdoScaleSet.CheckedChanged += new EventHandler(this.rdoScaleSet_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 90);
            this.label1.Name = "label1";
            this.label1.Size = new Size(66, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "缩小超过1:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 122);
            this.label2.Name = "label2";
            this.label2.Size = new Size(66, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "放大超过1:";
            this.txtMinScale.Enabled = false;
            this.txtMinScale.Location = new System.Drawing.Point(88, 88);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(88, 21);
            this.txtMinScale.TabIndex = 4;
            this.txtMinScale.Text = "";
            this.txtMaxScale.Enabled = false;
            this.txtMaxScale.Location = new System.Drawing.Point(88, 120);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(88, 21);
            this.txtMaxScale.TabIndex = 5;
            this.txtMaxScale.Text = "";
            this.txtMaxScale.TextChanged += new EventHandler(this.txtMaxScale_TextChanged);
            base.Controls.Add(this.txtMaxScale);
            base.Controls.Add(this.txtMinScale);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoScaleSet);
            base.Controls.Add(this.rdoNoScale);
            base.Name = "FlowShowScalePropertyPage";
            base.Size = new Size(216, 208);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Label label1;
        private Label label2;
        private RadioButton rdoNoScale;
        private RadioButton rdoScaleSet;
        private TextBox txtMaxScale;
        private TextBox txtMinScale;
    }
}
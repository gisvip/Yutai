﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class AnnoReferenceScaleSetCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboMapUnit = new ComboBoxEdit();
            this.txtScale = new TextEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.cboMapUnit.Properties.BeginInit();
            this.txtScale.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboMapUnit);
            this.groupBox1.Controls.Add(this.txtScale);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参考比例";
            this.cboMapUnit.EditValue = "未知单位";
            this.cboMapUnit.Location = new Point(80, 128);
            this.cboMapUnit.Name = "cboMapUnit";
            this.cboMapUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMapUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "度", "分米" });
            this.cboMapUnit.Size = new Size(168, 23);
            this.cboMapUnit.TabIndex = 5;
            this.txtScale.EditValue = "0";
            this.txtScale.Location = new Point(80, 96);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(168, 23);
            this.txtScale.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 128);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "地图单位:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new Size(66, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "参考比例1:";
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(288, 48);
            this.label2.TabIndex = 1;
            this.label2.Text = "如果当前比例尺大于参考比例尺，注记将显得更大，如果当前比例尺小于参考比例尺，注记将显得较小";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(116, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "为注记设定参考比例";
            base.Controls.Add(this.groupBox1);
            base.Name = "AnnoReferenceScaleSetCtrl";
            base.Size = new Size(336, 248);
            base.Load += new EventHandler(this.AnnoReferenceScaleSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboMapUnit.Properties.EndInit();
            this.txtScale.Properties.EndInit();
            base.ResumeLayout(false);
        }
    
        private ComboBoxEdit cboMapUnit;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtScale;
    }
}
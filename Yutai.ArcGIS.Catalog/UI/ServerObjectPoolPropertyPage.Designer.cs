﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class ServerObjectPoolPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtMaxInstances1 = new TextEdit();
            this.label3 = new Label();
            this.txtMaxInstances = new TextEdit();
            this.txtMinInstances = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.rdoIsPooled = new RadioGroup();
            this.txtMaxUsageTime = new TextEdit();
            this.label4 = new Label();
            this.txtMaxWaitTime = new TextEdit();
            this.label5 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtMaxInstances1.Properties.BeginInit();
            this.txtMaxInstances.Properties.BeginInit();
            this.txtMinInstances.Properties.BeginInit();
            this.rdoIsPooled.Properties.BeginInit();
            this.txtMaxUsageTime.Properties.BeginInit();
            this.txtMaxWaitTime.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtMaxInstances1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMaxInstances);
            this.groupBox1.Controls.Add(this.txtMinInstances);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoIsPooled);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(336, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.txtMaxInstances1.EditValue = "4";
            this.txtMaxInstances1.Location = new Point(120, 130);
            this.txtMaxInstances1.Name = "txtMaxInstances1";
            this.txtMaxInstances1.Size = new Size(88, 23);
            this.txtMaxInstances1.TabIndex = 6;
            this.txtMaxInstances1.EditValueChanged += new EventHandler(this.txtMaxInstances1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(32, 134);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "最可用实例数:";
            this.txtMaxInstances.EditValue = "4";
            this.txtMaxInstances.Location = new Point(120, 86);
            this.txtMaxInstances.Name = "txtMaxInstances";
            this.txtMaxInstances.Size = new Size(88, 23);
            this.txtMaxInstances.TabIndex = 4;
            this.txtMaxInstances.EditValueChanged += new EventHandler(this.txtMaxInstances_EditValueChanged);
            this.txtMinInstances.EditValue = "2";
            this.txtMinInstances.Location = new Point(120, 54);
            this.txtMinInstances.Name = "txtMinInstances";
            this.txtMinInstances.Size = new Size(88, 23);
            this.txtMinInstances.TabIndex = 3;
            this.txtMinInstances.EditValueChanged += new EventHandler(this.txtMinInstances_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(32, 86);
            this.label2.Name = "label2";
            this.label2.Size = new Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "最大实例数:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(32, 62);
            this.label1.Name = "label1";
            this.label1.Size = new Size(72, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "最小实例数:";
            this.rdoIsPooled.Location = new Point(8, 14);
            this.rdoIsPooled.Name = "rdoIsPooled";
            this.rdoIsPooled.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoIsPooled.Properties.Appearance.Options.UseBackColor = true;
            this.rdoIsPooled.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoIsPooled.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "缓冲"), new RadioGroupItem(null, "无缓冲") });
            this.rdoIsPooled.Size = new Size(168, 136);
            this.rdoIsPooled.TabIndex = 0;
            this.rdoIsPooled.SelectedIndexChanged += new EventHandler(this.rdoIsPooled_SelectedIndexChanged);
            this.txtMaxUsageTime.EditValue = "600";
            this.txtMaxUsageTime.Location = new Point(216, 184);
            this.txtMaxUsageTime.Name = "txtMaxUsageTime";
            this.txtMaxUsageTime.Size = new Size(88, 23);
            this.txtMaxUsageTime.TabIndex = 8;
            this.txtMaxUsageTime.EditValueChanged += new EventHandler(this.txtMaxUsageTime_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(40, 192);
            this.label4.Name = "label4";
            this.label4.Size = new Size(171, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server Objects最大使用时间:";
            this.txtMaxWaitTime.EditValue = "60";
            this.txtMaxWaitTime.Location = new Point(216, 216);
            this.txtMaxWaitTime.Name = "txtMaxWaitTime";
            this.txtMaxWaitTime.Size = new Size(88, 23);
            this.txtMaxWaitTime.TabIndex = 10;
            this.txtMaxWaitTime.EditValueChanged += new EventHandler(this.txtMaxWaitTime_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(40, 224);
            this.label5.Name = "label5";
            this.label5.Size = new Size(171, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Server Objects最大等待时间:";
            base.Controls.Add(this.txtMaxWaitTime);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtMaxUsageTime);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.groupBox1);
            base.Name = "ServerObjectPoolPropertyPage";
            base.Size = new Size(400, 272);
            base.Load += new EventHandler(this.ServerObjectPoolPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtMaxInstances1.Properties.EndInit();
            this.txtMaxInstances.Properties.EndInit();
            this.txtMinInstances.Properties.EndInit();
            this.rdoIsPooled.Properties.EndInit();
            this.txtMaxUsageTime.Properties.EndInit();
            this.txtMaxWaitTime.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private RadioGroup rdoIsPooled;
        private TextEdit txtMaxInstances;
        private TextEdit txtMaxInstances1;
        private TextEdit txtMaxUsageTime;
        private TextEdit txtMaxWaitTime;
        private TextEdit txtMinInstances;
    }
}
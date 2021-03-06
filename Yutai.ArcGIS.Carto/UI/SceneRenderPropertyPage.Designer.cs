﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class SceneRenderPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtRenderRefreshRate = new NumericUpDown();
            this.label2 = new Label();
            this.label1 = new Label();
            this.rdoRenderWhenNavigating = new RadioButton();
            this.rdoRenderWhenStopped = new RadioButton();
            this.rdoRenderAlways = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.chkSmoothShading = new CheckBox();
            this.chkIlluminate = new CheckBox();
            this.cboDepthPriorityValue = new ComboBox();
            this.label3 = new Label();
            this.groupBox3 = new GroupBox();
            this.trackBar1 = new TrackBar();
            this.checkBox3 = new CheckBox();
            this.checkBox4 = new CheckBox();
            this.label4 = new Label();
            this.rdordoRenderCache = new RadioButton();
            this.rdoRenderImmediate = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.txtRenderRefreshRate.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.trackBar1.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtRenderRefreshRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoRenderWhenNavigating);
            this.groupBox1.Controls.Add(this.rdoRenderWhenStopped);
            this.groupBox1.Controls.Add(this.rdoRenderAlways);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(385, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "可见性";
            int[] bits = new int[4];
            bits[0] = 1;
            bits[3] = 65536;
            this.txtRenderRefreshRate.Increment = new decimal(bits);
            this.txtRenderRefreshRate.Location = new Point(280, 90);
            this.txtRenderRefreshRate.Name = "txtRenderRefreshRate";
            this.txtRenderRefreshRate.Size = new Size(63, 21);
            this.txtRenderRefreshRate.TabIndex = 5;
            int[] bits2 = new int[4];
            bits2[0] = 75;
            bits2[3] = 131072;
            this.txtRenderRefreshRate.Value = new decimal(bits2);
            this.txtRenderRefreshRate.ValueChanged += new EventHandler(this.txtRenderRefreshRate_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(349, 92);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "秒";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 90);
            this.label1.Name = "label1";
            this.label1.Size = new Size(257, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "导航时刷新频率大于以下数字时，简单绘制图层";
            this.rdoRenderWhenNavigating.AutoSize = true;
            this.rdoRenderWhenNavigating.Location = new Point(10, 66);
            this.rdoRenderWhenNavigating.Name = "rdoRenderWhenNavigating";
            this.rdoRenderWhenNavigating.Size = new Size(107, 16);
            this.rdoRenderWhenNavigating.TabIndex = 2;
            this.rdoRenderWhenNavigating.Text = "导航时渲染图层";
            this.rdoRenderWhenNavigating.UseVisualStyleBackColor = true;
            this.rdoRenderWhenNavigating.CheckedChanged += new EventHandler(this.rdoRenderWhenNavigating_CheckedChanged);
            this.rdoRenderWhenStopped.AutoSize = true;
            this.rdoRenderWhenStopped.Location = new Point(6, 44);
            this.rdoRenderWhenStopped.Name = "rdoRenderWhenStopped";
            this.rdoRenderWhenStopped.Size = new Size(131, 16);
            this.rdoRenderWhenStopped.TabIndex = 1;
            this.rdoRenderWhenStopped.Text = "导航停止时渲染图层";
            this.rdoRenderWhenStopped.UseVisualStyleBackColor = true;
            this.rdoRenderWhenStopped.CheckedChanged += new EventHandler(this.rdoRenderWhenStopped_CheckedChanged);
            this.rdoRenderAlways.AutoSize = true;
            this.rdoRenderAlways.Checked = true;
            this.rdoRenderAlways.Location = new Point(6, 22);
            this.rdoRenderAlways.Name = "rdoRenderAlways";
            this.rdoRenderAlways.Size = new Size(107, 16);
            this.rdoRenderAlways.TabIndex = 0;
            this.rdoRenderAlways.TabStop = true;
            this.rdoRenderAlways.Text = "始终渲染该图层";
            this.rdoRenderAlways.UseVisualStyleBackColor = true;
            this.rdoRenderAlways.CheckedChanged += new EventHandler(this.rdoRenderAlways_CheckedChanged);
            this.groupBox2.Controls.Add(this.chkSmoothShading);
            this.groupBox2.Controls.Add(this.chkIlluminate);
            this.groupBox2.Controls.Add(this.cboDepthPriorityValue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new Point(10, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(381, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "效果";
            this.chkSmoothShading.AutoSize = true;
            this.chkSmoothShading.Location = new Point(6, 42);
            this.chkSmoothShading.Name = "chkSmoothShading";
            this.chkSmoothShading.Size = new Size(96, 16);
            this.chkSmoothShading.TabIndex = 3;
            this.chkSmoothShading.Text = "使用平滑阴影";
            this.chkSmoothShading.UseVisualStyleBackColor = true;
            this.chkSmoothShading.CheckedChanged += new EventHandler(this.chkSmoothShading_CheckedChanged);
            this.chkIlluminate.AutoSize = true;
            this.chkIlluminate.Location = new Point(6, 20);
            this.chkIlluminate.Name = "chkIlluminate";
            this.chkIlluminate.Size = new Size(180, 16);
            this.chkIlluminate.TabIndex = 2;
            this.chkIlluminate.Text = "根据屏幕光源显示要素阴影区";
            this.chkIlluminate.UseVisualStyleBackColor = true;
            this.chkIlluminate.CheckedChanged += new EventHandler(this.chkIlluminate_CheckedChanged);
            this.cboDepthPriorityValue.FormattingEnabled = true;
            this.cboDepthPriorityValue.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            this.cboDepthPriorityValue.Location = new Point(134, 64);
            this.cboDepthPriorityValue.Name = "cboDepthPriorityValue";
            this.cboDepthPriorityValue.Size = new Size(121, 20);
            this.cboDepthPriorityValue.TabIndex = 1;
            this.cboDepthPriorityValue.SelectedIndexChanged += new EventHandler(this.cboDepthPriorityValue_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new Size(113, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "设置阴影绘制优先级";
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rdordoRenderCache);
            this.groupBox3.Controls.Add(this.rdoRenderImmediate);
            this.groupBox3.Location = new Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(381, 77);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "优化";
            this.trackBar1.Location = new Point(166, 103);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new Size(104, 45);
            this.trackBar1.TabIndex = 7;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new Point(13, 87);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new Size(96, 16);
            this.checkBox3.TabIndex = 6;
            this.checkBox3.Text = "禁用材质纹理";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new Point(115, 87);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new Size(120, 16);
            this.checkBox4.TabIndex = 5;
            this.checkBox4.Text = "启用压缩纹理渲染";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(13, 116);
            this.label4.Name = "label4";
            this.label4.Size = new Size(77, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "栅格图像品质";
            this.rdordoRenderCache.AutoSize = true;
            this.rdordoRenderCache.Location = new Point(7, 43);
            this.rdordoRenderCache.Name = "rdordoRenderCache";
            this.rdordoRenderCache.Size = new Size(101, 16);
            this.rdordoRenderCache.TabIndex = 1;
            this.rdordoRenderCache.Text = "使用图层Cache";
            this.rdordoRenderCache.UseVisualStyleBackColor = true;
            this.rdordoRenderCache.CheckedChanged += new EventHandler(this.rdordoRenderCache_CheckedChanged);
            this.rdoRenderImmediate.AutoSize = true;
            this.rdoRenderImmediate.Checked = true;
            this.rdoRenderImmediate.Location = new Point(7, 21);
            this.rdoRenderImmediate.Name = "rdoRenderImmediate";
            this.rdoRenderImmediate.Size = new Size(71, 16);
            this.rdoRenderImmediate.TabIndex = 0;
            this.rdoRenderImmediate.TabStop = true;
            this.rdoRenderImmediate.Text = "直接渲染";
            this.rdoRenderImmediate.UseVisualStyleBackColor = true;
            this.rdoRenderImmediate.CheckedChanged += new EventHandler(this.rdoRenderImmediate_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "SceneRenderPropertyPage";
            base.Size = new Size(457, 326);
            base.Load += new EventHandler(this.SceneRenderPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtRenderRefreshRate.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.trackBar1.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBox cboDepthPriorityValue;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox chkIlluminate;
        private CheckBox chkSmoothShading;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioButton rdordoRenderCache;
        private RadioButton rdoRenderAlways;
        private RadioButton rdoRenderImmediate;
        private RadioButton rdoRenderWhenNavigating;
        private RadioButton rdoRenderWhenStopped;
        private TrackBar trackBar1;
        private NumericUpDown txtRenderRefreshRate;
    }
}
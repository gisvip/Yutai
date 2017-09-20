using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class frmAttributeQueryBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSelectType = new System.Windows.Forms.ComboBox();
            this.chkShowSelectbaleLayer = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkZoomToSelect = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbMap);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboSelectType);
            this.panel1.Controls.Add(this.chkShowSelectbaleLayer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxLayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 120);
            this.panel1.TabIndex = 7;
            // 
            // cmbMap
            // 
            this.cmbMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMap.Items.AddRange(new object[] {
            "二维地图",
            "三维地图"});
            this.cmbMap.Location = new System.Drawing.Point(56, 6);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(373, 22);
            this.cmbMap.TabIndex = 53;
            this.cmbMap.SelectedIndexChanged += new System.EventHandler(this.cmbMap_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 52;
            this.label2.Text = "地图:";
            // 
            // cboSelectType
            // 
            this.cboSelectType.Items.AddRange(new object[] {
            "创建一个新的选择集",
            "添加到现有选择集",
            "从现有选择集中删除",
            "从现有选择集中选择"});
            this.cboSelectType.Location = new System.Drawing.Point(56, 88);
            this.cboSelectType.Name = "cboSelectType";
            this.cboSelectType.Size = new System.Drawing.Size(373, 22);
            this.cboSelectType.TabIndex = 51;
            this.cboSelectType.Text = "创建一个新的选择集";
            this.cboSelectType.SelectedIndexChanged += new System.EventHandler(this.cboSelectType_SelectedIndexChanged);
            // 
            // chkShowSelectbaleLayer
            // 
            this.chkShowSelectbaleLayer.Location = new System.Drawing.Point(56, 62);
            this.chkShowSelectbaleLayer.Name = "chkShowSelectbaleLayer";
            this.chkShowSelectbaleLayer.Size = new System.Drawing.Size(168, 20);
            this.chkShowSelectbaleLayer.TabIndex = 50;
            this.chkShowSelectbaleLayer.Text = "只显示可选择图层";
            this.chkShowSelectbaleLayer.Click += new System.EventHandler(this.chkShowSelectbaleLayer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 49;
            this.label3.Text = "方法:";
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.Location = new System.Drawing.Point(56, 35);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(373, 22);
            this.comboBoxLayer.TabIndex = 48;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 47;
            this.label1.Text = "图层:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 413);
            this.panel2.TabIndex = 52;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkZoomToSelect);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnApply);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 533);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 64);
            this.panel3.TabIndex = 53;
            // 
            // chkZoomToSelect
            // 
            this.chkZoomToSelect.AutoSize = true;
            this.chkZoomToSelect.Location = new System.Drawing.Point(12, 3);
            this.chkZoomToSelect.Name = "chkZoomToSelect";
            this.chkZoomToSelect.Size = new System.Drawing.Size(110, 18);
            this.chkZoomToSelect.TabIndex = 57;
            this.chkZoomToSelect.Text = "缩放到选中要素";
            this.chkZoomToSelect.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(8, 27);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 26);
            this.btnClear.TabIndex = 56;
            this.btnClear.Text = "清除";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(296, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 26);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "关闭";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(232, 27);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(56, 26);
            this.btnApply.TabIndex = 54;
            this.btnApply.Text = "应用";
            // 
            // frmAttributeQueryBuilder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(441, 597);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAttributeQueryBuilder";
            this.ShowInTaskbar = false;
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.frmAttributeQueryBuilder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion



        private Panel panel1;

        private ComboBox cboSelectType;

        private CheckBox chkShowSelectbaleLayer;

        private Label label3;

        private ComboBox comboBoxLayer;

        private Label label1;

        private Panel panel2;
        private ComboBox cmbMap;
        private Label label2;
        private Panel panel3;
        private CheckBox chkZoomToSelect;
        private Button btnClear;
        private Button btnClose;
        private Button btnApply;
    }
}

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleQueryByAddressUI
    {
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
            this.CloseBut = new System.Windows.Forms.Button();
            this.QueryBut = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.LayerBox = new System.Windows.Forms.ComboBox();
            this.lable = new System.Windows.Forms.Label();
            this.BlurCheck = new System.Windows.Forms.CheckBox();
            this.FieldBox = new System.Windows.Forms.TextBox();
            this.SqlBox = new System.Windows.Forms.TextBox();
            this.RevBut = new System.Windows.Forms.Button();
            this.NoneBut = new System.Windows.Forms.Button();
            this.AllBut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ValueBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBut
            // 
            this.CloseBut.Location = new System.Drawing.Point(327, 41);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(75, 23);
            this.CloseBut.TabIndex = 27;
            this.CloseBut.Text = "关闭";
            this.CloseBut.UseVisualStyleBackColor = true;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            // 
            // QueryBut
            // 
            this.QueryBut.Location = new System.Drawing.Point(327, 12);
            this.QueryBut.Name = "QueryBut";
            this.QueryBut.Size = new System.Drawing.Size(75, 23);
            this.QueryBut.TabIndex = 26;
            this.QueryBut.Text = "查询";
            this.QueryBut.UseVisualStyleBackColor = true;
            this.QueryBut.Click += new System.EventHandler(this.QueryBut_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(142, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 25;
            this.radioButton2.Text = "管线层";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(23, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "管点层";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // LayerBox
            // 
            this.LayerBox.FormattingEnabled = true;
            this.LayerBox.Location = new System.Drawing.Point(43, 35);
            this.LayerBox.Name = "LayerBox";
            this.LayerBox.Size = new System.Drawing.Size(148, 20);
            this.LayerBox.TabIndex = 23;
            this.LayerBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_SelectedIndexChanged);
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Location = new System.Drawing.Point(12, 39);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(29, 12);
            this.lable.TabIndex = 22;
            this.lable.Text = "图层";
            // 
            // BlurCheck
            // 
            this.BlurCheck.AutoSize = true;
            this.BlurCheck.Location = new System.Drawing.Point(332, 278);
            this.BlurCheck.Name = "BlurCheck";
            this.BlurCheck.Size = new System.Drawing.Size(72, 16);
            this.BlurCheck.TabIndex = 28;
            this.BlurCheck.Text = "模糊查询";
            this.BlurCheck.UseVisualStyleBackColor = true;
            this.BlurCheck.CheckedChanged += new System.EventHandler(this.BlurCheck_CheckedChanged);
            // 
            // FieldBox
            // 
            this.FieldBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FieldBox.Location = new System.Drawing.Point(15, 73);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.ReadOnly = true;
            this.FieldBox.Size = new System.Drawing.Size(55, 14);
            this.FieldBox.TabIndex = 29;
            this.FieldBox.Text = "FieldBox";
            // 
            // SqlBox
            // 
            this.SqlBox.Location = new System.Drawing.Point(76, 66);
            this.SqlBox.Name = "SqlBox";
            this.SqlBox.ReadOnly = true;
            this.SqlBox.Size = new System.Drawing.Size(222, 21);
            this.SqlBox.TabIndex = 30;
            // 
            // RevBut
            // 
            this.RevBut.Location = new System.Drawing.Point(208, 140);
            this.RevBut.Name = "RevBut";
            this.RevBut.Size = new System.Drawing.Size(76, 23);
            this.RevBut.TabIndex = 3;
            this.RevBut.Text = "反选(&I)";
            this.RevBut.UseVisualStyleBackColor = true;
            this.RevBut.Click += new System.EventHandler(this.RevBut_Click);
            // 
            // NoneBut
            // 
            this.NoneBut.Location = new System.Drawing.Point(208, 95);
            this.NoneBut.Name = "NoneBut";
            this.NoneBut.Size = new System.Drawing.Size(76, 23);
            this.NoneBut.TabIndex = 2;
            this.NoneBut.Text = "全不选(&N)";
            this.NoneBut.UseVisualStyleBackColor = true;
            this.NoneBut.Click += new System.EventHandler(this.NoneBut_Click);
            // 
            // AllBut
            // 
            this.AllBut.Location = new System.Drawing.Point(208, 50);
            this.AllBut.Name = "AllBut";
            this.AllBut.Size = new System.Drawing.Size(76, 23);
            this.AllBut.TabIndex = 1;
            this.AllBut.Text = "全选(&A)";
            this.AllBut.UseVisualStyleBackColor = true;
            this.AllBut.Click += new System.EventHandler(this.AllBut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RevBut);
            this.groupBox2.Controls.Add(this.NoneBut);
            this.groupBox2.Controls.Add(this.AllBut);
            this.groupBox2.Controls.Add(this.ValueBox);
            this.groupBox2.Location = new System.Drawing.Point(14, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 191);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择查询对象";
            // 
            // ValueBox
            // 
            this.ValueBox.CheckOnClick = true;
            this.ValueBox.FormattingEnabled = true;
            this.ValueBox.Location = new System.Drawing.Point(10, 17);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(166, 164);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 0;
            this.ValueBox.SelectedIndexChanged += new System.EventHandler(this.ValueBox_SelectedIndexChanged);
            // 
            // SimpleQueryByAddressUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 306);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SqlBox);
            this.Controls.Add(this.FieldBox);
            this.Controls.Add(this.BlurCheck);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.QueryBut);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.LayerBox);
            this.Controls.Add(this.lable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SimpleQueryByAddressUI";
            this.Text = "快速查询－按地址";
            this.Load += new System.EventHandler(this.SimpleQueryByAddressUI_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SimpleQueryByAddressUI_HelpRequested);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private QueryResult resultDlg;
		private Button CloseBut;
		private Button QueryBut;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private ComboBox LayerBox;
		private Label lable;
		private CheckBox BlurCheck;
		private TextBox FieldBox;
		private TextBox SqlBox;
		private Button RevBut;
		private Button NoneBut;
		private Button AllBut;
		private GroupBox groupBox2;
		private CheckedListBox ValueBox;
    }
}
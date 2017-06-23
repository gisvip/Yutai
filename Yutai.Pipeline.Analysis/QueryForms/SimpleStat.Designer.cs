﻿
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleStat
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
			this.CalButton = new Button();
			this.groupBox1 = new GroupBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.checkedListBox1 = new CheckedListBox();
			this.button1 = new Button();
			this.listBox1 = new ListBox();
			this.groupBox2 = new GroupBox();
			this.groupBox3 = new GroupBox();
			this.InsertBut = new Button();
			this.button5 = new Button();
			this.button4 = new Button();
			this.button3 = new Button();
			this.button2 = new Button();
			this.groupBox4 = new GroupBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.GeometrySet = new CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.CalButton.Location = new System.Drawing.Point(107, 306);
			this.CalButton.Name = "CalButton";
			this.CalButton.Size = new Size(71, 23);
			this.CalButton.TabIndex = 3;
			this.CalButton.Text = "确定(&Q)";
			this.CalButton.UseVisualStyleBackColor = true;
			this.CalButton.Click += new EventHandler(this.CalButton_Click_1);
			this.groupBox1.Controls.Add(this.RevBut);
			this.groupBox1.Controls.Add(this.NoneBut);
			this.groupBox1.Controls.Add(this.AllBut);
			this.groupBox1.Controls.Add(this.checkedListBox1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(312, 108);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "管线数据层列表";
			this.RevBut.Location = new System.Drawing.Point(229, 70);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(76, 23);
			this.RevBut.TabIndex = 6;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(230, 41);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(76, 23);
			this.NoneBut.TabIndex = 5;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(230, 12);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(76, 23);
			this.AllBut.TabIndex = 4;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(6, 16);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new Size(217, 84);
			this.checkedListBox1.Sorted = true;
			this.checkedListBox1.TabIndex = 2;
			this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
			this.button1.Location = new System.Drawing.Point(187, 306);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 14;
			this.button1.Text = "关闭(&G)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(6, 14);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(73, 148);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 17;
			this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
			this.groupBox2.Controls.Add(this.listBox1);
			this.groupBox2.Location = new System.Drawing.Point(12, 126);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(85, 169);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "管径范围";
			this.groupBox3.Controls.Add(this.InsertBut);
			this.groupBox3.Controls.Add(this.button5);
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.button2);
			this.groupBox3.Location = new System.Drawing.Point(103, 126);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(78, 169);
			this.groupBox3.TabIndex = 20;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "操作";
			this.InsertBut.Location = new System.Drawing.Point(6, 140);
			this.InsertBut.Name = "InsertBut";
			this.InsertBut.Size = new Size(69, 23);
			this.InsertBut.TabIndex = 28;
			this.InsertBut.Text = "插入行";
			this.InsertBut.UseVisualStyleBackColor = true;
			this.InsertBut.Click += new EventHandler(this.InsertBut_Click);
			this.button5.Location = new System.Drawing.Point(6, 80);
			this.button5.Name = "button5";
			this.button5.Size = new Size(69, 23);
			this.button5.TabIndex = 27;
			this.button5.Text = "添加行";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.button4.Location = new System.Drawing.Point(6, 110);
			this.button4.Name = "button4";
			this.button4.Size = new Size(69, 23);
			this.button4.TabIndex = 26;
			this.button4.Text = "删除行";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button3.Location = new System.Drawing.Point(6, 50);
			this.button3.Name = "button3";
			this.button3.Size = new Size(69, 23);
			this.button3.TabIndex = 25;
			this.button3.Text = "添加上限";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button2.Location = new System.Drawing.Point(6, 20);
			this.button2.Name = "button2";
			this.button2.Size = new Size(69, 23);
			this.button2.TabIndex = 21;
			this.button2.Text = "添加下限";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.dataGridView1);
			this.groupBox4.Location = new System.Drawing.Point(187, 126);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(137, 169);
			this.groupBox4.TabIndex = 21;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "统计范围设置";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 150);
			this.label1.Name = "label1";
			this.label1.Size = new Size(107, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "下限=<统计值<上限";
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.BackgroundColor = SystemColors.ActiveCaptionText;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1,
				this.Column4
			});
			this.dataGridView1.GridColor = SystemColors.ControlDarkDark;
			this.dataGridView1.Location = new System.Drawing.Point(6, 14);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new Size(126, 127);
			this.dataGridView1.TabIndex = 4;
			this.Column1.Frozen = true;
			this.Column1.HeaderText = "下限";
			this.Column1.MaxInputLength = 6;
			this.Column1.MinimumWidth = 6;
			this.Column1.Name = "Column1";
			this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column1.Width = 60;
			this.Column4.HeaderText = "上限";
			this.Column4.MaxInputLength = 6;
			this.Column4.Name = "Column4";
			this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column4.Width = 60;
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(18, 310);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 22;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.GeometrySet.Click += new EventHandler(this.GeometrySet_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(334, 335);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.CalButton);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleStat";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "管径分段统计";
			base.Load += new EventHandler(this.SimpleStat_Load);
			base.VisibleChanged += new EventHandler(this.SimpleStat_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.SimpleStat_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private string strGJ;
		private IField myfield;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private IField myfieldGJ;
		private QueryResult resultDlg;
		private Button CalButton;
		private GroupBox groupBox1;
		private Button RevBut;
		private Button NoneBut;
		private Button AllBut;
		private CheckedListBox checkedListBox1;
		private Button button1;
		private ListBox listBox1;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private Button button3;
		private Button button2;
		private GroupBox groupBox4;
		private DataGridView dataGridView1;
		private Button button4;
		private Button button5;
		private DataGridViewTextBoxColumn Column1;
		private DataGridViewTextBoxColumn Column4;
		private Label label1;
		private Button InsertBut;
		private CheckBox GeometrySet;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Controls.Editor.UI;
using Yutai.Shared;
using Array = System.Array;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class FeatureInfoControl
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.combLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPos = new DevExpress.XtraEditors.TextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ZoomToFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.PanToFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.objTree = new System.Windows.Forms.TreeView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelAttach = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Infolist = new System.Windows.Forms.ListView();
            this.columnHeader_0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combLayer.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPos.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelAttach.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combLayer);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 26);
            this.panel1.TabIndex = 0;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // combLayer
            // 
            this.combLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combLayer.EditValue = "";
            this.combLayer.Location = new System.Drawing.Point(40, 0);
            this.combLayer.Name = "combLayer";
            this.combLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combLayer.Size = new System.Drawing.Size(530, 20);
            this.combLayer.TabIndex = 3;
            this.combLayer.SelectedIndexChanged += new System.EventHandler(this.combLayer_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(40, 26);
            this.panel4.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层";
            // 
            // txtPos
            // 
            this.txtPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPos.EditValue = "";
            this.txtPos.Location = new System.Drawing.Point(0, 0);
            this.txtPos.Name = "txtPos";
            this.txtPos.Properties.Appearance.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtPos.Properties.Appearance.Options.UseBackColor = true;
            this.txtPos.Properties.ReadOnly = true;
            this.txtPos.Size = new System.Drawing.Size(570, 20);
            this.txtPos.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomToFeature,
            this.PanToFeature});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // ZoomToFeature
            // 
            this.ZoomToFeature.Name = "ZoomToFeature";
            this.ZoomToFeature.Size = new System.Drawing.Size(136, 22);
            this.ZoomToFeature.Text = "缩放到要素";
            this.ZoomToFeature.Click += new System.EventHandler(this.ZoomToFeature_Click);
            // 
            // PanToFeature
            // 
            this.PanToFeature.Name = "PanToFeature";
            this.PanToFeature.Size = new System.Drawing.Size(136, 22);
            this.PanToFeature.Text = "平移到要素";
            this.PanToFeature.Click += new System.EventHandler(this.PanToFeature_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.objTree);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(570, 95);
            this.panel3.TabIndex = 2;
            // 
            // objTree
            // 
            this.objTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objTree.HideSelection = false;
            this.objTree.Location = new System.Drawing.Point(0, 26);
            this.objTree.Name = "objTree";
            this.objTree.Size = new System.Drawing.Size(570, 69);
            this.objTree.TabIndex = 2;
            this.objTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.objTree_AfterSelect);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panelAttach);
            this.panel5.Controls.Add(this.txtPos);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(570, 42);
            this.panel5.TabIndex = 1;
            // 
            // panelAttach
            // 
            this.panelAttach.AutoSize = true;
            this.panelAttach.Controls.Add(this.textBox1);
            this.panelAttach.Controls.Add(this.button1);
            this.panelAttach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttach.Location = new System.Drawing.Point(0, 20);
            this.panelAttach.Name = "panelAttach";
            this.panelAttach.Size = new System.Drawing.Size(570, 22);
            this.panelAttach.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(64, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(506, 21);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "附件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Infolist);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 244);
            this.panel2.TabIndex = 5;
            // 
            // Infolist
            // 
            this.Infolist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_0,
            this.columnHeader_1});
            this.Infolist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Infolist.Location = new System.Drawing.Point(0, 42);
            this.Infolist.Name = "Infolist";
            this.Infolist.Size = new System.Drawing.Size(570, 202);
            this.Infolist.TabIndex = 5;
            this.Infolist.UseCompatibleStateImageBehavior = false;
            this.Infolist.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_0
            // 
            this.columnHeader_0.Text = "字段";
            this.columnHeader_0.Width = 87;
            // 
            // columnHeader_1
            // 
            this.columnHeader_1.Text = "字段值";
            this.columnHeader_1.Width = 164;
            // 
            // FeatureInfoControl
            // 
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "FeatureInfoControl";
            this.Size = new System.Drawing.Size(570, 339);
            this.Load += new System.EventHandler(this.FeatureInfoControl_Load);
            this.SizeChanged += new System.EventHandler(this.FeatureInfoControl_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.combLayer.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPos.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelAttach.ResumeLayout(false);
            this.panelAttach.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       
        private Button button1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ComboBoxEdit combLayer;
        private ContextMenuStrip contextMenuStrip1;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event iactiveViewEvents_Event_0;
        private IArray iarray_0;
        private IBasicMap ibasicMap_0;
        private IContainer components;
        private ListView Infolist;
        private IPoint ipoint_0;
        private Label label1;
        private TreeView objTree;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panelAttach;
        private ToolStripMenuItem PanToFeature;
        private TextBox textBox1;
        private TextEdit txtPos;
        private ToolStripMenuItem ZoomToFeature;
    }
}
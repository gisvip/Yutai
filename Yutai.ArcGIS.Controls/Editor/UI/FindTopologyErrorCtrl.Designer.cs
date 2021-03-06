﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class FindTopologyErrorCtrl
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
            this.chkVisibleRegion = new DevExpress.XtraEditors.CheckEdit();
            this.chkException = new DevExpress.XtraEditors.CheckEdit();
            this.chkError = new DevExpress.XtraEditors.CheckEdit();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.lblErrorNum = new System.Windows.Forms.Label();
            this.cboFindType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewError = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ZoomTo = new DevExpress.XtraBars.BarButtonItem();
            this.PanTo = new DevExpress.XtraBars.BarButtonItem();
            this.SelectFeature = new DevExpress.XtraBars.BarButtonItem();
            this.PromoteToRuleException = new DevExpress.XtraBars.BarButtonItem();
            this.DemoteFromRuleException = new DevExpress.XtraBars.BarButtonItem();
            this.NewFeature = new DevExpress.XtraBars.BarButtonItem();
            this.MergeErrorToFeature = new DevExpress.XtraBars.BarButtonItem();
            this.SubtractError = new DevExpress.XtraBars.BarButtonItem();
            this.Simplify = new DevExpress.XtraBars.BarButtonItem();
            this.Explode = new DevExpress.XtraBars.BarButtonItem();
            this.Delete = new DevExpress.XtraBars.BarButtonItem();
            this.Split = new DevExpress.XtraBars.BarButtonItem();
            this.ExtendLine = new DevExpress.XtraBars.BarButtonItem();
            this.TrimLine = new DevExpress.XtraBars.BarButtonItem();
            this.ShowTopoRuleDescript = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkException.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkError.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkVisibleRegion);
            this.panel1.Controls.Add(this.chkException);
            this.panel1.Controls.Add(this.chkError);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.lblErrorNum);
            this.panel1.Controls.Add(this.cboFindType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 72);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // chkVisibleRegion
            // 
            this.chkVisibleRegion.EditValue = true;
            this.chkVisibleRegion.Location = new System.Drawing.Point(232, 44);
            this.chkVisibleRegion.Name = "chkVisibleRegion";
            this.chkVisibleRegion.Properties.Caption = "仅限可视区域";
            this.chkVisibleRegion.Size = new System.Drawing.Size(104, 19);
            this.chkVisibleRegion.TabIndex = 7;
            // 
            // chkException
            // 
            this.chkException.Location = new System.Drawing.Point(160, 44);
            this.chkException.Name = "chkException";
            this.chkException.Properties.Caption = "例外";
            this.chkException.Size = new System.Drawing.Size(48, 19);
            this.chkException.TabIndex = 6;
            // 
            // chkError
            // 
            this.chkError.EditValue = true;
            this.chkError.Location = new System.Drawing.Point(96, 44);
            this.chkError.Name = "chkError";
            this.chkError.Properties.Caption = "错误";
            this.chkError.Size = new System.Drawing.Size(48, 19);
            this.chkError.TabIndex = 5;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(8, 40);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(72, 24);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "开始查找";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblErrorNum
            // 
            this.lblErrorNum.AutoSize = true;
            this.lblErrorNum.Location = new System.Drawing.Point(344, 10);
            this.lblErrorNum.Name = "lblErrorNum";
            this.lblErrorNum.Size = new System.Drawing.Size(0, 12);
            this.lblErrorNum.TabIndex = 3;
            // 
            // cboFindType
            // 
            this.cboFindType.EditValue = "";
            this.cboFindType.Location = new System.Drawing.Point(48, 8);
            this.cboFindType.Name = "cboFindType";
            this.cboFindType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFindType.Size = new System.Drawing.Size(272, 20);
            this.cboFindType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找:";
            // 
            // listViewError
            // 
            this.listViewError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewError.FullRowSelect = true;
            this.listViewError.GridLines = true;
            this.listViewError.HideSelection = false;
            this.listViewError.Location = new System.Drawing.Point(0, 72);
            this.listViewError.Name = "listViewError";
            this.listViewError.Size = new System.Drawing.Size(504, 216);
            this.listViewError.TabIndex = 2;
            this.listViewError.UseCompatibleStateImageBehavior = false;
            this.listViewError.View = System.Windows.Forms.View.Details;
            this.listViewError.SelectedIndexChanged += new System.EventHandler(this.listViewError_SelectedIndexChanged);
            this.listViewError.Click += new System.EventHandler(this.listViewError_Click);
            this.listViewError.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewError_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "规则类型";
            this.columnHeader2.Width = 96;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "类1";
            this.columnHeader1.Width = 67;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类2";
            this.columnHeader3.Width = 72;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "几何类型";
            this.columnHeader4.Width = 69;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "要素1";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "要素2";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "例外";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ZoomTo,
            this.PanTo,
            this.SelectFeature,
            this.PromoteToRuleException,
            this.DemoteFromRuleException,
            this.NewFeature,
            this.MergeErrorToFeature,
            this.SubtractError,
            this.Simplify,
            this.Explode,
            this.Delete,
            this.Split,
            this.ExtendLine,
            this.TrimLine,
            this.ShowTopoRuleDescript});
            this.barManager1.MaxItemId = 15;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(504, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 288);
            this.barDockControlBottom.Size = new System.Drawing.Size(504, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 288);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(504, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 288);
            // 
            // ZoomTo
            // 
            this.ZoomTo.Caption = "缩放到";
            this.ZoomTo.Id = 0;
            this.ZoomTo.Name = "ZoomTo";
            this.ZoomTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ZoomTo_ItemClick);
            // 
            // PanTo
            // 
            this.PanTo.Caption = "平移到";
            this.PanTo.Id = 1;
            this.PanTo.Name = "PanTo";
            this.PanTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PanTo_ItemClick);
            // 
            // SelectFeature
            // 
            this.SelectFeature.Caption = "选择要素";
            this.SelectFeature.Id = 2;
            this.SelectFeature.Name = "SelectFeature";
            this.SelectFeature.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SelectFeature_ItemClick);
            // 
            // PromoteToRuleException
            // 
            this.PromoteToRuleException.Caption = "标记为例外";
            this.PromoteToRuleException.Id = 3;
            this.PromoteToRuleException.Name = "PromoteToRuleException";
            this.PromoteToRuleException.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PromoteToRuleException_ItemClick);
            // 
            // DemoteFromRuleException
            // 
            this.DemoteFromRuleException.Caption = "标记为错误";
            this.DemoteFromRuleException.Id = 4;
            this.DemoteFromRuleException.Name = "DemoteFromRuleException";
            this.DemoteFromRuleException.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DemoteFromRuleException_ItemClick);
            // 
            // NewFeature
            // 
            this.NewFeature.Caption = "新建要素";
            this.NewFeature.Id = 5;
            this.NewFeature.Name = "NewFeature";
            this.NewFeature.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewFeature_ItemClick);
            // 
            // MergeErrorToFeature
            // 
            this.MergeErrorToFeature.Caption = "合并...";
            this.MergeErrorToFeature.Id = 6;
            this.MergeErrorToFeature.Name = "MergeErrorToFeature";
            this.MergeErrorToFeature.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MergeErrorToFeature_ItemClick);
            // 
            // SubtractError
            // 
            this.SubtractError.Caption = "扣除";
            this.SubtractError.Id = 7;
            this.SubtractError.Name = "SubtractError";
            this.SubtractError.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SubtractError_ItemClick);
            // 
            // Simplify
            // 
            this.Simplify.Caption = "简化";
            this.Simplify.Id = 8;
            this.Simplify.Name = "Simplify";
            this.Simplify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Simplify_ItemClick);
            // 
            // Explode
            // 
            this.Explode.Caption = "炸开";
            this.Explode.Id = 9;
            this.Explode.Name = "Explode";
            // 
            // Delete
            // 
            this.Delete.Caption = "删除";
            this.Delete.Id = 10;
            this.Delete.Name = "Delete";
            this.Delete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Delete_ItemClick);
            // 
            // Split
            // 
            this.Split.Caption = "断开";
            this.Split.Id = 11;
            this.Split.Name = "Split";
            this.Split.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Split_ItemClick);
            // 
            // ExtendLine
            // 
            this.ExtendLine.Caption = "延伸";
            this.ExtendLine.Id = 12;
            this.ExtendLine.Name = "ExtendLine";
            this.ExtendLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExtendLine_ItemClick);
            // 
            // TrimLine
            // 
            this.TrimLine.Caption = "裁剪";
            this.TrimLine.Id = 13;
            this.TrimLine.Name = "TrimLine";
            this.TrimLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.TrimLine_ItemClick);
            // 
            // ShowTopoRuleDescript
            // 
            this.ShowTopoRuleDescript.Caption = "显示规则描述";
            this.ShowTopoRuleDescript.Id = 14;
            this.ShowTopoRuleDescript.Name = "ShowTopoRuleDescript";
            this.ShowTopoRuleDescript.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ShowTopoRuleDescript_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ZoomTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.PanTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.SelectFeature),
            new DevExpress.XtraBars.LinkPersistInfo(this.NewFeature, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.PromoteToRuleException, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.DemoteFromRuleException)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // FindTopologyErrorCtrl
            // 
            this.Controls.Add(this.listViewError);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FindTopologyErrorCtrl";
            this.Size = new System.Drawing.Size(504, 288);
            this.Load += new System.EventHandler(this.FindTopologyErrorCtrl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkException.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkError.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        private IContainer components;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private SimpleButton btnFind;
        private ComboBoxEdit cboFindType;
        private CheckEdit chkError;
        private CheckEdit chkException;
        private CheckEdit chkVisibleRegion;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private BarButtonItem Delete;
        private BarButtonItem DemoteFromRuleException;
        private BarButtonItem Explode;
        private BarButtonItem ExtendLine;
        private Label label1;
        private Label lblErrorNum;
        private ListView listViewError;
        private bool m_CanDo;
        private IWorkspace m_pEditWorkspace;
        private IMap m_pFocusMap;
        private ITopologyLayer m_TopologyLayer;
        private BarButtonItem MergeErrorToFeature;
        private BarButtonItem NewFeature;
        private Panel panel1;
        private BarButtonItem PanTo;
        private PopupMenu popupMenu1;
        private BarButtonItem PromoteToRuleException;
        private BarButtonItem SelectFeature;
        private BarButtonItem ShowTopoRuleDescript;
        private BarButtonItem Simplify;
        private BarButtonItem Split;
        private BarButtonItem SubtractError;
        private BarButtonItem TrimLine;
        private BarButtonItem ZoomTo;
    }
}
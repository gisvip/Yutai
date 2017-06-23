﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class MarkerElementPropertyPage
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
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtWidth = new SpinEdit();
            this.symbolItem1 = new SymbolItem();
            this.btnChangeSymbol = new SimpleButton();
            this.txtAngle = new SpinEdit();
            this.label3 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(56, 16);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "颜色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "大小:";
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(56, 56);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Size = new Size(80, 21);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(160, 24);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(176, 88);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 4;
            this.btnChangeSymbol.Location = new Point(184, 128);
            this.btnChangeSymbol.Name = "btnChangeSymbol";
            this.btnChangeSymbol.Size = new Size(144, 32);
            this.btnChangeSymbol.TabIndex = 5;
            this.btnChangeSymbol.Text = "更改符号...";
            this.btnChangeSymbol.Click += new EventHandler(this.btnChangeSymbol_Click);
            int[] bits2 = new int[4];
            this.txtAngle.EditValue = new decimal(bits2);
            this.txtAngle.Location = new Point(56, 92);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAngle.Size = new Size(80, 21);
            this.txtAngle.TabIndex = 7;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 100);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "角度:";
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "MarkerElementPropertyPage";
            base.Size = new Size(360, 192);
            base.Load += new EventHandler(this.MarkerElementPropertyPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnChangeSymbol;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
        private SymbolItem symbolItem1;
        private SpinEdit txtAngle;
        private SpinEdit txtWidth;
    }
}
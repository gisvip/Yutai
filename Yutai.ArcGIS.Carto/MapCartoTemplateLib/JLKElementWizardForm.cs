﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class JLKElementWizardForm : Form
    {
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private CustomLegendConfigPage customLegendConfigPage_0 = new CustomLegendConfigPage();
        private ElementPosition elementPosition_0 = new ElementPosition();
        private ElementSymbolSetPage elementSymbolSetPage_0 = new ElementSymbolSetPage();
        private ElementTypeSelectPage elementTypeSelectPage_0 = new ElementTypeSelectPage();
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private LegendFormatSetupCtrl legendFormatSetupCtrl_0 = new LegendFormatSetupCtrl();
        private LegendFrameUserControl legendFrameUserControl_0 = new LegendFrameUserControl();
        private LegendTitleUserControl legendTitleUserControl_0 = new LegendTitleUserControl();
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private Panel panel1;
        private PictureSelectPage pictureSelectPage_0 = new PictureSelectPage();
        private TableCellSetPage tableCellSetPage_0 = new TableCellSetPage();
        private TableGeneralPage tableGeneralPage_0 = new TableGeneralPage();
        private TextElementValueSetPage textElementValueSetPage_0 = new TextElementValueSetPage();

        public JLKElementWizardForm()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.elementTypeSelectPage_0.Visible = true;
                    this.elementPosition_0.Visible = false;
                    if ((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.JoinTableElement) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.OLEElement))
                    {
                        this.btnNext.Text = "下一步>";
                    }
                    break;

                case 2:
                    this.elementPosition_0.Visible = true;
                    if (((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.ScaleBarElement) && (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.ScaleTextElement)) && (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.NorthElement))
                    {
                        if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.TableElement)
                        {
                            this.tableGeneralPage_0.Visible = false;
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.CustomLegendElement)
                        {
                            this.customLegendConfigPage_0.Visible = false;
                            this.btnNext.Text = "下一步>";
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.PictureElement)
                        {
                            this.btnNext.Text = "下一步>";
                            this.pictureSelectPage_0.Visible = false;
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.LegendElement)
                        {
                            this.legendTitleUserControl_0.Visible = false;
                        }
                        else
                        {
                            this.elementSymbolSetPage_0.Visible = false;
                        }
                        break;
                    }
                    this.btnNext.Text = "下一步>";
                    this.elementSymbolSetPage_0.Visible = false;
                    break;

                case 3:
                    this.btnNext.Text = "下一步>";
                    if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.LegendElement)
                    {
                        if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.TableElement)
                        {
                            this.tableGeneralPage_0.Visible = true;
                            this.tableCellSetPage_0.Visible = false;
                        }
                        else
                        {
                            this.elementSymbolSetPage_0.Visible = true;
                            this.textElementValueSetPage_0.Visible = false;
                        }
                        break;
                    }
                    this.legendTitleUserControl_0.Visible = true;
                    this.legendFormatSetupCtrl_0.Visible = false;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.elementTypeSelectPage_0.MapTemplate = this.MapTemplate;
                    this.elementTypeSelectPage_0.Apply();
                    this.elementTypeSelectPage_0.Visible = false;
                    this.MapTemplateElement = this.elementTypeSelectPage_0.MapTemplateElement;
                    this.method_0(this.elementTypeSelectPage_0.MapTemplateElement);
                    this.elementPosition_0.Visible = true;
                    if ((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.JoinTableElement) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.OLEElement))
                    {
                        this.btnNext.Text = "完成";
                    }
                    break;

                case 1:
                    this.elementPosition_0.Apply();
                    this.elementPosition_0.Visible = false;
                    if ((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.JoinTableElement) && (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.OLEElement))
                    {
                        if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.CustomLegendElement)
                        {
                            this.customLegendConfigPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.TableElement)
                        {
                            this.tableGeneralPage_0.Visible = true;
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.LegendElement)
                        {
                            this.legendTitleUserControl_0.Visible = true;
                        }
                        else if (((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.ScaleBarElement) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.ScaleTextElement)) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.NorthElement))
                        {
                            this.elementSymbolSetPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.PictureElement)
                        {
                            this.pictureSelectPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.DataGraphicElement)
                        {
                            base.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.elementSymbolSetPage_0.Visible = true;
                        }
                        break;
                    }
                    base.DialogResult = DialogResult.OK;
                    break;

                case 2:
                    if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.PictureElement)
                    {
                        if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.TableElement)
                        {
                            if (!this.tableGeneralPage_0.CanApply())
                            {
                                return;
                            }
                            this.tableGeneralPage_0.Apply();
                            this.tableGeneralPage_0.Visible = false;
                            this.tableCellSetPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            if (((this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.ScaleBarElement) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.ScaleTextElement)) || (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.NorthElement))
                            {
                                base.DialogResult = DialogResult.OK;
                                return;
                            }
                            if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.CustomLegendElement)
                            {
                                this.customLegendConfigPage_0.Apply();
                                base.DialogResult = DialogResult.OK;
                                return;
                            }
                            if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType == MapTemplateElementType.LegendElement)
                            {
                                this.legendTitleUserControl_0.Visible = false;
                                this.legendFormatSetupCtrl_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.btnNext.Text = "完成";
                                this.elementSymbolSetPage_0.Visible = false;
                                this.textElementValueSetPage_0.Visible = true;
                            }
                        }
                        break;
                    }
                    this.pictureSelectPage_0.Apply();
                    base.DialogResult = DialogResult.OK;
                    return;

                case 3:
                    if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.LegendElement)
                    {
                        if (this.elementTypeSelectPage_0.MapTemplateElement.MapTemplateElementType != MapTemplateElementType.TableElement)
                        {
                            this.textElementValueSetPage_0.Apply();
                        }
                        else
                        {
                            this.tableCellSetPage_0.Apply();
                        }
                    }
                    base.DialogResult = DialogResult.OK;
                    return;
            }
            this.int_0++;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JLKElementWizardForm));
            this.panel1 = new Panel();
            this.btnLast = new Button();
            this.btnNext = new Button();
            this.button3 = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1b1, 0x132);
            this.panel1.TabIndex = 8;
            this.btnLast.Location = new Point(0xb8, 0x138);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x4b, 0x17);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(0x109, 0x138);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.button3.DialogResult = DialogResult.Cancel;
            this.button3.Location = new Point(0x15d, 0x138);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 10;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b1, 0x152);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "JLKElementWizardForm";
            this.Text = "元素向导";
            base.Load += new EventHandler(this.JLKElementWizardForm_Load);
            base.ResumeLayout(false);
        }

        private void JLKElementWizardForm_Load(object sender, EventArgs e)
        {
            this.elementTypeSelectPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.elementTypeSelectPage_0);
            this.elementPosition_0.Dock = DockStyle.Fill;
            this.elementPosition_0.Visible = false;
            this.panel1.Controls.Add(this.elementPosition_0);
            this.elementSymbolSetPage_0.Dock = DockStyle.Fill;
            this.elementSymbolSetPage_0.Visible = false;
            this.panel1.Controls.Add(this.elementSymbolSetPage_0);
            this.textElementValueSetPage_0.Dock = DockStyle.Fill;
            this.textElementValueSetPage_0.Visible = false;
            this.panel1.Controls.Add(this.textElementValueSetPage_0);
            this.pictureSelectPage_0.Dock = DockStyle.Fill;
            this.pictureSelectPage_0.Visible = false;
            this.panel1.Controls.Add(this.pictureSelectPage_0);
            this.customLegendConfigPage_0.Dock = DockStyle.Fill;
            this.customLegendConfigPage_0.Visible = false;
            this.panel1.Controls.Add(this.customLegendConfigPage_0);
            this.legendTitleUserControl_0.Dock = DockStyle.Fill;
            this.legendTitleUserControl_0.Visible = false;
            this.panel1.Controls.Add(this.legendTitleUserControl_0);
            this.legendFrameUserControl_0.Dock = DockStyle.Fill;
            this.legendFrameUserControl_0.Visible = false;
            this.panel1.Controls.Add(this.legendFrameUserControl_0);
            this.legendFormatSetupCtrl_0.Dock = DockStyle.Fill;
            this.legendFormatSetupCtrl_0.Visible = false;
            this.panel1.Controls.Add(this.legendFormatSetupCtrl_0);
            this.tableGeneralPage_0.Dock = DockStyle.Fill;
            this.tableGeneralPage_0.Visible = false;
            this.panel1.Controls.Add(this.tableGeneralPage_0);
            this.tableCellSetPage_0.Dock = DockStyle.Fill;
            this.tableCellSetPage_0.Visible = false;
            this.panel1.Controls.Add(this.tableCellSetPage_0);
        }

        private void method_0(MapCartoTemplateLib.MapTemplateElement mapTemplateElement_1)
        {
            this.elementPosition_0.MapTemplateElement = mapTemplateElement_1;
            this.elementSymbolSetPage_0.MapTemplateElement = mapTemplateElement_1;
            this.pictureSelectPage_0.MapTemplateElement = mapTemplateElement_1;
            this.textElementValueSetPage_0.MapTemplateElement = mapTemplateElement_1;
            this.legendTitleUserControl_0.MapTemplateElement = mapTemplateElement_1;
            this.legendFormatSetupCtrl_0.MapTemplateElement = mapTemplateElement_1;
            this.legendFrameUserControl_0.MapTemplateElement = mapTemplateElement_1;
            this.customLegendConfigPage_0.MapTemplateElement = mapTemplateElement_1;
            this.tableCellSetPage_0.SetObjects(mapTemplateElement_1);
            this.tableGeneralPage_0.SetObjects(mapTemplateElement_1);
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateElement_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.mapTemplateElement_0 = value;
            }
        }
    }
}

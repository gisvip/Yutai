﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class ExportMap2PDFPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private IExport m_pExport = null;

        public ExportMap2PDFPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboColorspace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboColorspace.SelectedIndex != -1))
            {
                (this.m_pExport as IExportColorspaceSettings).Colorspace =
                    (esriExportColorspace) this.cboColorspace.SelectedIndex;
            }
        }

        private void cboEmbedFonts_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportPDF).EmbedFonts = this.chkEmbedFonts.Checked;
            }
        }

        private void cboImageCompression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboImageCompression.SelectedIndex != -1))
            {
                (this.m_pExport as IExportPDF).ImageCompression =
                    (esriExportImageCompression) this.cboImageCompression.SelectedIndex;
            }
        }

        private void chkPolygonizeMarkers_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportVectorOptions).PolygonizeMarkers = this.chkPolygonizeMarkers.Checked;
            }
        }

        private void ExportMap2EMFPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pExport != null)
            {
                this.chkPolygonizeMarkers.Checked = (this.m_pExport as IExportVectorOptions).PolygonizeMarkers;
                this.cboColorspace.SelectedIndex = (int) (this.m_pExport as IExportColorspaceSettings).Colorspace;
                this.chkEmbedFonts.Checked = (this.m_pExport as IExportPDF).EmbedFonts;
                this.cboImageCompression.SelectedIndex = (int) (this.m_pExport as IExportPDF).ImageCompression;
                this.m_CanDo = true;
            }
        }

        public IExport Export
        {
            set { this.m_pExport = value; }
        }
    }
}
﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewObjectClass : Form
    {
        private AnnoClassSetCtrl annoClassSetCtrl_0 = new AnnoClassSetCtrl();
        private AnnoReferenceScaleSetCtrl annoReferenceScaleSetCtrl_0 = new AnnoReferenceScaleSetCtrl();
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private IObjectClass iobjectClass_0 = null;
        private NewDatasetCoordinateDomainPage newDatasetCoordinateDomainPage_0 = new NewDatasetCoordinateDomainPage();
        private NewDatasetSpatialRefPage newDatasetSpatialRefPage_0 = new NewDatasetSpatialRefPage();
        private NewDatasetTolerancePage newDatasetTolerancePage_0 = new NewDatasetTolerancePage();
        private NewObjectClassFieldsPage newObjectClassFieldsPage_0 = new NewObjectClassFieldsPage();
        private NewObjectClassGeneralPage newObjectClassGeneralPage_0 = new NewObjectClassGeneralPage();
        private VCSControlPage vcscontrolPage_0 = new VCSControlPage();

        public frmNewObjectClass()
        {
            this.InitializeComponent();
            NewObjectClassHelper.m_pObjectClassHelper = new NewObjectClassHelper();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewObjectClassHelper.m_pObjectClassHelper = null;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset == null)
            {
                if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType != esriFeatureType.esriFTAnnotation)
                {
                    switch (this.int_0)
                    {
                        case 0:
                            return;

                        case 1:
                            this.btnLast.Enabled = false;
                            this.newObjectClassGeneralPage_0.Visible = true;
                            this.newDatasetSpatialRefPage_0.Visible = false;
                            break;

                        case 2:
                            this.newDatasetSpatialRefPage_0.Visible = true;
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                break;
                            }
                            if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem) ||
                                !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                            {
                                this.newDatasetTolerancePage_0.Visible = false;
                                break;
                            }
                            this.vcscontrolPage_0.Visible = false;
                            break;

                        case 3:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                this.btnNext.Text = "下一步";
                                break;
                            }
                            if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem) ||
                                !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                            {
                                this.newDatasetTolerancePage_0.Visible = true;
                                if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                {
                                    this.newObjectClassFieldsPage_0.Visible = false;
                                    this.btnNext.Text = "下一步";
                                }
                                else
                                {
                                    this.newDatasetCoordinateDomainPage_0.Visible = false;
                                }
                                break;
                            }
                            this.vcscontrolPage_0.Visible = true;
                            this.newDatasetTolerancePage_0.Visible = false;
                            break;

                        case 4:
                            if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem) ||
                                !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                this.btnNext.Text = "下一步";
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = true;
                            if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                break;
                            }
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;

                        case 5:
                            this.newDatasetCoordinateDomainPage_0.Visible = true;
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;
                    }
                }
                else
                {
                    switch (this.int_0)
                    {
                        case 0:
                            return;

                        case 1:
                            this.btnLast.Enabled = false;
                            this.newObjectClassGeneralPage_0.Visible = true;
                            this.newDatasetSpatialRefPage_0.Visible = false;
                            break;

                        case 2:
                            this.newDatasetSpatialRefPage_0.Visible = true;
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = false;
                            break;

                        case 3:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                this.annoReferenceScaleSetCtrl_0.Visible = false;
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = true;
                            if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                break;
                            }
                            this.annoReferenceScaleSetCtrl_0.Visible = false;
                            break;

                        case 4:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.annoReferenceScaleSetCtrl_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                break;
                            }
                            if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                this.annoReferenceScaleSetCtrl_0.Visible = false;
                                break;
                            }
                            this.annoReferenceScaleSetCtrl_0.Visible = true;
                            this.annoClassSetCtrl_0.Visible = false;
                            break;

                        case 5:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.annoClassSetCtrl_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                this.btnNext.Text = "下一步";
                                break;
                            }
                            if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.annoReferenceScaleSetCtrl_0.Visible = true;
                                this.annoClassSetCtrl_0.Visible = false;
                                break;
                            }
                            this.annoClassSetCtrl_0.Visible = true;
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;

                        case 6:
                            this.annoClassSetCtrl_0.Visible = true;
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;
                    }
                }
            }
            else if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                switch (this.int_0)
                {
                    case 0:
                        return;

                    case 1:
                        this.btnLast.Enabled = false;
                        this.newObjectClassGeneralPage_0.Visible = true;
                        this.annoReferenceScaleSetCtrl_0.Visible = false;
                        break;

                    case 2:
                        this.annoReferenceScaleSetCtrl_0.Visible = true;
                        this.annoClassSetCtrl_0.Visible = false;
                        break;

                    case 3:
                        this.annoClassSetCtrl_0.Visible = true;
                        this.newObjectClassFieldsPage_0.Visible = false;
                        this.btnNext.Text = "下一步";
                        break;
                }
            }
            else
            {
                switch (this.int_0)
                {
                    case 0:
                        return;

                    case 1:
                        this.newObjectClassGeneralPage_0.Visible = true;
                        this.btnLast.Enabled = false;
                        if (!NewObjectClassHelper.m_pObjectClassHelper.HasM)
                        {
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;
                        }
                        if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                        {
                            this.newDatasetSpatialRefPage_0.Visible = false;
                            break;
                        }
                        this.newDatasetTolerancePage_0.Visible = false;
                        break;

                    case 2:
                        if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                        {
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                this.btnNext.Text = "下一步";
                                break;
                            }
                            if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newDatasetTolerancePage_0.Visible = true;
                                this.newObjectClassFieldsPage_0.Visible = false;
                                this.btnNext.Text = "下一步";
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = true;
                            this.newDatasetCoordinateDomainPage_0.Visible = false;
                        }
                        break;

                    case 3:
                        if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision &&
                            !NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                        {
                            this.newDatasetCoordinateDomainPage_0.Visible = true;
                            this.newObjectClassFieldsPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                        break;
                }
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((this.int_0 != 0) || this.newObjectClassGeneralPage_0.Apply())
            {
                if (NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset == null)
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        switch (this.int_0)
                        {
                            case 0:
                                NewObjectClassHelper.m_pObjectClassHelper.InitFields();
                                this.btnLast.Enabled = true;
                                this.newObjectClassGeneralPage_0.Visible = false;
                                this.newDatasetSpatialRefPage_0.Visible = true;
                                break;

                            case 1:
                                if (!this.newDatasetSpatialRefPage_0.Apply())
                                {
                                    return;
                                }
                                this.newDatasetSpatialRefPage_0.Visible = false;
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    this.newDatasetCoordinateDomainPage_0.Visible = true;
                                    break;
                                }
                                if (
                                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is
                                    IUnknownCoordinateSystem) || !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                                {
                                    this.newDatasetTolerancePage_0.Visible = true;
                                    break;
                                }
                                this.vcscontrolPage_0.Visible = true;
                                break;

                            case 2:
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    if (!this.newDatasetCoordinateDomainPage_0.Apply())
                                    {
                                        return;
                                    }
                                    this.newDatasetCoordinateDomainPage_0.Visible = false;
                                    this.newObjectClassFieldsPage_0.Visible = true;
                                    this.btnNext.Text = "完成";
                                    break;
                                }
                                if (
                                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is
                                    IUnknownCoordinateSystem) || !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                                {
                                    if (!this.newDatasetTolerancePage_0.Apply())
                                    {
                                        return;
                                    }
                                    this.newDatasetTolerancePage_0.Visible = false;
                                    if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                    {
                                        this.newObjectClassFieldsPage_0.Visible = true;
                                        this.btnNext.Text = "完成";
                                    }
                                    else
                                    {
                                        this.newDatasetCoordinateDomainPage_0.Visible = true;
                                    }
                                    break;
                                }
                                this.vcscontrolPage_0.Apply();
                                this.vcscontrolPage_0.Visible = false;
                                this.newDatasetTolerancePage_0.Visible = true;
                                break;

                            case 3:
                                if (!this.newObjectClassFieldsPage_0.Visible)
                                {
                                    if (
                                        !(NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is
                                            IUnknownCoordinateSystem) && NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                                    {
                                        if (!this.newDatasetTolerancePage_0.Apply())
                                        {
                                            return;
                                        }
                                        this.newDatasetTolerancePage_0.Visible = false;
                                        if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                        {
                                            this.newObjectClassFieldsPage_0.Visible = true;
                                            this.btnNext.Text = "完成";
                                        }
                                        else
                                        {
                                            this.newDatasetCoordinateDomainPage_0.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!this.newDatasetCoordinateDomainPage_0.Apply())
                                        {
                                            return;
                                        }
                                        this.newDatasetCoordinateDomainPage_0.Visible = false;
                                        this.newObjectClassFieldsPage_0.Visible = true;
                                        this.btnNext.Text = "完成";
                                    }
                                    break;
                                }
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;

                            case 4:
                                if (
                                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is
                                    IUnknownCoordinateSystem) || !NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                                {
                                    this.newObjectClassFieldsPage_0.Apply();
                                    this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                    if (this.iobjectClass_0 != null)
                                    {
                                        base.DialogResult = DialogResult.OK;
                                        base.Close();
                                        NewObjectClassHelper.m_pObjectClassHelper = null;
                                    }
                                    return;
                                }
                                if (!this.newDatasetCoordinateDomainPage_0.Apply())
                                {
                                    return;
                                }
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                this.newObjectClassFieldsPage_0.Visible = true;
                                this.btnNext.Text = "完成";
                                break;

                            case 5:
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                NewObjectClassHelper.m_pObjectClassHelper = null;
                                return;
                        }
                    }
                    else
                    {
                        switch (this.int_0)
                        {
                            case 0:
                                NewObjectClassHelper.m_pObjectClassHelper.InitFields();
                                this.btnLast.Enabled = true;
                                this.newObjectClassGeneralPage_0.Visible = false;
                                this.newDatasetSpatialRefPage_0.Visible = true;
                                break;

                            case 1:
                                if (!this.newDatasetSpatialRefPage_0.Apply())
                                {
                                    return;
                                }
                                this.newDatasetSpatialRefPage_0.Visible = false;
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    this.newDatasetCoordinateDomainPage_0.Visible = true;
                                    break;
                                }
                                this.newDatasetTolerancePage_0.Visible = true;
                                break;

                            case 2:
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    if (!this.newDatasetCoordinateDomainPage_0.Apply())
                                    {
                                        return;
                                    }
                                    this.newDatasetCoordinateDomainPage_0.Visible = false;
                                    this.annoReferenceScaleSetCtrl_0.Visible = true;
                                    break;
                                }
                                if (!this.newDatasetTolerancePage_0.Apply())
                                {
                                    return;
                                }
                                this.newDatasetTolerancePage_0.Visible = false;
                                if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                {
                                    this.newDatasetCoordinateDomainPage_0.Visible = true;
                                    break;
                                }
                                this.annoReferenceScaleSetCtrl_0.Visible = true;
                                break;

                            case 3:
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    if (!this.annoReferenceScaleSetCtrl_0.Do())
                                    {
                                        return;
                                    }
                                    this.annoReferenceScaleSetCtrl_0.Visible = false;
                                    this.newObjectClassFieldsPage_0.Visible = true;
                                    break;
                                }
                                if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                {
                                    if (!this.newDatasetCoordinateDomainPage_0.Apply())
                                    {
                                        return;
                                    }
                                    this.newDatasetCoordinateDomainPage_0.Visible = false;
                                    this.annoReferenceScaleSetCtrl_0.Visible = true;
                                    break;
                                }
                                if (!this.annoReferenceScaleSetCtrl_0.Do())
                                {
                                    return;
                                }
                                this.annoReferenceScaleSetCtrl_0.Visible = false;
                                this.annoClassSetCtrl_0.Visible = true;
                                break;

                            case 4:
                                if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                                {
                                    this.annoClassSetCtrl_0.Visible = false;
                                    this.newObjectClassFieldsPage_0.Visible = true;
                                    this.btnNext.Text = "完成";
                                    break;
                                }
                                if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                                {
                                    if (!this.annoReferenceScaleSetCtrl_0.Do())
                                    {
                                        return;
                                    }
                                    this.annoReferenceScaleSetCtrl_0.Visible = false;
                                    this.annoClassSetCtrl_0.Visible = true;
                                    break;
                                }
                                this.annoClassSetCtrl_0.Visible = false;
                                this.newObjectClassFieldsPage_0.Visible = true;
                                this.btnNext.Text = "完成";
                                break;

                            case 5:
                                if (!this.newObjectClassFieldsPage_0.Visible)
                                {
                                    this.annoClassSetCtrl_0.Visible = false;
                                    this.newObjectClassFieldsPage_0.Visible = true;
                                    this.btnNext.Text = "完成";
                                    break;
                                }
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;

                            case 6:
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;
                        }
                    }
                }
                else if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    switch (this.int_0)
                    {
                        case 0:
                            NewObjectClassHelper.m_pObjectClassHelper.InitFields();
                            this.btnLast.Enabled = true;
                            this.newObjectClassGeneralPage_0.Visible = false;
                            this.annoReferenceScaleSetCtrl_0.Visible = true;
                            break;

                        case 1:
                            if (!this.annoReferenceScaleSetCtrl_0.Do())
                            {
                                return;
                            }
                            this.annoReferenceScaleSetCtrl_0.Visible = false;
                            this.annoClassSetCtrl_0.Visible = true;
                            break;

                        case 2:
                            this.annoClassSetCtrl_0.Visible = false;
                            this.newObjectClassFieldsPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;

                        case 3:
                            this.newObjectClassFieldsPage_0.Apply();
                            this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                            if (this.iobjectClass_0 != null)
                            {
                                base.DialogResult = DialogResult.OK;
                                base.Close();
                                NewObjectClassHelper.m_pObjectClassHelper = null;
                            }
                            return;
                    }
                }
                else
                {
                    switch (this.int_0)
                    {
                        case 0:
                            NewObjectClassHelper.m_pObjectClassHelper.InitFields();
                            this.newObjectClassGeneralPage_0.Visible = false;
                            this.btnLast.Enabled = true;
                            if (!NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.newObjectClassFieldsPage_0.Visible = true;
                                this.btnNext.Text = "完成";
                                break;
                            }
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = true;
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = true;
                            break;

                        case 1:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;
                            }
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newDatasetCoordinateDomainPage_0.Visible = false;
                                this.newDatasetCoordinateDomainPage_0.Apply();
                                this.newObjectClassFieldsPage_0.Visible = true;
                                this.btnNext.Text = "完成";
                                break;
                            }
                            this.newDatasetTolerancePage_0.Visible = false;
                            this.newDatasetTolerancePage_0.Apply();
                            if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newObjectClassFieldsPage_0.Visible = true;
                                this.btnNext.Text = "完成";
                                break;
                            }
                            this.newDatasetCoordinateDomainPage_0.Visible = true;
                            break;

                        case 2:
                            if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                            {
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;
                            }
                            if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.newObjectClassFieldsPage_0.Apply();
                                this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    NewObjectClassHelper.m_pObjectClassHelper = null;
                                }
                                return;
                            }
                            this.newDatasetCoordinateDomainPage_0.Visible = false;
                            this.newDatasetCoordinateDomainPage_0.Apply();
                            this.newObjectClassFieldsPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;

                        case 3:
                            this.newObjectClassFieldsPage_0.Apply();
                            this.iobjectClass_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                            if (this.iobjectClass_0 != null)
                            {
                                base.DialogResult = DialogResult.OK;
                                base.Close();
                                NewObjectClassHelper.m_pObjectClassHelper = null;
                            }
                            return;
                    }
                }
                this.int_0++;
            }
        }

        private void frmNewObjectClass_Load(object sender, EventArgs e)
        {
            this.annoReferenceScaleSetCtrl_0.Dock = DockStyle.Fill;
            this.annoReferenceScaleSetCtrl_0.Visible = false;
            this.panel1.Controls.Add(this.annoReferenceScaleSetCtrl_0);
            this.annoClassSetCtrl_0.Dock = DockStyle.Fill;
            this.annoClassSetCtrl_0.Visible = false;
            this.panel1.Controls.Add(this.annoClassSetCtrl_0);
            this.newDatasetTolerancePage_0.Dock = DockStyle.Fill;
            this.newDatasetTolerancePage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetTolerancePage_0);
            this.newObjectClassFieldsPage_0.Dock = DockStyle.Fill;
            this.newObjectClassFieldsPage_0.Visible = false;
            this.panel1.Controls.Add(this.newObjectClassFieldsPage_0);
            this.newDatasetCoordinateDomainPage_0.Dock = DockStyle.Fill;
            this.newDatasetCoordinateDomainPage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetCoordinateDomainPage_0);
            this.newDatasetSpatialRefPage_0.Dock = DockStyle.Fill;
            this.newDatasetSpatialRefPage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetSpatialRefPage_0);
            this.newObjectClassGeneralPage_0.Dock = DockStyle.Fill;
            this.vcscontrolPage_0.Dock = DockStyle.Fill;
            this.vcscontrolPage_0.Visible = false;
            this.panel1.Controls.Add(this.vcscontrolPage_0);
            this.panel1.Controls.Add(this.newObjectClassGeneralPage_0);
            this.btnLast.Enabled = false;
        }

        public bool IsFeatureClass
        {
            set { NewObjectClassHelper.m_pObjectClassHelper.IsFeatureClass = value; }
        }

        public IObjectClass ObjectClass
        {
            get { return this.iobjectClass_0; }
        }

        public object Workspace
        {
            set
            {
                if (value is IWorkspace)
                {
                    NewObjectClassHelper.m_pObjectClassHelper.Workspace = value as IWorkspace;
                }
                else if (value is IFeatureDataset)
                {
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset = value as IFeatureDataset;
                }
            }
        }
    }
}
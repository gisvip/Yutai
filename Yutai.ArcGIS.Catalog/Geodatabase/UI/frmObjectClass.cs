﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmObjectClass : Form
    {
        private AnnoClassSetCtrl annoClassSetCtrl_0 = new AnnoClassSetCtrl();
        private AnnoReferenceScaleSetCtrl annoReferenceScaleSetCtrl_0 = new AnnoReferenceScaleSetCtrl();
        private Container container_0 = null;
        private int int_0 = 0;
        private IObjectClass iobjectClass_0 = null;
        private const int m_TotalStep = 3;
        private ObjectClassGeneral objectClassGeneral_0 = new ObjectClassGeneral();
        private ObjectClassKeyConfig objectClassKeyConfig_0 = new ObjectClassKeyConfig();
        private ObjectFieldsPage objectFieldsPage_0 = new ObjectFieldsPage();

        public frmObjectClass()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
                ObjectClassHelper.CreateOCHelper();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.int_0 == 1)
            {
                if (ObjectClassHelper.m_pObjectClassHelper.Workspace.Type == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectClassGeneral_0);
                    this.btnLast.Enabled = false;
                    this.btnNext.Text = "下一步";
                }
                else
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectClassGeneral_0);
                    this.btnLast.Enabled = false;
                }
            }
            else if (this.int_0 == 2)
            {
                if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation)
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.annoReferenceScaleSetCtrl_0);
                }
                else
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectClassKeyConfig_0);
                    this.btnNext.Text = "下一步";
                }
            }
            else if (this.int_0 == 3)
            {
                if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation)
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.annoClassSetCtrl_0);
                }
            }
            else if ((this.int_0 == 4) &&
                     (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation))
            {
                this.panel1.Controls.Clear();
                this.panel1.Controls.Add(this.objectClassKeyConfig_0);
                this.btnNext.Text = "下一步";
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.btnLast.Enabled = true;
            if (this.int_0 == 0)
            {
                if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation)
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.annoReferenceScaleSetCtrl_0);
                }
                else if (ObjectClassHelper.m_pObjectClassHelper.Workspace.Type == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    ObjectClassHelper.m_pObjectClassHelper.InitFields();
                    this.objectFieldsPage_0.Fields = ObjectClassHelper.m_pObjectClassHelper.m_pFieds;
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectFieldsPage_0);
                    this.btnNext.Text = "完成";
                }
                else
                {
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectClassKeyConfig_0);
                }
            }
            else
            {
                Exception exception;
                if (this.int_0 == 1)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation)
                    {
                        if (!this.annoReferenceScaleSetCtrl_0.Do())
                        {
                            return;
                        }
                        this.panel1.Controls.Clear();
                        this.panel1.Controls.Add(this.annoClassSetCtrl_0);
                    }
                    else
                    {
                        if (ObjectClassHelper.m_pObjectClassHelper.Workspace.Type ==
                            esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            try
                            {
                                this.objectFieldsPage_0.Apply();
                                ObjectClassHelper.m_pObjectClassHelper.m_pFieds =
                                    this.objectFieldsPage_0.Fields as IFieldsEdit;
                                this.iobjectClass_0 = ObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                                if (this.iobjectClass_0 != null)
                                {
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                }
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                MessageBox.Show(exception.Message);
                                Logger.Current.Error("", exception, "");
                            }
                            return;
                        }
                        ObjectClassHelper.m_pObjectClassHelper.InitFields();
                        this.objectFieldsPage_0.Fields = ObjectClassHelper.m_pObjectClassHelper.m_pFieds;
                        this.panel1.Controls.Clear();
                        this.panel1.Controls.Add(this.objectFieldsPage_0);
                        this.btnNext.Text = "完成";
                    }
                }
                else if (this.int_0 == 2)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType != esriFeatureType.esriFTAnnotation)
                    {
                        try
                        {
                            this.objectFieldsPage_0.Apply();
                            ObjectClassHelper.m_pObjectClassHelper.m_pFieds =
                                this.objectFieldsPage_0.Fields as IFieldsEdit;
                            this.iobjectClass_0 = ObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                            if (this.iobjectClass_0 != null)
                            {
                                base.DialogResult = DialogResult.OK;
                                base.Close();
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            MessageBox.Show(exception.Message);
                            Logger.Current.Error("", exception, "");
                        }
                        return;
                    }
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(this.objectClassKeyConfig_0);
                }
                else if (this.int_0 == 3)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper.m_FeatreType == esriFeatureType.esriFTAnnotation)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.InitFields();
                        this.objectFieldsPage_0.Fields = ObjectClassHelper.m_pObjectClassHelper.m_pFieds;
                        this.panel1.Controls.Clear();
                        this.panel1.Controls.Add(this.objectFieldsPage_0);
                        this.btnNext.Text = "完成";
                    }
                }
                else if (this.int_0 == 4)
                {
                    try
                    {
                        this.objectFieldsPage_0.Apply();
                        ObjectClassHelper.m_pObjectClassHelper.m_pFieds = this.objectFieldsPage_0.Fields as IFieldsEdit;
                        this.iobjectClass_0 = ObjectClassHelper.m_pObjectClassHelper.CreateObjectClass();
                        if (this.iobjectClass_0 != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        MessageBox.Show(exception.Message);
                        Logger.Current.Error("", exception, "");
                    }
                    return;
                }
            }
            this.int_0++;
        }

        private void frmObjectClass_Load(object sender, EventArgs e)
        {
            if (this.ifeatureWorkspace_0 != null)
            {
                this.objectFieldsPage_0.Workspace = this.ifeatureWorkspace_0 as IWorkspace;
            }
            else
            {
                this.objectFieldsPage_0.Workspace = this.ifeatureDataset_0.Workspace;
            }
            this.panel1.Controls.Add(this.objectClassGeneral_0);
        }

        private void method_0()
        {
            this.btnLast.Enabled = false;
            this.btnNext.Enabled = false;
        }

        private void method_1(int int_1, IFieldsEdit ifieldsEdit_1)
        {
            IFieldEdit field = new FieldClass();
            field.Name_2 = "OBJECTID";
            field.AliasName_2 = "OBJECTID";
            field.IsNullable_2 = false;
            field.Type_2 = esriFieldType.esriFieldTypeOID;
            ifieldsEdit_1.AddField(field);
            if (int_1 == 1)
            {
                field = new FieldClass();
                field.Name_2 = "SHAPE";
                field.AliasName_2 = "SHAPE";
                field.IsNullable_2 = true;
                field.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDefEdit edit2 = new GeometryDefClass();
                if (this.ifeatureDataset_0 != null)
                {
                    edit2.SpatialReference_2 = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
                }
                else
                {
                    edit2.SpatialReference_2 = new UnknownCoordinateSystemClass();
                }
                edit2.GridCount_2 = 1;
                edit2.set_GridSize(0, 1000.0);
                edit2.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                edit2.HasZ_2 = false;
                edit2.HasM_2 = false;
                field.GeometryDef_2 = edit2;
                ifieldsEdit_1.AddField(field);
            }
        }

        public object Dataset
        {
            set
            {
                this.ifeatureWorkspace_0 = value as IFeatureWorkspace;
                this.ifeatureDataset_0 = value as IFeatureDataset;
                if (value is IWorkspace)
                {
                    ObjectClassHelper.m_pObjectClassHelper.Workspace = value as IWorkspace;
                    if ((value as IWorkspace).Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        ObjectClassShareData.m_IsShapeFile = true;
                    }
                    else
                    {
                        ObjectClassShareData.m_IsShapeFile = false;
                    }
                    ObjectClassShareData.m_IsInFeatureDataset = false;
                }
                else if (value is IFeatureDataset)
                {
                    ObjectClassHelper.m_pObjectClassHelper.FeatureDataset = value as IFeatureDataset;
                    ObjectClassShareData.m_IsShapeFile = false;
                    ObjectClassShareData.m_IsInFeatureDataset = true;
                }
            }
        }

        public IObjectClass ObjectClass
        {
            get { return this.iobjectClass_0; }
        }

        public enumUseType UseType
        {
            set
            {
                this.enumUseType_0 = value;
                this.objectFieldsPage_0.UseType = value;
                ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass = value == enumUseType.enumUTFeatureClass;
            }
        }
    }
}
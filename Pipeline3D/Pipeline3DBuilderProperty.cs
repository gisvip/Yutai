// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  Pipeline3DBuilderProperty.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/10/12  10:09
// 更新时间 :  2017/10/12  10:09

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline3D
{
    public class Pipeline3DBuilderProperty
    {
        private int _division;
        private List<Pipeline3DBuilderItem> _builderItems;

        public Pipeline3DBuilderProperty()
        {
            _builderItems = new List<Pipeline3DBuilderItem>();
        }
        public int Division
        {
            get { return _division; }
            set { _division = value; }
        }

        public string NameSuf { get; set; }
        public enumPipelineDepthType DefaultDepthType { get; set; }
        public enumPipeSectionType DefaultSectionType { get; set; }
        public IWorkspace SaveWorkspace { get; set; }
        public enumMultiSaveType SaveType { get; set; }
        public bool IsCreateJXJ { get; set; }
        public bool IsCreateLJD { get; set; }
        public IEnvelope Envelope { get; set; }
        public bool IsExtentOnly { get; set; }

        public List<Pipeline3DBuilderItem> BuilderItems
        {
            get { return _builderItems; }
            set { _builderItems = value; }
        }
    }

    public class Pipeline3DBuilderItem
    {
        private Pipeline3DBuilderProperty _builderProperty;
        private IPipelineLayer _pipelineLayer;

        private IBasicLayerInfo _pointLayerInfo;
        private IBasicLayerInfo _lineLayerInfo;
        private IFeatureClass _pointPatchClass;
        private IFeatureClass _linePatchClass;
        private int _idxDmgcField = -1;
        private int _idxJdsdField = -1;
        private int _idxJgggField = -1;
        private int _idxPointLinkOidField = -1;

        private int _idxQdgcField = -1;
        private int _idxZdgcField = -1;
        private int _idxQdmsField = -1;
        private int _idxZdmsField = -1;
        private int _idxGjField = -1;
        private int _idxLineLinkOidField = -1;

        public Pipeline3DBuilderItem(Pipeline3DBuilderProperty builderProperty, IPipelineLayer pipelineLayer)
        {
            _builderProperty = builderProperty;
            _pipelineLayer = pipelineLayer;
        }

        public IPipelineLayer PipelineLayer
        {
            get { return _pipelineLayer; }
        }

        public IBasicLayerInfo PointLayerInfo
        {
            get
            {
                if (_pointLayerInfo == null)
                {
                    if (PipelineLayer == null)
                        return null;
                    _pointLayerInfo = PipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                }
                return _pointLayerInfo;
            }
        }
        
        public IBasicLayerInfo LineLayerInfo
        {
            get
            {
                if (_lineLayerInfo == null)
                {
                    if (PipelineLayer == null)
                        return null;
                    _lineLayerInfo = PipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                }
                return _lineLayerInfo;
            }
        }

        public IFeatureClass PointPatchClass
        {
            get
            {
                if (_pointPatchClass == null)
                {
                    _pointPatchClass = CreatePatchClass(_builderProperty.SaveWorkspace as IWorkspace2, null, _pointLayerInfo.FeatureClass,
                        _builderProperty.NameSuf);
                }
                return _pointPatchClass;
            }
        }

        public IFeatureClass LinePatchClass
        {
            get
            {
                if (_linePatchClass == null)
                {
                    _linePatchClass = CreatePatchClass(_builderProperty.SaveWorkspace as IWorkspace2, null, _lineLayerInfo.FeatureClass,
                        _builderProperty.NameSuf);
                }
                return _linePatchClass;
            }
        }

        public int IdxDmgcField
        {
            get
            {
                if (_idxDmgcField < 0)
                {
                    if (_pointLayerInfo == null)
                        return -1;
                    _idxDmgcField =
                        _pointLayerInfo.FeatureClass.FindField(
                            _pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.DMGC));
                }
                return _idxDmgcField;
            }
        }

        public int IdxJdsdField
        {
            get
            {
                if (_idxJdsdField < 0)
                {
                    if (_pointLayerInfo == null)
                        return -1;
                    _idxJdsdField =
                        _pointLayerInfo.FeatureClass.FindField(
                            _pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.JDSD));
                }
                return _idxJdsdField;
            }
        }

        public int IdxJgggField
        {
            get
            {
                if (_idxJgggField < 0)
                {
                    if (_pointLayerInfo == null)
                        return -1;
                    _idxJgggField =
                        _pointLayerInfo.FeatureClass.FindField(
                            _pointLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.JGGG));
                }
                return _idxJgggField;
            }
        }

        public int IdxPointLinkOidField
        {
            get
            {
                if (_idxPointLinkOidField < 0)
                {
                    _idxPointLinkOidField = _pointPatchClass.FindField("LinkOID");
                }
                return _idxPointLinkOidField;
            }
        }

        public int IdxQdgcField
        {
            get
            {
                if (_idxQdgcField < 0)
                {
                    _idxQdgcField =
                        _lineLayerInfo.FeatureClass.FindField(
                            _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                }
                return _idxQdgcField;
            }
        }

        public int IdxZdgcField
        {
            get
            {
                if (_idxZdgcField < 0)
                    _idxZdgcField =
                        _lineLayerInfo.FeatureClass.FindField(
                            _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                return _idxZdgcField;
            }
        }

        public int IdxQdmsField
        {
            get
            {
                if (_idxQdmsField < 0)
                    _idxQdmsField =
                        _lineLayerInfo.FeatureClass.FindField(
                            _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                return _idxQdmsField;
            }
        }

        public int IdxZdmsField
        {
            get
            {
                if (_idxZdmsField < 0)
                    _idxZdmsField =
                        _lineLayerInfo.FeatureClass.FindField(
                            _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
                return _idxZdmsField;
            }
        }

        public int IdxGjField
        {
            get
            {
                if (_idxGjField < 0)
                    _idxGjField =
                        _lineLayerInfo.FeatureClass.FindField(
                            _lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                return _idxGjField;
            }
        }

        public int IdxLineLinkOidField
        {
            get
            {
                if (_idxLineLinkOidField < 0)
                    _idxLineLinkOidField = _linePatchClass.FindField("LinkOID");
                return _idxLineLinkOidField;
            }
        }

        public Pipeline3DBuilderProperty BuilderProperty
        {
            get { return _builderProperty; }
        }

        private IFeatureClass CreatePatchClass(IWorkspace2 workspace, IFeatureDataset featureDataset, IFeatureClass sourceClass, string suffixName, bool isAddAttr = false)
        {
            IDataset pDataset = sourceClass as IDataset;
            string featureClassName = pDataset.Name + suffixName;

            IFeatureClass featureClass;
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace; // Explicit Cast

            if (workspace.get_NameExists(esriDatasetType.esriDTFeatureClass, featureClassName)) //feature class with that name already exists 
            {
                featureClass = featureWorkspace.OpenFeatureClass(featureClassName);
                return featureClass;
            }

            UID CLSID = new UIDClass();
            CLSID.Value = "esriGeoDatabase.Feature";


            IObjectClassDescription objectClassDescription = new FeatureClassDescriptionClass();
            IFields fields;

            fields = objectClassDescription.RequiredFields;
            IFieldsEdit fieldsEdit = fields as IFieldsEdit;
            ISpatialReference sSpatial = null;

            for (int i = 0; i < sourceClass.Fields.FieldCount; i++)
            {
                IField sField = sourceClass.Fields.Field[i];
                if (sField.Type == esriFieldType.esriFieldTypeBlob) continue;
                if (sField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IGeometryDef def = sField.GeometryDef;
                    sSpatial = def.SpatialReference;
                    continue;
                }
                if (sField.Type == esriFieldType.esriFieldTypeOID) continue;
                if (sField.Type == esriFieldType.esriFieldTypeRaster) continue;
                if (sField.Editable == false) continue;
                if (isAddAttr)
                {
                    IClone pClone = sField as IClone;
                    fieldsEdit.AddField(pClone.Clone() as IField);
                }
            }

            IField newField = new FieldClass();
            IFieldEdit newFieldEdit = newField as IFieldEdit;
            newFieldEdit.Name_2 = "LinkOID";
            newFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            newFieldEdit.AliasName_2 = "原始OID";
            fieldsEdit.AddField(newField);

            String strShapeField = "";

            // locate the shape field
            for (int j = 0; j < fields.FieldCount; j++)
            {
                if (fields.get_Field(j).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    strShapeField = fields.get_Field(j).Name;
                    IGeometryDef def = fields.get_Field(j).GeometryDef;
                    IGeometryDefEdit defEdit = def as IGeometryDefEdit;
                    defEdit.GeometryType_2 = esriGeometryType.esriGeometryMultiPatch;
                    if (featureDataset == null)
                    {
                        defEdit.SpatialReference_2 = sSpatial;
                    }
                    defEdit.HasZ_2 = true;
                }
            }

            // Use IFieldChecker to create a validated fields collection.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)workspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);


            // finally create and return the feature class
            if (featureDataset == null)// if no feature dataset passed in, create at the workspace level
            {
                featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, CLSID, null, esriFeatureType.esriFTSimple, strShapeField, "");
            }
            else
            {
                featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, CLSID, null, esriFeatureType.esriFTSimple, strShapeField, "");
            }
            return featureClass;
        }

    }
    public enum enumMultiSaveType
    {
        Follow = 0,
        Collection = 1
    }
}
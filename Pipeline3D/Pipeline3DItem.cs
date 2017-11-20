using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline3D
{
    class Pipeline3DItem : I3DItem
    {
        private I3DBuilder _3DBuilder;
        private enumPipelineHeightType _heightType;
        private enumPipeSectionType _sectionType;
        private enumRotationAngleType _rotationAngleType;
        private IPipelineLayer _pipelineLayer;
        private IBasicLayerInfo _pointLayerInfo;
        private IBasicLayerInfo _lineLayerInfo;
        private IFeatureClass _pointPatchClass;
        private IFeatureClass _linePatchClass;
        private int _idxDmgcField = -1;
        private int _idxJdsdField = -1;
        private int _idxJgggField = -1;
        private int _idxPointLinkField = -1;
        private int _idxQdgcField = -1;
        private int _idxZdgcField = -1;
        private int _idxQdmsField = -1;
        private int _idxZdmsField = -1;
        private int _idxGjField = -1;
        private int _idxLineLinkField = -1;
        private string _dmgcFieldName;
        private string _jdsdFieldName;
        private string _jgggFieldName;
        private string _qdgcFieldName;
        private string _zdgcFieldName;
        private string _qdmsFieldName;
        private string _zdmsFieldName;
        private string _gjFieldName;
        private string _xzjdFieldName;
        private int _idxXzjdField = -1;
        private string _fswFieldName;
        private int _idxFswField = -1;
        private List<string> _fswValueList;
        private List<string> _cylinderSubs;
        private List<string> _squareSubs;
        private List<string> _sphereSubs;
        private int _idxMsfsField = -1;
        private string _msfsFieldName;
        private int _idxGgField = -1;
        private string _ggFieldName;
        private List<string> _msfsValueList;
        private List<string> _lCylinderSubs;
        private List<string> _lSquareSubs;

        public Pipeline3DItem(I3DBuilder builder, IPipelineLayer pipelineLayer)
        {
            _3DBuilder = builder;
            _pipelineLayer = pipelineLayer;
        }

        public string Name
        {
            get { return _pipelineLayer.Name; }
        }

        public enumPipelineHeightType HeightType
        {
            get { return _heightType; }
            set { _heightType = value; }
        }

        public enumPipeSectionType SectionType
        {
            get { return _sectionType; }
            set { _sectionType = value; }
        }

        public enumRotationAngleType RotationAngleType
        {
            get { return _rotationAngleType; }
            set { _rotationAngleType = value; }
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
                    _pointPatchClass = CreatePatchClass(_3DBuilder.SaveWorkspace as IWorkspace2, null,
                        _pointLayerInfo.FeatureClass, _3DBuilder.NameSuf);
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
                    _linePatchClass = CreatePatchClass(_3DBuilder.SaveWorkspace as IWorkspace2, null,
                        _lineLayerInfo.FeatureClass, _3DBuilder.NameSuf);
                }
                return _linePatchClass;
            }
        }

        public List<string> FswValueList
        {
            get
            {
                if (IdxFswField < 0)
                    return null;
                if (_fswValueList == null)
                {
                    _fswValueList = new List<string>();
                    CommonHelper.GetUniqueValues((ITable)_pointLayerInfo.FeatureClass, _fswFieldName, _fswValueList);
                }
                return _fswValueList;
            }
        }

        public List<string> CylinderSubs
        {
            get
            {
                if (_cylinderSubs == null)
                    _cylinderSubs = new List<string>();
                return _cylinderSubs;
            }
            set { _cylinderSubs = value; }
        }

        public List<string> SquareSubs
        {
            get
            {
                if (_squareSubs == null)
                    _squareSubs = new List<string>();
                return _squareSubs;
            }
            set { _squareSubs = value; }
        }

        public List<string> SphereSubs
        {
            get
            {
                if (_sphereSubs == null)
                    _sphereSubs = new List<string>();
                return _sphereSubs;
            }
            set { _sphereSubs = value; }
        }

        public List<string> MsfsValueList
        {
            get
            {
                if (IdxMsfsField < 0)
                    return null;
                if (_msfsValueList == null)
                {
                    _msfsValueList = new List<string>();
                    CommonHelper.GetUniqueValues((ITable)_lineLayerInfo.FeatureClass, _msfsFieldName, _msfsValueList);
                }
                return _msfsValueList;
            }
            set { _msfsValueList = value; }
        }

        public List<string> LCylinderSubs
        {
            get
            {
                if (_lCylinderSubs == null)
                    _lCylinderSubs = new List<string>();
                return _lCylinderSubs;
            }
            set { _lCylinderSubs = value; }
        }

        public List<string> LSquareSubs
        {
            get
            {
                if (_lSquareSubs == null)
                    _lSquareSubs = new List<string>();
                return _lSquareSubs;
            }
            set { _lSquareSubs = value; }
        }

        public string DmgcFieldName
        {
            get { return _dmgcFieldName; }
            set { _dmgcFieldName = value; }
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
                        _pointLayerInfo.FeatureClass.FindField(_dmgcFieldName);
                }
                return _idxDmgcField;
            }
        }

        public string JdsdFieldName
        {
            get { return _jdsdFieldName; }
            set { _jdsdFieldName = value; }
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
                        _pointLayerInfo.FeatureClass.FindField(_jdsdFieldName);
                }
                return _idxJdsdField;
            }
        }

        public string JgggFieldName
        {
            get { return _jgggFieldName; }
            set { _jgggFieldName = value; }
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
                        _pointLayerInfo.FeatureClass.FindField(_jgggFieldName);
                }
                return _idxJgggField;
            }
        }

        public string XzjdFieldName
        {
            get { return _xzjdFieldName; }
            set { _xzjdFieldName = value; }
        }

        public int IdxXzjdField
        {
            get
            {
                if (_idxXzjdField < 0)
                {
                    if (_pointLayerInfo == null)
                        return -1;
                    _idxXzjdField = _pointLayerInfo.FeatureClass.FindField(_xzjdFieldName);
                }
                return _idxXzjdField;
            }
        }

        public string FswFieldName
        {
            get { return _fswFieldName; }
            set { _fswFieldName = value; }
        }

        public int IdxFswField
        {
            get
            {
                if (_idxFswField < 0)
                {
                    if (_pointLayerInfo == null)
                        return -1;
                    if (string.IsNullOrEmpty(_fswFieldName))
                        return -1;
                    _idxFswField = _pointLayerInfo.FeatureClass.FindField(_fswFieldName);
                }
                return _idxFswField;
            }
        }

        public int IdxPointLinkField
        {
            get
            {
                if (_idxPointLinkField < 0)
                {
                    _idxPointLinkField = _pointPatchClass.FindField("LinkOID");
                }
                return _idxPointLinkField;
            }
        }

        public string QdgcFieldName
        {
            get { return _qdgcFieldName; }
            set { _qdgcFieldName = value; }
        }

        public int IdxQdgcField
        {
            get
            {
                if (_idxQdgcField < 0)
                {
                    _idxQdgcField =
                        _lineLayerInfo.FeatureClass.FindField(_qdgcFieldName);
                }
                return _idxQdgcField;
            }
        }

        public string ZdgcFieldName
        {
            get { return _zdgcFieldName; }
            set { _zdgcFieldName = value; }
        }

        public int IdxZdgcField
        {
            get
            {
                if (_idxZdgcField < 0)
                    _idxZdgcField =
                        _lineLayerInfo.FeatureClass.FindField(_zdgcFieldName);
                return _idxZdgcField;
            }
        }

        public string QdmsFieldName
        {
            get { return _qdmsFieldName; }
            set { _qdmsFieldName = value; }
        }

        public int IdxQdmsField
        {
            get
            {
                if (_idxQdmsField < 0)
                    _idxQdmsField =
                        _lineLayerInfo.FeatureClass.FindField(_qdmsFieldName);
                return _idxQdmsField;
            }
        }

        public string ZdmsFieldName
        {
            get { return _zdmsFieldName; }
            set { _zdmsFieldName = value; }
        }

        public int IdxZdmsField
        {
            get
            {
                if (_idxZdmsField < 0)
                    _idxZdmsField =
                        _lineLayerInfo.FeatureClass.FindField(_zdmsFieldName);
                return _idxZdmsField;
            }
        }

        public string GjFieldName
        {
            get { return _gjFieldName; }
            set { _gjFieldName = value; }
        }

        public int IdxGjField
        {
            get
            {
                if (_idxGjField < 0)
                    _idxGjField =
                        _lineLayerInfo.FeatureClass.FindField(_gjFieldName);
                return _idxGjField;
            }
        }

        public int IdxMsfsField
        {
            get
            {
                if (_idxMsfsField < 0)
                    _idxMsfsField = _lineLayerInfo.FeatureClass.FindField(_msfsFieldName);
                return _idxMsfsField;
            }
            set { _idxMsfsField = value; }
        }

        public string MsfsFieldName
        {
            get { return _msfsFieldName; }
            set { _msfsFieldName = value; }
        }

        public int IdxGgField
        {
            get
            {
                if (_idxGgField < 0)
                    _idxGgField = _lineLayerInfo.FeatureClass.FindField(_ggFieldName);
                return _idxGgField;
            }
            set { _idxGgField = value; }
        }

        public string GgFieldName
        {
            get { return _ggFieldName; }
            set { _ggFieldName = value; }
        }

        public int IdxLineLinkField
        {
            get
            {
                if (_idxLineLinkField < 0)
                    _idxLineLinkField = _linePatchClass.FindField("LinkOID");
                return _idxLineLinkField;
            }
        }

        public I3DBuilder Builder
        {
            get { return _3DBuilder; }
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
}

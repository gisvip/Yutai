using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Class3D
{
    public class Pipeline3DBuilder
    {
        private ISpatialReference _defaultReference;
        private Pipeline3DBuilderProperty _buildProperty;
        private IPipelineLayer _pipelineLayer;

        public Pipeline3DBuilderProperty BuildProperty
        {
            get { return _buildProperty; }
            set { _buildProperty = value; }
        }

        public Pipeline3DBuilder()
        {
            _buildProperty = new Pipeline3DBuilderProperty();
        }

        public IPipelineLayer PipelineLayer
        {
            get { return _pipelineLayer; }
            set { _pipelineLayer = value; }
        }

        public bool Build()
        {
            for (int i = 0; i < _pipelineLayer.Layers.Count; i++)
            {
                IBasicLayerInfo basicLayer = _pipelineLayer.Layers[i];
                IFeatureClass oneClass = null;
                IWorkspace2 outWorkspace;
                IFeatureDataset outDataset = null;
                if (_buildProperty.SaveType == enumMultiSaveType.Follow)
                {
                    outWorkspace =
                        (IWorkspace2)
                        Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace(basicLayer.FeatureClass);
                    outDataset = basicLayer.FeatureClass.FeatureDataset;
                }
                else
                {
                    outWorkspace = _buildProperty.SaveWorkspace as IWorkspace2;
                }

                oneClass = CreatePatchClass(outWorkspace, outDataset, basicLayer.FeatureClass, _buildProperty.NameSuf, true);

                if (basicLayer.DataType == enumPipelineDataType.Line)
                {
                    ImportPipelineToPatch(basicLayer, oneClass, _buildProperty);
                }
                else if (basicLayer.DataType == enumPipelineDataType.Point)
                {
                    ImportPipePointToPatch(_pipelineLayer, basicLayer, oneClass, _buildProperty);
                }

            }
            return true;
        }

        private void ImportPipePointToPatch(IPipelineLayer pipelineLayer, IBasicLayerInfo basicLayer, IFeatureClass oneClass, Pipeline3DBuilderProperty buildProperty)
        {
            try
            {
                IWorkspace outWorkspace = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace(oneClass) as IWorkspace;
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();

                IFeatureCursor pCursor = null;
                if (buildProperty.IsExtentOnly == false || buildProperty.Envelope == null || buildProperty.Envelope.IsEmpty == true)
                {
                    pCursor = basicLayer.FeatureClass.Search(null, false);
                }
                else
                {
                    ISpatialFilter pFilter = new SpatialFilterClass();
                    pFilter.Geometry = buildProperty.Envelope;
                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pCursor = basicLayer.FeatureClass.Search((IQueryFilter)pFilter, false);
                }

                IFeature sFeature = null;
                int linkOIDIdx = oneClass.FindField("LinkOID");


                //开始获取特殊字段索引
                int heightIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.PointWords.DMGC));
                int depthIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.PointWords.JDSD));
                int angleIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.PointWords.XZJD));
                int standardIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.PointWords.JGGG));

                //! 如果存在井底埋深，则进行竖直管线生成，包括窖井，雨水篦子等，如果没有井底深度，则只生成球体
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        //开始读取原始数据
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;
                        if (sFeature.Value[depthIndex] != DBNull.Value)
                        {
                            IGeometry patch = CreatePointExtrudePatch(sFeature, basicLayer, heightIndex, depthIndex,
                                standardIndex, angleIndex, _buildProperty.Division);
                            IFeature newFeature = oneClass.CreateFeature();
                            newFeature.Shape = patch;
                            newFeature.Value[linkOIDIdx] = sFeature.OID;
                            newFeature.Store();
                        }

                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("OBJECTID:{0}\r\n{1}", sFeature.OID, e.Message));
                    }
                }
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0}\r\n{1}", basicLayer.AliasName, e.Message));
            }
        }


        private IGeometry CreatePointExtrudePatch(IFeature pFeature, IBasicLayerInfo pipeInfo, int heightIndex, int depthIndex, int standardIndex, int angleIndex, int division = 12)
        {
            try
            {
                double jinZoom = 2.5;
                double nullDepth = 2;
                double nullElev = 151;
                string nullStandard = "70";
                IPoint oPoint = pFeature.Shape as IPoint;
                IPoint startPoint = new PointClass()
                {
                    X = oPoint.X,
                    Y = oPoint.Y
                };
                double depth = 0;
                if (pipeInfo.DepthType == enumPipelineDepthType.Absolute)
                {
                    startPoint.Z = pFeature.Value[depthIndex] != DBNull.Value ? Convert.ToDouble(pFeature.Value[depthIndex]) : nullElev;
                    depth = Convert.ToDouble(pFeature.Value[heightIndex]) - Convert.ToDouble(pFeature.Value[depthIndex]);
                }
                else
                {
                    if (pFeature.Value[heightIndex] != DBNull.Value &&
                        pFeature.Value[depthIndex] != DBNull.Value)
                    {
                        startPoint.Z = Convert.ToDouble(pFeature.Value[heightIndex]) -
                                       Convert.ToDouble(pFeature.Value[depthIndex]);
                        depth = Convert.ToDouble(pFeature.Value[depthIndex]);
                    }
                    else if (pFeature.Value[heightIndex] != DBNull.Value &&
                            pFeature.Value[depthIndex] == DBNull.Value)
                    {
                        startPoint.Z = Convert.ToDouble(pFeature.Value[heightIndex]) -
                                       nullDepth;
                        depth = nullDepth;
                    }
                    else if (pFeature.Value[heightIndex] == DBNull.Value &&
                             pFeature.Value[depthIndex] == DBNull.Value)
                    {
                        startPoint.Z = nullElev;
                        depth = nullDepth;
                    }
                }


                object _missing = Type.Missing;
                IPointCollection pointCollection = new PolygonClass() as IPointCollection;
                IVector3D pVectorZ = new Vector3DClass();
                pVectorZ.SetComponents(0, 0, 1);
                IVector3D VectorXOY = new Vector3DClass();
                VectorXOY.SetComponents(1, 0, 0);
                string standard;
                object objStandard = pFeature.Value[standardIndex];
                if (objStandard is DBNull || objStandard == null)
                    standard = nullStandard;
                else
                {
                    standard = objStandard.ToString().Trim();
                }
                if (string.IsNullOrEmpty(standard)) standard = nullStandard;
                string[] standards = standard.Split('*');
                if (standards.Length > 1)
                {
                    double xl = jinZoom * Convert.ToDouble(standards[0]) / 1000;
                    double yl = jinZoom * Convert.ToDouble(standards[1]) / 1000;
                    IPoint pnt = new PointClass();
                    pnt.X = -xl / 2;
                    pnt.Y = -yl / 2;
                    pnt.Z = 0;
                    pointCollection.AddPoint(pnt, ref _missing, ref _missing);
                    pnt = new PointClass();
                    pnt.X = -xl / 2;
                    pnt.Y = yl / 2;
                    pnt.Z = 0;
                    pointCollection.AddPoint(pnt, ref _missing, ref _missing);
                    pnt = new PointClass();
                    pnt.X = xl / 2;
                    pnt.Y = yl / 2;
                    pnt.Z = 0;
                    pointCollection.AddPoint(pnt, ref _missing, ref _missing);
                    pnt = new PointClass();
                    pnt.X = xl / 2;
                    pnt.Y = -yl / 2;
                    pnt.Z = 0;
                    pointCollection.AddPoint(pnt, ref _missing, ref _missing);
                    ((IPolygon)pointCollection).Close();
                }
                else
                {
                    if (standards[0] == "") standards[0] = nullStandard;
                    double angle = 2 * Math.PI / division;
                    for (int i = 0; i < division; i++)
                    {
                        double xl = jinZoom * Convert.ToDouble(standards[0]) / 1000;
                        IPoint pPoint = new PointClass();
                        pPoint.X = xl * Math.Cos(angle * i) / 2;
                        pPoint.Y = xl * Math.Sin(angle * i) / 2;
                        pPoint.Z = 0;
                        pointCollection.AddPoint(pPoint, ref _missing, ref _missing);
                    }
                    ((IPolygon)pointCollection).Close();
                }

                IConstructMultiPatch patch = new MultiPatchClass();
                IZAware zAware = pointCollection as IZAware;
                zAware.ZAware = true;
                patch.ConstructExtrude(depth, pointCollection as IGeometry);
                ITransform3D transform3D = patch as ITransform3D;
                transform3D.Move3D(startPoint.X, startPoint.Y, startPoint.Z);
                return patch as IGeometry;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ImportPipelineToPatch(IBasicLayerInfo basicLayer, IFeatureClass oneClass, Pipeline3DBuilderProperty property)
        {
            try
            {
                IWorkspace outWorkspace = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace(oneClass) as IWorkspace;
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();

                IFeatureCursor pCursor = null;
                if (property.IsExtentOnly == false || property.Envelope == null || property.Envelope.IsEmpty == true)
                {
                    pCursor = basicLayer.FeatureClass.Search(null, false);
                }
                else
                {
                    ISpatialFilter pFilter = new SpatialFilterClass();
                    pFilter.Geometry = property.Envelope;
                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pCursor = basicLayer.FeatureClass.Search((IQueryFilter)pFilter, false);
                }

                IFeature sFeature = null;
                int linkOIDIdx = oneClass.FindField("LinkOID");


                //开始获取特殊字段索引
                int startHeightIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                int endHeightIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                int startDepthIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                int endDepthIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
                int standardIndex = pCursor.FindField(basicLayer.GetFieldName(PipeConfigWordHelper.LineWords.GJ));

                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        //开始读取原始数据
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;


                        CustomPipeline customPipeline = new CustomPipeline(sFeature, basicLayer, startHeightIndex, endHeightIndex, startDepthIndex, endDepthIndex, standardIndex, _buildProperty.Division);
                        for (int i = 0; i < customPipeline.StandardList.Count; i++)
                        {
                            IGeometry patch = customPipeline.CreateLinePatch(i);
                            IFeature newFeature = oneClass.CreateFeature();
                            newFeature.Shape = patch;
                            newFeature.Value[linkOIDIdx] = sFeature.OID;
                            newFeature.Store();

                        }
                        //! 生成连接段球体
                        if (customPipeline.StandardList.Count > 0)
                        {
                            IFeature newFeature = oneClass.CreateFeature();
                            if (string.IsNullOrEmpty(customPipeline.StandardList[0]))
                                customPipeline.StandardList[0] = "100";
                            double radius = Convert.ToDouble(customPipeline.StandardList[0]) / 1000.0;
                            newFeature.Shape = customPipeline.CreateStartSphere(radius, 10);
                            newFeature.Store();
                            newFeature = oneClass.CreateFeature();
                            newFeature.Shape = customPipeline.CreateEndSphere(radius, 10);
                            newFeature.Store();
                        }

                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("OBJECTID:{0}\r\n{1}", sFeature.OID, e.Message));
                    }
                }
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0}\r\n{1}", basicLayer.AliasName, e.Message));
            }
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

    public class Pipeline3DBuilderProperty
    {
        private int _division;

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
    }

    public enum enumMultiSaveType
    {
        Follow = 0,
        Collection = 1
    }
}

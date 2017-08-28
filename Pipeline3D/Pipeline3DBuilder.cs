using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{

    public class Pipeline3DBuilder
    {
        private ISpatialReference _defaultReference;
        private List<PipeClassInfo> _pipeClasses;
        private List<IFeatureClass> _patchClasses;
        public Pipeline3DBuilder()
        {
            _pipeClasses = new List<PipeClassInfo>();
            _patchClasses = new List<IFeatureClass>();
        }
        public void AddPipeClass(PipeClassInfo pipeClassInfo)
        {
            _pipeClasses.Add(pipeClassInfo);
        }
        public void AddPipeClasses(List<PipeClassInfo> pipeClassInfos)
        {
            _pipeClasses.AddRange(pipeClassInfos);
        }


        public void AddPipeClass(IFeatureClass pipeClass, PipeElevationType elevationType,
            string groundElevationFieldName0, string underGroundElevationFieldName0, string groundElevationFieldName1, string underGroundElevationFieldName1, string standFieldName,
            string materialFieldName, string justPointTypeFieldName)
        {
            _pipeClasses.Add(new PipeClassInfo(pipeClass, elevationType, groundElevationFieldName0, underGroundElevationFieldName0, groundElevationFieldName1, underGroundElevationFieldName1, standFieldName, materialFieldName, justPointTypeFieldName));
        }

        public List<IFeatureClass> PatchClasses
        {
            get { return _patchClasses; }
        }

        public bool Build(IWorkspace outWorkspace, string suffixName = "_3D")
        {
            if (_pipeClasses.Count == 0) return false;
            foreach (PipeClassInfo pipeClass in _pipeClasses)
            {
                //创建对应的MultiPatch要素类
                IFeatureClass oneClass = CreatePatchClass((IWorkspace2)outWorkspace, null, pipeClass.PipeClass, suffixName);
                ImportPipeClassToPatch(outWorkspace, pipeClass, oneClass);
            }
            return true;
        }

        private void ImportPipeClassToPatch(IWorkspace outWorkspace, PipeClassInfo pipeInfo, IFeatureClass patchClass, IGeometry boundary = null)
        {
            try
            {
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();

                IFeatureCursor pCursor = null;
                if (boundary == null || boundary.IsEmpty == true)
                {
                    pCursor = pipeInfo.PipeClass.Search(null, false);
                }
                else
                {
                    ISpatialFilter pFilter = new SpatialFilterClass();
                    pFilter.Geometry = boundary;
                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pCursor = pipeInfo.PipeClass.Search((IQueryFilter)pFilter, false);
                }
                IFeature sFeature = null;
                int linkOIDIdx = patchClass.FindField("LinkOID");
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        //开始读取原始数据
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;
                        if (pipeInfo.ClassType == PipeClassType.Point)
                        {
                            IGeometry patch = CreatePointPatch(sFeature, pipeInfo);
                            IFeature newFeature = patchClass.CreateFeature();
                            newFeature.Shape = patch;
                            newFeature.Value[linkOIDIdx] = sFeature.OID;
                            newFeature.Store();
                        }
                        else if (pipeInfo.ClassType == PipeClassType.Line)
                        {
                            if (sFeature.OID == 1)
                            { }
                            CustomPipeline customPipeline = new CustomPipeline(sFeature, pipeInfo);
                            for (int i = 0; i < customPipeline.StandardList.Count; i++)
                            {
                                IGeometry patch = customPipeline.CreateLinePatch(i); 
                                IFeature newFeature = patchClass.CreateFeature();
                                newFeature.Shape = patch;
                                newFeature.Value[linkOIDIdx] = sFeature.OID;
                                newFeature.Store();
                            }
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
                throw new Exception(string.Format("{0}\r\n{1}", pipeInfo.PipeClass.AliasName, e.Message));
            }
        }

        private IGeometry CreatePointPatch(IFeature pFeature, PipeClassInfo pipeInfo)
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
                if (pipeInfo.ElevationType == PipeElevationType.Absolute)
                {
                    startPoint.Z = pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index] != DBNull.Value ? Convert.ToDouble(pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index]) : nullElev;
                    depth = Convert.ToDouble(pFeature.Value[pipeInfo.StartGroundElevationField.Index]) - Convert.ToDouble(pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index]);
                }
                else
                {
                    if (pFeature.Value[pipeInfo.StartGroundElevationField.Index] != DBNull.Value &&
                        pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index] != DBNull.Value)
                    {
                        startPoint.Z = Convert.ToDouble(pFeature.Value[pipeInfo.StartGroundElevationField.Index]) -
                                       Convert.ToDouble(pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index]);
                        depth = Convert.ToDouble(pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index]);
                    }
                    else if (pFeature.Value[pipeInfo.StartGroundElevationField.Index] != DBNull.Value &&
                            pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index] == DBNull.Value)
                    {
                        startPoint.Z = Convert.ToDouble(pFeature.Value[pipeInfo.StartGroundElevationField.Index]) -
                                       nullDepth;
                        depth = nullDepth;
                    }
                    else if (pFeature.Value[pipeInfo.StartGroundElevationField.Index] == DBNull.Value &&
                             pFeature.Value[pipeInfo.StartUnderGroundElevationField.Index] == DBNull.Value)
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
                object objStandard = pFeature.Value[pipeInfo.StandardField.Index];
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
                    double angle = 2 * Math.PI / pipeInfo.Division;
                    for (int i = 0; i < pipeInfo.Division; i++)
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
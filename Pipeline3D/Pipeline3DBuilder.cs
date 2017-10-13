using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{

    public class Pipeline3DBuilder
    {
        private ISpatialReference _defaultReference;
        private Pipeline3DBuilderProperty _builderPropertie;

        public Pipeline3DBuilderProperty BuilderPropertie
        {
            get { return _builderPropertie; }
            set { _builderPropertie = value; }
        }

        public bool Build()
        {
            if (_builderPropertie == null || _builderPropertie.BuilderItems.Count == 0) return false;
            foreach (Pipeline3DBuilderItem builderItem in _builderPropertie.BuilderItems)
            {
                ImportPipeClassToPointPatch(_builderPropertie.SaveWorkspace, builderItem);
                ImportPipeClassToLinePatch(_builderPropertie.SaveWorkspace, builderItem);
            }
            return true;
        }

        private void ImportPipeClassToPointPatch(IWorkspace outWorkspace, Pipeline3DBuilderItem builderItem, IGeometry boundary = null)
        {
            try
            {
                if (builderItem.PointLayerInfo == null)
                    return;
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();

                IFeatureCursor pCursor = null;
                if (boundary == null || boundary.IsEmpty == true)
                {
                    pCursor = builderItem.PointLayerInfo.FeatureClass.Search(null, false);
                }
                else
                {
                    ISpatialFilter pFilter = new SpatialFilterClass();
                    pFilter.Geometry = boundary;
                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pCursor = builderItem.PointLayerInfo.FeatureClass.Search((IQueryFilter)pFilter, false);
                }
                IFeature sFeature = null;
                IFeatureCursor insertCursor = builderItem.PointPatchClass.Insert(true);
                IFeatureBuffer featureBuffer;
                int count = 0;
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        //开始读取原始数据
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;
                        IGeometry patchGeometry = CreatePointPatch(sFeature, builderItem);
                        featureBuffer = builderItem.PointPatchClass.CreateFeatureBuffer();
                        featureBuffer.Shape = patchGeometry;
                        featureBuffer.Value[builderItem.IdxPointLinkOidField] = sFeature.OID;

                        insertCursor.InsertFeature(featureBuffer);
                        count++;
                        if (count >= 500)
                        {
                            insertCursor.Flush();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("OBJECTID:{0}\r\n{1}", sFeature.OID, e.Message));
                    }
                }
                insertCursor.Flush();
                Marshal.ReleaseComObject(insertCursor);
                Marshal.ReleaseComObject(pCursor);
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0}\r\n{1}", builderItem.PointLayerInfo.AliasName, e.Message));
            }
        }

        private void ImportPipeClassToLinePatch(IWorkspace outWorkspace, Pipeline3DBuilderItem builderItem, IGeometry boundary = null)
        {
            try
            {
                if (builderItem.LineLayerInfo == null)
                    return;
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();
                IFeatureClassLoad featureClassLoad = builderItem.LinePatchClass as IFeatureClassLoad;
                featureClassLoad.LoadOnlyMode = true;
                IFeatureCursor pCursor = null;
                if (boundary == null || boundary.IsEmpty == true)
                {
                    pCursor = builderItem.LineLayerInfo.FeatureClass.Search(null, false);
                }
                else
                {
                    ISpatialFilter pFilter = new SpatialFilterClass();
                    pFilter.Geometry = boundary;
                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pCursor = builderItem.LineLayerInfo.FeatureClass.Search((IQueryFilter)pFilter, false);
                }
                IFeature sFeature = null;
                IFeatureCursor insertCursor = builderItem.LinePatchClass.Insert(true);
                IFeatureBuffer featureBuffer;
                int count = 0;
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;
                        CustomPipeline customPipeline = new CustomPipeline(sFeature, builderItem);
                        for (int i = 0; i < customPipeline.StandardList.Count; i++)
                        {
                            IGeometry patchGeometry = customPipeline.CreateLinePatch(i);
                            featureBuffer = builderItem.LinePatchClass.CreateFeatureBuffer();
                            featureBuffer.Shape = patchGeometry;
                            featureBuffer.Value[builderItem.IdxLineLinkOidField] = sFeature.OID;
                            insertCursor.InsertFeature(featureBuffer);
                            count++;
                            if (count >= 500)
                            {
                                insertCursor.Flush();
                                count = 0;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("OBJECTID:{0}\r\n{1}", sFeature.OID, e.Message));
                    }
                }
                insertCursor.Flush();
                Marshal.ReleaseComObject(insertCursor);
                Marshal.ReleaseComObject(pCursor);
                featureClassLoad.LoadOnlyMode = false;
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0}\r\n{1}", builderItem.LineLayerInfo.AliasName, e.Message));
            }
        }
        
        private IGeometry CreatePointPatch(IFeature pFeature, Pipeline3DBuilderItem builderItem)
        {
            try
            {
                string nullStandard = "0.01";
                IPoint oPoint = pFeature.Shape as IPoint;
                IPoint startPoint = new PointClass()
                {
                    X = oPoint.X,
                    Y = oPoint.Y
                };
                startPoint.Z = ConvertToDouble(pFeature.Value[builderItem.IdxDmgcField]);
                if (double.IsNaN(startPoint.Z))
                    startPoint.Z = 0;
                double depth = ConvertToDouble(pFeature.Value[builderItem.IdxJdsdField]);
                if (double.IsNaN(depth))
                    depth = 0.01;
                startPoint.Z = startPoint.Z - depth;
                object _missing = Type.Missing;
                IPointCollection pointCollection = new PolygonClass() as IPointCollection;
                IVector3D pVectorZ = new Vector3DClass();
                pVectorZ.SetComponents(0, 0, 1);
                IVector3D VectorXOY = new Vector3DClass();
                VectorXOY.SetComponents(1, 0, 0);
                string standard;
                object objStandard = pFeature.Value[builderItem.IdxJgggField];
                if (objStandard is DBNull)
                    standard = nullStandard;
                else if (objStandard == null)
                    standard = nullStandard;
                else
                {
                    standard = objStandard.ToString().Trim();
                }
                if (string.IsNullOrEmpty(standard)) standard = nullStandard;
                string[] standards = standard.Split('*');
                if (standards.Length > 1)
                {
                    double xl = Convert.ToDouble(standards[0]) / 100;
                    double yl = Convert.ToDouble(standards[1]) / 100;
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
                    if (standards[0] == "" || standards[0] == "<空>") standards[0] = nullStandard;
                    double angle = 2 * Math.PI / _builderPropertie.Division;
                    for (int i = 0; i < _builderPropertie.Division; i++)
                    {
                        double xl = Convert.ToDouble(standards[0]) / 100;
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

        public static double ConvertToDouble(object obj)
        {
            double value;
            if (obj == null || obj is DBNull || double.TryParse(obj.ToString(), out value) == false)
            {
                value = Double.NaN;
            }
            return value;
        }

        public static string ConvertToString(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return null;
            }
            return obj.ToString();
        }
    }
}
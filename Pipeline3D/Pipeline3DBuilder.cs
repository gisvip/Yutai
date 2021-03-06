using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{

    public class Pipeline3DBuilder
    {
        private I3DBuilder _3DBuilder;
        private List<string> _checkedList;

        public Pipeline3DBuilder(I3DBuilder builder)
        {
            _3DBuilder = builder;
            _checkedList = _3DBuilder.GetCheckPipeline();
        }

        public void Build()
        {
            if (_checkedList == null || _checkedList.Any() == false)
                return;
            if (_3DBuilder.Items == null || _3DBuilder.Items.Any() == false)
                return;
            foreach (I3DItem item in _3DBuilder.Items)
            {
                if (_checkedList.Contains(item.Name) == false)
                    continue;
                ImportPipeClassToPointPatch(_3DBuilder.SaveWorkspace, item);
                ImportPipeClassToLinePatch(_3DBuilder.SaveWorkspace, item);
            }

            return;
        }

        private void ImportPipeClassToPointPatch(IWorkspace outWorkspace, I3DItem builderItem, IGeometry boundary = null)
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
                IFeatureBuffer featureBuffer = builderItem.PointPatchClass.CreateFeatureBuffer();
                int count = 0;
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        //开始读取原始数据
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;
                        IGeometry patchGeometry = CreatePointPatch(sFeature, builderItem);
                        if (patchGeometry == null)
                            continue;
                        featureBuffer.Shape = patchGeometry;
                        featureBuffer.Value[builderItem.IdxPointLinkField] = sFeature.OID;

                        insertCursor.InsertFeature(featureBuffer);
                        count++;
                        if (count >= 1000)
                        {
                            count = 0;
                            insertCursor.Flush();
                            Marshal.ReleaseComObject(insertCursor);
                            insertCursor = builderItem.PointPatchClass.Insert(true);
                            featureBuffer = builderItem.PointPatchClass.CreateFeatureBuffer();
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

        private void ImportPipeClassToLinePatch(IWorkspace outWorkspace, I3DItem builderItem, IGeometry boundary = null)
        {
            try
            {
                if (builderItem.LineLayerInfo == null)
                    return;
                IWorkspaceEdit workspaceEdit = outWorkspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();
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
                IFeatureBuffer featureBuffer = builderItem.LinePatchClass.CreateFeatureBuffer();
                int count = 0;
                while ((sFeature = pCursor.NextFeature()) != null)
                {
                    try
                    {
                        IGeometry pShape = sFeature.Shape;
                        if (pShape.IsEmpty) continue;

                        List<IGeometry> geometrys = CreateLinePatch(sFeature, builderItem);
                        foreach (IGeometry geometry in geometrys)
                        {
                            if (geometry == null)
                                continue;
                            featureBuffer.Shape = geometry;
                            featureBuffer.Value[builderItem.IdxLineLinkField] = sFeature.OID;
                            insertCursor.InsertFeature(featureBuffer);
                        }

                        count++;

                        if (count >= 1000)
                        {
                            count = 0;
                            insertCursor.Flush();
                            Marshal.ReleaseComObject(insertCursor);
                            insertCursor = builderItem.LinePatchClass.Insert(true);
                            featureBuffer = builderItem.LinePatchClass.CreateFeatureBuffer();
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
                throw new Exception(string.Format("{0}\r\n{1}", builderItem.LineLayerInfo.AliasName, e.Message));
            }
        }

        private List<IGeometry> CreateLinePatch(IFeature feature, I3DItem builderItem)
        {
            List<IGeometry> list = new List<IGeometry>();
            try
            {
                IPolyline polyline = feature.Shape as IPolyline;
                if (polyline == null || polyline.IsEmpty)
                    return list;

                double qdgc = ConvertToDouble(feature.Value[builderItem.IdxQdgcField]);
                double zdgc = ConvertToDouble(feature.Value[builderItem.IdxZdgcField]);

                if (Math.Abs(qdgc) < 0.01 || double.IsNaN(qdgc))
                    qdgc = zdgc;
                if (Math.Abs(zdgc) < 0.01 || double.IsNaN(zdgc))
                    zdgc = qdgc;

                string msfs = ConvertToString(feature.Value[builderItem.IdxMsfsField]).Trim();
                if (builderItem.LCylinderSubs.Contains(msfs))
                {
                    string standard = ConvertToString(feature.Value[builderItem.IdxGjField]).Replace(" ", "");
                    List<string> standardList = GetStandardList(standard);

                    string[] standards = standardList[0].Split('*');

                    if (string.IsNullOrEmpty(standards[0]))
                        standards[0] = "100";
                    double diameter = ConvertToDouble(standards[0]) / 1000;
                    switch (builderItem.LineLayerInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            qdgc = qdgc - diameter / 2;
                            zdgc = zdgc - diameter / 2;
                            break;
                        case enumPipelineHeightType.Bottom:
                            qdgc = qdgc + diameter / 2;
                            zdgc = zdgc + diameter / 2;
                            break;
                    }
                    I3DLine line = new Line3DCylinder(polyline, qdgc, zdgc, diameter, builderItem.Builder.Division);
                    list.Add(line.CreateGeometry());
                    list.Add(line.CreateStartSphere());
                    list.Add(line.CreateEndSphere());

                }
                else if (builderItem.LSquareSubs.Contains(msfs))
                {
                    string standard = ConvertToString(feature.Value[builderItem.IdxGgField]).Replace(" ", "");
                    List<string> standardList = GetStandardList(standard);

                    string[] standards = standardList[0].Split('*');

                    double height = 0, width = 0;
                    switch (builderItem.LineLayerInfo.SectionType)
                    {
                        case enumPipeSectionType.HeightAndWidth:
                            height = ConvertToDouble(standards[0]) / 100;
                            width = ConvertToDouble(standards[1]) / 100;
                            break;
                        case enumPipeSectionType.WidthAndHeight:
                            width = ConvertToDouble(standards[0]) / 100;
                            height = ConvertToDouble(standards[1]) / 100;
                            break;
                    }

                    switch (builderItem.LineLayerInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            qdgc = qdgc - height / 2;
                            zdgc = zdgc - height / 2;
                            break;
                        case enumPipelineHeightType.Bottom:
                            qdgc = qdgc + height / 2;
                            zdgc = zdgc + height / 2;
                            break;
                    }
                    I3DLine line = new Line3DSquare(polyline, qdgc, zdgc, width, height, builderItem.Builder.Division);
                    list.Add(line.CreateGeometry());
                }
                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }

        private List<string> GetStandardList(string standard)
        {
            List<string> standardList = new List<string>();
            string[] standards = standard.Split(',', '，');
            for (int i = 0; i < standards.Length; i++)
            {
                string[] subStandards = standards[i].Split('*');
                if (subStandards.Length > 1 && subStandards[1].Length == 1)
                {
                    int count = Convert.ToInt32(subStandards[1]);
                    for (int j = 0; j < count; j++)
                    {
                        standardList.Add(subStandards[0]);
                    }
                }
                else
                {
                    standardList.Add(standards[i]);
                }
            }
            return standardList;
        }

        private IGeometry CreatePointPatch(IFeature pFeature, I3DItem builderItem)
        {
            try
            {
                IPoint oPoint = pFeature.Shape as IPoint;
                if (oPoint == null)
                    return null;
                double z = ConvertToDouble(pFeature.Value[builderItem.IdxDmgcField]);
                if (double.IsNaN(z))
                    z = 0;
                double depth = ConvertToDouble(pFeature.Value[builderItem.IdxJdsdField]);
                if (double.IsNaN(depth))
                    depth = 0.01;

                string gg = ConvertToString(pFeature.Value[builderItem.IdxJgggField]);

                string fsw = ConvertToString(pFeature.Value[builderItem.IdxFswField]).Trim();
                if (builderItem.CylinderSubs.Contains(fsw))
                {
                    if (string.IsNullOrEmpty(gg) || gg == "<空>")
                        gg = "50";
                    if (gg.Contains("*"))
                    {
                        gg = gg.Split('*')[0];
                    }
                    double diameter;
                    if (double.TryParse(gg, out diameter) == false)
                        return null;
                    diameter = diameter / 100;
                    return new Point3DCylinder(oPoint.X, oPoint.Y, z, depth, diameter, _3DBuilder.Division).CreateGeometry();
                }
                else if (builderItem.SquareSubs.Contains(fsw))
                {
                    if (string.IsNullOrEmpty(gg) || gg == "<空>")
                        gg = "50*50";
                    double length, width;
                    if (gg.Split('*').Length <= 0)
                        return null;
                    if (double.TryParse(gg.Split('*')[0], out length) == false)
                        return null;
                    if (gg.Split('*').Length == 1)
                        width = length;
                    else if (double.TryParse(gg.Split('*')[1], out width) == false)
                        return null;
                    length /= 100;
                    width /= 100;
                    double angle = ConvertToDouble(pFeature.Value[builderItem.IdxXzjdField]);
                    if (double.IsNaN(angle))
                        angle = 0;

                    if (builderItem.RotationAngleType == enumRotationAngleType.Angle)
                    {
                        angle = angle * Math.PI / 180;
                    }

                    return new Point3DSquare(oPoint.X, oPoint.Y, z, depth, length, width, angle, _3DBuilder.Division).CreateGeometry();
                }
                if (builderItem.SphereSubs.Contains(fsw))
                {
                    double maxDiameter = GetDiameter(oPoint, builderItem);
                    return new Point3DSphere(oPoint.X, oPoint.Y, z, depth, maxDiameter, _3DBuilder.Division).CreateGeometry();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private double GetDiameter(IPoint point, I3DItem item)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = item.LineLayerInfo.FeatureClass.Search(spatialFilter, false);
            IFeature lineFeature;
            double maxDiameter = 0;
            while ((lineFeature = featureCursor.NextFeature()) != null)
            {
                string gg = ConvertToString(lineFeature.Value[item.IdxGjField]); if (string.IsNullOrEmpty(gg) || gg == "<空>")
                    gg = "50";
                if (gg.Contains("*"))
                {
                    gg = gg.Split('*')[0];
                }
                double diameter;
                if (double.TryParse(gg, out diameter) == false)
                    diameter = 0;
                diameter = diameter / 1000;

                if (maxDiameter < diameter)
                    maxDiameter = diameter;
            }
            return maxDiameter;
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
                return "";
            }
            return obj.ToString();
        }
    }
}
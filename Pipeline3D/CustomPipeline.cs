using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{
    public class CustomPipeline
    {
        private IPolyline _polyline;
        private double _qdgc;
        private double _zdgc;
        private string _standard;   // 规格
        private List<string> _standardList; // 多条管线 管径
        private I3DItem _builderItem;

        public CustomPipeline(IFeature feature, I3DItem builderItem)
        {
            _builderItem = builderItem;
            _polyline = feature.ShapeCopy as IPolyline;

            _qdgc = ConvertToDouble(feature.Value[_builderItem.IdxQdgcField]);
            _zdgc = ConvertToDouble(feature.Value[_builderItem.IdxZdgcField]);

            if (Math.Abs(_qdgc) < 0.001 || double.IsNaN(_qdgc))
                _qdgc = _zdgc;
            if (Math.Abs(_zdgc) < 0.001 || double.IsNaN(_zdgc))
                _zdgc = _qdgc;

            _standard = feature.get_Value(_builderItem.IdxGjField).ToString().Replace(" ", "");
            GetStandardList();
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

        public string Standard
        {
            get { return _standard; }
            set { _standard = value; }
        }

        public List<string> StandardList
        {
            get { return _standardList; }
            set { _standardList = value; }
        }

        private void GetStandardList()
        {
            _standardList = new List<string>();
            string[] standards = _standard.Split(',', '，');
            for (int i = 0; i < standards.Length; i++)
            {
                string[] subStandards = standards[i].Split('*');
                if (subStandards.Length > 1 && subStandards[1].Length == 1)
                {
                    int count = Convert.ToInt32(subStandards[1]);
                    for (int j = 0; j < count; j++)
                    {
                        _standardList.Add(subStandards[0]);
                    }
                }
                else
                {
                    _standardList.Add(standards[i]);
                }
            }
        }

        private SectionStruct CreatePointCollection(int num)
        {
            object missing = Type.Missing;
            string[] standards = _standardList[num].Split('*');
            if (standards.Length > 1)
            {
                double xl = Convert.ToDouble(standards[0]) / 1000;
                double yl = Convert.ToDouble(standards[1]) / 1000;
                if (num == 0)
                    switch (_builderItem.LineLayerInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            switch (_builderItem.LineLayerInfo.SectionType)
                            {
                                case enumPipeSectionType.HeightAndWidth:
                                    _qdgc = _qdgc - xl / 2;
                                    _zdgc = _zdgc - xl / 2;
                                    break;
                                case enumPipeSectionType.WidthAndHeight:
                                    _qdgc = _qdgc - yl / 2;
                                    _zdgc = _zdgc - yl / 2;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            break;
                        case enumPipelineHeightType.Bottom:
                            switch (_builderItem.LineLayerInfo.SectionType)
                            {
                                case enumPipeSectionType.HeightAndWidth:
                                    _qdgc = _qdgc + xl / 2;
                                    _zdgc = _zdgc + xl / 2;
                                    break;
                                case enumPipeSectionType.WidthAndHeight:
                                    _qdgc = _qdgc + yl / 2;
                                    _zdgc = _zdgc + yl / 2;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                return new SectionStruct()
                {
                    XL = xl,
                    YL = yl
                };
            }
            else
            {
                if (string.IsNullOrEmpty(_standardList[num]))
                    _standardList[num] = "100";
                double xl = Convert.ToDouble(_standardList[num]) / 1000;
                if (num == 0)
                    switch (_builderItem.LineLayerInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            _qdgc = _qdgc - xl / 2;
                            _zdgc = _zdgc - xl / 2;
                            break;
                        case enumPipelineHeightType.Bottom:
                            _qdgc = _qdgc + xl / 2;
                            _zdgc = _zdgc + xl / 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                double length = 0;
                for (int i = 1; i <= num; i++)
                {
                    length += (Convert.ToDouble(_standardList[i]) / 1000 + Convert.ToDouble(_standardList[i - 1]) / 1000) / 2;
                }
                return new SectionStruct()
                {
                    XL = xl,
                    YL = Double.NaN
                };
            }
        }

        public IGeometry CreateLinePatch(int num)
        {
            SectionStruct sectionStruct = CreatePointCollection(num);
            if (double.IsNaN(sectionStruct.YL))
            {
                return CreateGeometry(sectionStruct.XL, _polyline, _qdgc, _zdgc);
            }
            else
            {
                return null;
            }
        }

        public IGeometry CreateGeometry(double radius, IPolyline polyline, double qdgc, double zdgc)
        {
            IPointCollection pointCollection = CreatePointCollectionForCircle(radius);
            IVector3D pVectorZ = new Vector3DClass();
            pVectorZ.SetComponents(0, 0, 1);
            IConstructMultiPatch patch = new MultiPatchClass();
            IZAware zAware = pointCollection as IZAware;
            if (zAware == null)
                return null;
            zAware.ZAware = true;
            // 依据管线长度拉伸
            patch.ConstructExtrude(polyline.Length, pointCollection as IGeometry);
            // 依据管线角度旋转
            IVector3D pVector3D = new Vector3DClass();
            pVector3D.SetComponents(polyline.ToPoint.X - polyline.FromPoint.X, polyline.ToPoint.Y - polyline.FromPoint.Y, zdgc - qdgc);
            double rotateAngle = Math.Acos(pVector3D.ZComponent / pVector3D.Magnitude);
            IVector3D vectorAxis = pVectorZ.CrossProduct(pVector3D) as IVector3D;
            ITransform3D transform3D = patch as ITransform3D;
            transform3D.RotateVector3D(vectorAxis, rotateAngle);
            // 平移到指定位置
            transform3D.Move3D(polyline.FromPoint.X, polyline.FromPoint.Y, qdgc);
            return patch as IGeometry;
        }

        private IPointCollection CreatePointCollectionForCircle(double radius)
        {
            object missing = Type.Missing;
            //radius = radius / 2;
            IPointCollection pointCollection = new PolygonClass();
            double angle = 2 * Math.PI / _builderItem.Builder.Division;
            for (int i = 0; i < _builderItem.Builder.Division; i++)
            {
                IPoint point = new PointClass();
                point.X = radius * Math.Cos(angle * i) / 2;
                point.Y = radius * Math.Sin(angle * i) / 2;
                point.Z = 0;
                pointCollection.AddPoint(point, ref missing, ref missing);
            }
            ((IPolygon)pointCollection).Close();
            return pointCollection;
        }


        public IGeometry CreateStartSphere(double radius, int step)
        {
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_polyline.FromPoint.X, _polyline.FromPoint.Y);
            //if (_builderItem.HeightType == enumPipelineHeightType.Top)
            //{
            //    double z1 = _qdgc - radius / 2.0;

            //    centerPoint.Z = z1;
            //}
            //else if (_builderItem.HeightType == enumPipelineHeightType.Bottom)
            //{
            //    double z1 = _qdgc + radius / 2.0;
            //    centerPoint.Z = z1;
            //}
            //else
            //{
                centerPoint.Z = _qdgc;
            //}
            radius = radius * 1.4;
            return CreateSphere(centerPoint, radius / 2.0);
        }

        public IGeometry CreateEndSphere(double radius, int step)
        {
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_polyline.ToPoint.X, _polyline.ToPoint.Y);
            //if (_builderItem.HeightType == enumPipelineHeightType.Top)
            //{
            //    double z1 = _zdgc - radius / 2.0;

            //    centerPoint.Z = z1;
            //}
            //else if (_builderItem.HeightType == enumPipelineHeightType.Bottom)
            //{
            //    double z1 = _zdgc + radius / 2.0;
            //    centerPoint.Z = z1;
            //}
            //else
            //{
                centerPoint.Z = _zdgc;
            //}
            radius = radius * 1.4;
            return CreateSphere(centerPoint, radius / 2.0);
        }

        public IGeometry CreateSphere(IPoint centerPoint, double radius, double minLon = 0.0, double maxLon = 360.0, double minLat = -90.0, double maxLat = 90.0, double stepAngle = 18.0, bool bSmooth = false, bool bFlipS = false, bool bFlipT = false)
        {

            IMultiPatch patch = new MultiPatchClass();
            IGeometryCollection pGCol = patch as IGeometryCollection;
            IGeometry2 pGeom;
            IPoint pt;
            IPointCollection pStrip;
            IVector3D pVector = new Vector3DClass();
            IEncode3DProperties pGE = new GeometryEnvironmentClass();
            double xStep = (maxLon - minLon) / stepAngle;
            double yStep = (maxLat - minLat) / (stepAngle / 2.0);
            double lonRange = maxLon - minLon;
            double latRange = maxLat - minLat;
            object missing = Type.Missing;
            double lon = minLon;
            while (lon < maxLon)
            {
                pStrip = new TriangleStripClass();
                double lat = minLat;
                while (lat < maxLat)
                {
                    double azi = DegreesToRadians(lon);
                    double inc = DegreesToRadians(lat);
                    pVector.PolarSet(-azi, inc, radius);
                    pt = new PointClass();
                    pt.X = centerPoint.X + pVector.XComponent;
                    pt.Y = centerPoint.Y + pVector.YComponent;
                    pt.Z = centerPoint.Z + pVector.ZComponent;
                    double s = (lon - minLon) / lonRange;
                    if (bFlipS)
                    {
                        s = 1 + (s * -1);
                    }
                    if (s <= 0) s = 0.001;
                    else if (s >= 1)
                        s = 0.999;

                    double t = (maxLat - lat) / latRange;
                    if (bFlipT)
                        t = 1 + (t * -1);
                    if (t <= 0) t = 0.001;
                    else if (t >= 1)
                        t = 0.999;

                    double m = 0.0;
                    pGE.PackTexture2D(s, t, out m);
                    if (bSmooth)
                    {
                        pVector.Normalize();
                        pGE.PackNormal(pVector, out m);
                    }
                    pt.M = m;

                    pStrip.AddPoint(pt, ref missing, ref missing);
                    if ((lat != -90) && (lat != 90))
                    {
                        azi = (lon + xStep) * Math.PI / 180.00;
                        inc = lat * Math.PI / 180.00;
                        pVector.PolarSet(-azi, inc, radius);
                        pt = new PointClass();
                        pt.X = centerPoint.X + pVector.XComponent;
                        pt.Y = centerPoint.Y + pVector.YComponent;
                        pt.Z = centerPoint.Z + pVector.ZComponent;
                        s = (lon + xStep - minLon) / lonRange;
                        if (bFlipS)
                        {
                            s = 1 + (s * -1);
                        }
                        if (s <= 0) s = 0.001;
                        else if (s >= 1)
                            s = 0.999;

                        t = (maxLat - lat) / latRange;
                        if (bFlipT)
                            t = 1 + (t * -1);
                        if (t <= 0) t = 0.001;
                        else if (t >= 1)
                            t = 0.999;

                        m = 0.0;
                        pGE.PackTexture2D(s, t, out m);
                        if (bSmooth)
                        {
                            pVector.Normalize();
                            pGE.PackNormal(pVector, out m);
                        }
                        pt.M = m;

                        pStrip.AddPoint(pt, ref missing, ref missing);
                    }
                    lat = lat + yStep;
                }
                pGeom = pStrip as IGeometry2;
                pGCol.AddGeometry(pGeom, ref missing, ref missing);
                lon = lon + xStep;
            }

            IMAware pMAware = patch as IMAware;
            pMAware.MAware = true;
            return patch;

        }

        public double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.00;
        }
    }

    public struct SectionStruct
    {
        public double XL;
        public double YL;
    }
}

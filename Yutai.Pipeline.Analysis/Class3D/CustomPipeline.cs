using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Class3D
{
    public class CustomPipeline
    {
        private IPoint _startPoint; // 起点坐标
        private IPoint _endPoint;   // 终点坐标
        private string _standard;   // 规格
        private List<string> _standardList; // 多条管线 管径
        private IBasicLayerInfo _pipeClassInfo;
        private IPoint _targetPoint;    // 三维模型移动到的目标位置
        private int _division;

        public CustomPipeline(IFeature feature, IBasicLayerInfo pipeClassInfo, int startHeightIndex, int endHeightIndex, int startDepthIndex, int endDepthIndex, int standardIndex, int division)
        {
            _pipeClassInfo = pipeClassInfo;
            IPolyline polyline = feature.ShapeCopy as IPolyline;
            _startPoint = new PointClass
            {
                X = polyline.FromPoint.X,
                Y = polyline.FromPoint.Y
            };
            _endPoint = new PointClass
            {
                X = polyline.ToPoint.X,
                Y = polyline.ToPoint.Y
            };
            _division = division;
            if (pipeClassInfo.DepthType == enumPipelineDepthType.Absolute)
            {
                _startPoint.Z = feature.Value[startDepthIndex] != DBNull.Value ? Convert.ToDouble(feature.Value[startDepthIndex]) : 0;
                _endPoint.Z = feature.Value[endDepthIndex] != DBNull.Value ? Convert.ToDouble(feature.Value[endDepthIndex]) : 0;
            }
            else
            {
                if (feature.Value[startHeightIndex] != DBNull.Value && feature.Value[startDepthIndex] != DBNull.Value)
                {
                    _startPoint.Z = Convert.ToDouble(feature.Value[startHeightIndex]) - Convert.ToDouble(feature.Value[startDepthIndex]);
                }
                else
                {
                    _startPoint.Z = feature.Value[startHeightIndex] != DBNull.Value ? Convert.ToDouble(feature.Value[startHeightIndex]) : 0;
                }
                if (feature.Value[endHeightIndex] != DBNull.Value && feature.Value[endDepthIndex] != DBNull.Value)
                {
                    _endPoint.Z = Convert.ToDouble(feature.Value[endHeightIndex]) - Convert.ToDouble(feature.Value[endDepthIndex]);
                }
                else
                {
                    _endPoint.Z = feature.Value[endHeightIndex] != DBNull.Value ? Convert.ToDouble(feature.Value[endHeightIndex]) : 0;
                }
            }

            if (_startPoint.Z == 0)
                _startPoint.Z = _endPoint.Z;
            if (_endPoint.Z == 0)
                _endPoint.Z = _startPoint.Z;

            _standard = feature.get_Value(standardIndex).ToString().Replace(" ", "");
            GetStandardList();
        }

        public IPoint StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public IPoint EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
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

        public IBasicLayerInfo ClassInfo
        {
            get { return _pipeClassInfo; }
            set { _pipeClassInfo = value; }
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

        private void GetTargetPoint(double length)
        {
            double x = _startPoint.X - length * Math.Sin(Math.Atan2(_endPoint.Y - _startPoint.Y, _endPoint.X - _startPoint.X));
            double y = _startPoint.Y + length * Math.Cos(Math.Atan2(_endPoint.Y - _startPoint.Y, _endPoint.X - _startPoint.X));

            _targetPoint = new PointClass
            {
                X = x,
                Y = y,
                Z = _startPoint.Z
            };
        }

        private IPointCollection CreatePointCollectionForRectangle(double xl, double yl, ref object missing)
        {
            IPointCollection pointCollection = new PolygonClass();
            IPoint pnt = new PointClass();
            pnt.X = -xl / 2;
            pnt.Y = -yl / 2;
            pnt.Z = 0;
            pointCollection.AddPoint(pnt, ref missing, ref missing);
            pnt = new PointClass();
            pnt.X = -xl / 2;
            pnt.Y = yl / 2;
            pnt.Z = 0;
            pointCollection.AddPoint(pnt, ref missing, ref missing);
            pnt = new PointClass();
            pnt.X = xl / 2;
            pnt.Y = yl / 2;
            pnt.Z = 0;
            pointCollection.AddPoint(pnt, ref missing, ref missing);
            pnt = new PointClass();
            pnt.X = xl / 2;
            pnt.Y = -yl / 2;
            pnt.Z = 0;
            pointCollection.AddPoint(pnt, ref missing, ref missing);
            ((IPolygon)pointCollection).Close();
            return pointCollection;
        }

        private IPointCollection CreatePointCollectionForCircle(double radius, ref object missing)
        {
            radius = radius / 2;
            IPointCollection pointCollection = new PolygonClass();
            double angle = 2 * Math.PI / _division;
            for (int i = 0; i < _division; i++)
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

        private IPointCollection CreatePointCollection(int num)
        {
            object missing = Type.Missing;
            string[] standards = _standardList[num].Split('*');
            if (standards.Length > 1)
            {
                double xl = Convert.ToDouble(standards[0]) / 1000;
                double yl = Convert.ToDouble(standards[1]) / 1000;
                if (num == 0)
                    switch (_pipeClassInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            switch (_pipeClassInfo.SectionType)
                            {
                                case enumPipeSectionType.HeightAndWidth:
                                    _startPoint.Z = _startPoint.Z - xl / 2;
                                    _endPoint.Z = _endPoint.Z - xl / 2;
                                    break;
                                case enumPipeSectionType.WidthAndHeight:
                                    _startPoint.Z = _startPoint.Z - yl / 2;
                                    _endPoint.Z = _endPoint.Z - yl / 2;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            GetTargetPoint(yl / 2);
                            break;
                        case enumPipelineHeightType.Bottom:
                            switch (_pipeClassInfo.SectionType)
                            {
                                case enumPipeSectionType.HeightAndWidth:
                                    _startPoint.Z = _startPoint.Z + xl / 2;
                                    _endPoint.Z = _endPoint.Z + xl / 2;
                                    break;
                                case enumPipeSectionType.WidthAndHeight:
                                    _startPoint.Z = _startPoint.Z + yl / 2;
                                    _endPoint.Z = _endPoint.Z + yl / 2;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            GetTargetPoint(xl / 2);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                return CreatePointCollectionForRectangle(xl, yl, ref missing);
            }
            else
            {
                if (string.IsNullOrEmpty(_standardList[num]))
                    _standardList[num] = "100";
                double xl = Convert.ToDouble(_standardList[num]) / 1000;
                if (num == 0)
                    switch (_pipeClassInfo.HeightType)
                    {
                        case enumPipelineHeightType.Top:
                            _startPoint.Z = _startPoint.Z - xl / 2;
                            _endPoint.Z = _endPoint.Z - xl / 2;
                            break;
                        case enumPipelineHeightType.Bottom:
                            _startPoint.Z = _startPoint.Z + xl / 2;
                            _endPoint.Z = _endPoint.Z + xl / 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                double length = 0;
                for (int i = 1; i <= num; i++)
                {
                    length += (Convert.ToDouble(_standardList[i]) / 1000 + Convert.ToDouble(_standardList[i - 1]) / 1000) / 2;
                }
                GetTargetPoint(length);
                return CreatePointCollectionForCircle(xl, ref missing);
            }
        }

        public IGeometry CreateLinePatch(int num)
        {
            IPointCollection pointCollection = CreatePointCollection(num);
            IVector3D pVectorZ = new Vector3DClass();
            pVectorZ.SetComponents(0, 0, 1);
            IConstructMultiPatch patch = new MultiPatchClass();
            IZAware zAware = pointCollection as IZAware;
            if (zAware == null)
                return null;
            zAware.ZAware = true;
            // 管线长度
            double length = Math.Sqrt(Math.Pow((_startPoint.X - _endPoint.X), 2) + Math.Pow((_startPoint.Y - _endPoint.Y), 2) + Math.Pow((_startPoint.Z - _endPoint.Z), 2));
            // 依据管线长度拉伸
            patch.ConstructExtrude(length, pointCollection as IGeometry);
            // 依据管线角度旋转
            IVector3D pVector3D = new Vector3DClass();
            pVector3D.SetComponents(_endPoint.X - _startPoint.X, _endPoint.Y - _startPoint.Y, _endPoint.Z - _startPoint.Z);

            double rotateAngle = Math.Acos(pVector3D.ZComponent / pVector3D.Magnitude);
            IVector3D vectorAxis = pVectorZ.CrossProduct(pVector3D) as IVector3D;
            ITransform3D transform3D = patch as ITransform3D;
            transform3D.RotateVector3D(vectorAxis, rotateAngle);
            // 平移到指定位置
            transform3D.Move3D(_targetPoint.X, _targetPoint.Y, _targetPoint.Z);
            return patch as IGeometry;
        }

        public double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.00;
        }

        public IGeometry CreateStartSphere(double radius, int step)
        {
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_startPoint.X, _startPoint.Y);
            if (_pipeClassInfo.HeightType == enumPipelineHeightType.Top)
            {
                double z1 = _startPoint.Z - radius / 2.0;

                centerPoint.Z = z1;
            }
            else if (_pipeClassInfo.HeightType == enumPipelineHeightType.Bottom)
            {
                double z1 = _startPoint.Z + radius / 2.0;
                centerPoint.Z = z1;
            }
            else
            {
                centerPoint.Z = _startPoint.Z;
            }
            radius = radius * 1.4;
            return CreateSphere(centerPoint, radius);
        }

        public IGeometry CreateEndSphere(double radius, int step)
        {
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_endPoint.X, _endPoint.Y);
            if (_pipeClassInfo.HeightType == enumPipelineHeightType.Top)
            {
                double z1 = _endPoint.Z - radius / 2.0;

                centerPoint.Z = z1;
            }
            else if (_pipeClassInfo.HeightType == enumPipelineHeightType.Bottom)
            {
                double z1 = _endPoint.Z + radius / 2.0;
                centerPoint.Z = z1;
            }
            else
            {
                centerPoint.Z = _endPoint.Z;
            }
            radius = radius * 1.4;
            return CreateSphere(centerPoint, radius);
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
                    double s = (lon - minLon)/lonRange;
                    if (bFlipS)
                    {
                        s = 1 + (s*-1);
                    }
                    if (s <= 0) s = 0.001;
                    else if (s >= 1)
                        s = 0.999;

                    double t = (maxLat - lat)/latRange;
                    if (bFlipT)
                        t = 1 + (t*-1);
                    if (t <= 0) t = 0.001;
                    else if (t >= 1)
                        t = 0.999;

                    double m = 0.0;
                    pGE.PackTexture2D(s,t,out m);
                    if (bSmooth)
                    {
                        pVector.Normalize();
                        pGE.PackNormal(pVector,out m);
                    }
                    pt.M = m;

                    pStrip.AddPoint(pt,ref missing, ref missing);
                    if ((lat != -90) && (lat != 90))
                    {
                        azi = (lon + xStep)*Math.PI/180.00;
                        inc = lat*Math.PI/180.00;
                        pVector.PolarSet(-azi, inc, radius);
                        pt = new PointClass();
                        pt.X = centerPoint.X + pVector.XComponent;
                        pt.Y = centerPoint.Y + pVector.YComponent;
                        pt.Z = centerPoint.Z + pVector.ZComponent;
                         s = (lon+xStep - minLon) / lonRange;
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
    }
}
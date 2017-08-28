﻿using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{
    public class CustomPipeline
    {
        private IPoint _startPoint; // 起点坐标
        private IPoint _endPoint;   // 终点坐标
        private string _standard;   // 规格
        private List<string> _standardList; // 多条管线 管径
        private PipeClassInfo _pipeClassInfo;
        private IPoint _targetPoint;    // 三维模型移动到的目标位置

        public CustomPipeline(IFeature feature, PipeClassInfo pipeClassInfo)
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

            if (pipeClassInfo.ElevationType == PipeElevationType.Absolute)
            {
                _startPoint.Z = feature.Value[pipeClassInfo.StartUnderGroundElevationField.Index] != DBNull.Value ? Convert.ToDouble(feature.Value[pipeClassInfo.StartUnderGroundElevationField.Index]) : 0;
                _endPoint.Z = feature.Value[pipeClassInfo.EndUnderGroundElevationField.Index] != DBNull.Value ? Convert.ToDouble(feature.Value[pipeClassInfo.EndUnderGroundElevationField.Index]) : 0;
            }
            else
            {
                if (feature.Value[pipeClassInfo.StartGroundElevationField.Index] != DBNull.Value && feature.Value[pipeClassInfo.StartUnderGroundElevationField.Index] != DBNull.Value)
                {
                    _startPoint.Z = Convert.ToDouble(feature.Value[pipeClassInfo.StartGroundElevationField.Index]) - Convert.ToDouble(feature.Value[pipeClassInfo.StartUnderGroundElevationField.Index]);
                }
                else
                {
                    _startPoint.Z = feature.Value[pipeClassInfo.StartGroundElevationField.Index] != DBNull.Value ? Convert.ToDouble(feature.Value[pipeClassInfo.StartGroundElevationField.Index]) : 0;
                }
                if (feature.Value[pipeClassInfo.EndGroundElevationField.Index] != DBNull.Value && feature.Value[pipeClassInfo.EndUnderGroundElevationField.Index] != DBNull.Value)
                {
                    _endPoint.Z = Convert.ToDouble(feature.Value[pipeClassInfo.EndGroundElevationField.Index]) - Convert.ToDouble(feature.Value[pipeClassInfo.EndUnderGroundElevationField.Index]);
                }
                else
                {
                    _endPoint.Z = feature.Value[pipeClassInfo.EndGroundElevationField.Index] != DBNull.Value ? Convert.ToDouble(feature.Value[pipeClassInfo.EndGroundElevationField.Index]) : 0;
                }
            }

            if (_startPoint.Z == 0)
                _startPoint.Z = _endPoint.Z;
            if (_endPoint.Z == 0)
                _endPoint.Z = _startPoint.Z;

            _standard = feature.get_Value(pipeClassInfo.StandardField.Index).ToString().Replace(" ","");
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

        public PipeClassInfo ClassInfo
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
            double angle = 2 * Math.PI / _pipeClassInfo.Division;
            for (int i = 0; i < _pipeClassInfo.Division; i++)
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
                    switch (_pipeClassInfo.DepthType)
                    {
                        case PipeDepthType.Top:
                            switch (_pipeClassInfo.SectionType)
                            {
                                case PipeSectionType.HeightAndWidth:
                                    _startPoint.Z = _startPoint.Z - xl / 2;
                                    _endPoint.Z = _endPoint.Z - xl / 2;
                                    break;
                                case PipeSectionType.WidthAndHeight:
                                    _startPoint.Z = _startPoint.Z - yl / 2;
                                    _endPoint.Z = _endPoint.Z - yl / 2;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            GetTargetPoint(yl / 2);
                            break;
                        case PipeDepthType.Bottom:
                            switch (_pipeClassInfo.SectionType)
                            {
                                case PipeSectionType.HeightAndWidth:
                                    _startPoint.Z = _startPoint.Z + xl / 2;
                                    _endPoint.Z = _endPoint.Z + xl / 2;
                                    break;
                                case PipeSectionType.WidthAndHeight:
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
                    switch (_pipeClassInfo.DepthType)
                    {
                        case PipeDepthType.Top:
                            _startPoint.Z = _startPoint.Z - xl / 2;
                            _endPoint.Z = _endPoint.Z - xl / 2;
                            break;
                        case PipeDepthType.Bottom:
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
    }
}

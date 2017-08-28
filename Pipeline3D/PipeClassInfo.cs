using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{
    public enum PipeDepthType
    {
        Top,
        Bottom
    }

    public enum PipeSectionType
    {
        HeightAndWidth,
        WidthAndHeight
    }

    public class PipeClassInfo
    {
        public string PointGroundElevString = "地面高程";
        public string PointUnderGroundElevString = "井底埋深";
        public string PointStandardString = "地面点规格";
        public string PointMaterialString = "地面点材质";
        public string PointJustString = "附属物子类";
        public string LineStartGroundElevString = "起点高程";
        public string LineStartUnderGroundElevString = "起点埋深";
        public string LineEndGroundElevString = "终点高程";
        public string LineEndUnderGroundElevString = "终点埋深";
        public string LineStandardString = "管径";
        public string LineMaterialString = "材质";
        private PipeClassType _classType;
        private IFeatureClass _pipeClass;
        private PipeSpecialFieldInfo _standardField;
        private PipeSpecialFieldInfo _materialField;
        private PipeSpecialFieldInfo _justPointTypeField;
        private PipeElevationType _elevationType;
        private int _division;
        private PipeSpecialFieldInfo _endGroundElevationField;
        private PipeSpecialFieldInfo _endUnderGroundElevationField;
        private PipeSpecialFieldInfo _startGroundElevationField;
        private PipeSpecialFieldInfo _startUnderGroundElevationField;
        private PipeSpecialFieldInfo _pointElevationField;
        public PipeSpecialFieldInfo PointElevationField
        {
            get { return _pointElevationField; }
            set { _pointElevationField = value; }
        }

        public string ClassPath { get; set; }
        public PipeDepthType DepthType { get; set; }
        public PipeSectionType SectionType { get; set; }
        public int Division
        {
            get { return _division; }
            set { _division = value; }
        }

        public PipeClassType ClassType
        {
            get { return _classType; }
            set
            {
                _classType = value;
            }
        }

        public PipeClassInfo(IFeatureClass featureClass)
        {
            _division = 12;
            _elevationType = PipeElevationType.Relative;
            PipeClass = featureClass;
            if (_classType == PipeClassType.Point)
            {
                SetSpecialField(PointGroundElevString, PipeFieldType.StartGroundElevation);
                SetSpecialField(PointUnderGroundElevString, PipeFieldType.StartUnderGroundElevation);
                SetSpecialField(PointStandardString, PipeFieldType.Standard);
                SetSpecialField(PointMaterialString, PipeFieldType.Material);
                SetSpecialField(PointJustString, PipeFieldType.JustPoint);
            }
            else
            {
                SetSpecialField(LineStartGroundElevString, PipeFieldType.StartGroundElevation);
                SetSpecialField(LineStartUnderGroundElevString, PipeFieldType.StartUnderGroundElevation);
                SetSpecialField(LineEndGroundElevString, PipeFieldType.EndGroundElevation);
                SetSpecialField(LineEndUnderGroundElevString, PipeFieldType.EndUnderGroundElevation);
                SetSpecialField(LineStandardString, PipeFieldType.Standard);
                SetSpecialField(LineMaterialString, PipeFieldType.Material);
                
            }
        }

        public PipeClassInfo(IFeatureClass pipeClass, PipeElevationType elevationType,
            string groundElevationFieldName0, string underGroundElevationFieldName0, string groundElevationFieldName1, string underGroundElevationFieldName1, string standFieldName,
            string materialFieldName, string justPointTypeFieldName)
        {
            PipeClass = pipeClass;
            ElevationType = elevationType;
            SetSpecialField(groundElevationFieldName0, PipeFieldType.StartGroundElevation);
            SetSpecialField(underGroundElevationFieldName0, PipeFieldType.StartUnderGroundElevation);
            SetSpecialField(groundElevationFieldName1, PipeFieldType.EndGroundElevation);
            SetSpecialField(underGroundElevationFieldName1, PipeFieldType.EndUnderGroundElevation);
            SetSpecialField(standFieldName, PipeFieldType.Standard);
            SetSpecialField(materialFieldName, PipeFieldType.Material);
            SetSpecialField(justPointTypeFieldName, PipeFieldType.JustPoint);
            _division = 12;
        }

        public PipeClassInfo(IFeatureClass pipeClass, string groundElevationFieldName)
        {
            PipeClass = pipeClass;
            PointElevationField = new PipeSpecialFieldInfo()
            {
                Name = groundElevationFieldName,
                Index = pipeClass.FindField(groundElevationFieldName)
            };
        }

        public PipeClassInfo(IFeatureClass pipeClass, string lineStartUnderGroundElevString,
            string lineEndUnderGroundElevString, string lineStartGroundElevString, string lineEndGroundElevString)
        {
            PipeClass = pipeClass;
            StartGroundElevationField = new PipeSpecialFieldInfo
            {
                Name = lineStartGroundElevString,
                Index = pipeClass.FindField(lineStartGroundElevString)
            };
            EndGroundElevationField = new PipeSpecialFieldInfo
            {
                Name = lineEndGroundElevString,
                Index = pipeClass.FindField(lineEndGroundElevString)
            };
            StartUnderGroundElevationField = new PipeSpecialFieldInfo
            {
                Name = lineStartUnderGroundElevString,
                Index = pipeClass.FindField(lineStartUnderGroundElevString)
            };
            EndUnderGroundElevationField = new PipeSpecialFieldInfo
            {
                Name = lineEndUnderGroundElevString,
                Index = pipeClass.FindField(lineEndUnderGroundElevString)
            };
        }

        public IFeatureClass PipeClass
        {
            get { return _pipeClass; }
            set
            {
                _pipeClass = value;
                IDataset pDS = _pipeClass as IDataset;
                ClassPath = pDS.Workspace.PathName + "\\" + pDS.BrowseName;
                if (_pipeClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    _classType = PipeClassType.Line;
                }
                else if (_pipeClass.ShapeType == esriGeometryType.esriGeometryMultiPatch)
                {
                    _classType = PipeClassType.MultiPatch;
                }
                else if (_pipeClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    _classType = PipeClassType.Point;
                }
                else
                {
                    _classType = PipeClassType.None;
                    _pipeClass = null;
                }
            }
        }

        public PipeElevationType ElevationType
        {
            get { return _elevationType; }
            set { _elevationType = value; }
        }

        public PipeSpecialFieldInfo EndGroundElevationField
        {
            get { return _endGroundElevationField; }
            set { _endGroundElevationField = value; }
        }

        public PipeSpecialFieldInfo EndUnderGroundElevationField
        {
            get { return _endUnderGroundElevationField; }
            set { _endUnderGroundElevationField = value; }
        }

        public PipeSpecialFieldInfo StartGroundElevationField
        {
            get { return _startGroundElevationField; }
            set { _startGroundElevationField = value; }
        }

        public PipeSpecialFieldInfo StartUnderGroundElevationField
        {
            get { return _startUnderGroundElevationField; }
            set { _startUnderGroundElevationField = value; }
        }

        public PipeSpecialFieldInfo StandardField
        {
            get { return _standardField; }
            set { _standardField = value; }
        }

        public PipeSpecialFieldInfo MaterialField
        {
            get { return _materialField; }
            set { _materialField = value; }
        }

        public PipeSpecialFieldInfo JustPointTypeField
        {
            get { return _justPointTypeField; }
            set { _justPointTypeField = value; }
        }

       

        public void SetSpecialField(string fieldName, PipeFieldType fieldType)
        {
            if (string.IsNullOrEmpty(fieldName)) return;
            PipeSpecialFieldInfo info=new PipeSpecialFieldInfo()
            {
                Name = fieldName,
                Index = _pipeClass.FindField(fieldName)
            };

            if (info.Index < 0)
            {
                info.Name = "";
            }
            switch (fieldType)
            {
                case PipeFieldType.StartGroundElevation:
                    _startGroundElevationField = info;
                    break;
                case PipeFieldType.StartUnderGroundElevation:
                    _startUnderGroundElevationField = info;
                    break;
                case PipeFieldType.EndGroundElevation:
                    _endGroundElevationField = info;
                    break;
                case PipeFieldType.EndUnderGroundElevation:
                    _endUnderGroundElevationField = info;
                    break;
                case PipeFieldType.Material:
                    _materialField = info;
                    break;
                case PipeFieldType.Standard:
                    _standardField = info;
                    break;
                case PipeFieldType.JustPoint:
                    _justPointTypeField = info;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fieldType");
            }
        }
    }
}

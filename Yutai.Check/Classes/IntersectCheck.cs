using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Check.Classes
{
    class IntersectCheck : IGeometryCheck
    {
        private IDictionary<string, int> _gjFieldIndexDictionary;
        private IDictionary<string, int> _qdgcFieldIndexDictionary;
        private IDictionary<string, int> _zdgcFieldIndexDictionary;
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public IntersectCheck(IDataCheck dataCheck)
        {
            _dataCheck = dataCheck;
            _gjFieldIndexDictionary = new Dictionary<string, int>();
            _qdgcFieldIndexDictionary = new Dictionary<string, int>();
            _zdgcFieldIndexDictionary = new Dictionary<string, int>();
        }

        public BackgroundWorker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public List<FeatureItem> Check()
        {
            List<FeatureItem> list = new List<FeatureItem>();

            foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                if (_dataCheck.CheckPipelineList.Contains(pipelineLayer.Code))
                {
                    IBasicLayerInfo lineBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (lineBasicLayerInfo?.FeatureClass == null)
                        continue;
                    string gjFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);
                    string qdgcFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC);
                    string zdgcFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC);
                    int idxGj = lineBasicLayerInfo.FeatureClass.FindField(gjFieldName);
                    int idxQdgc = lineBasicLayerInfo.FeatureClass.FindField(qdgcFieldName);
                    int idxZdgc = lineBasicLayerInfo.FeatureClass.FindField(zdgcFieldName);
                    if (idxGj < 0 || idxQdgc < 0 || idxZdgc < 0)
                    {
                        list.Add(new FeatureItem()
                        {
                            PipelineName = pipelineLayer.Name,
                            PipeLayerName = lineBasicLayerInfo.FeatureClass.AliasName,
                            CheckItem = "流向检查",
                            ErrDesc = "字段配置错误 字段 " + (idxGj < 0 ? gjFieldName + " " : "") + (idxQdgc < 0 ? qdgcFieldName + " " : "") + (idxZdgc < 0 ? zdgcFieldName + " " : "") + "不存在",
                        });
                    }
                    else
                    {
                        _gjFieldIndexDictionary.Add(pipelineLayer.Code, idxGj);
                        _qdgcFieldIndexDictionary.Add(pipelineLayer.Code, idxQdgc);
                        _zdgcFieldIndexDictionary.Add(pipelineLayer.Code, idxZdgc);
                    }
                }
            }

            foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                if (_gjFieldIndexDictionary.ContainsKey(pipelineLayer.Code))
                {
                    IBasicLayerInfo lineBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (lineBasicLayerInfo?.FeatureClass == null)
                        continue;

                    string gjFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);
                    string qdgcFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC);
                    string zdgcFieldName = lineBasicLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC);
                    list.AddRange(Check(pipelineLayer.Name, pipelineLayer.Code, lineBasicLayerInfo.FeatureClass, gjFieldName, qdgcFieldName, zdgcFieldName));
                }
            }

            return list;
        }

        private List<FeatureItem> Check(string pipelineName, string pipelineCode, IFeatureClass featureClass, string gjFieldName, string qdgcFieldName, string zdgcFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();

            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                IPolyline polyline = feature.Shape as IPolyline;
                if (polyline == null || polyline.IsEmpty)
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "交叉检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }
                double gj = CommonHelper.ConvertToDouble(feature.Value[_gjFieldIndexDictionary[pipelineCode]]);
                double qdgc = CommonHelper.ConvertToDouble(feature.Value[_qdgcFieldIndexDictionary[pipelineCode]]);
                double zdgc = CommonHelper.ConvertToDouble(feature.Value[_zdgcFieldIndexDictionary[pipelineCode]]);
                if (double.IsNaN(gj) || double.IsNaN(qdgc) || double.IsNaN(zdgc))
                {
                    list.Add(new FeatureItem(feature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = featureClass.AliasName,
                        CheckItem = "交叉检查",
                        ErrDesc = "字段属性错误 字段 " + (double.IsNaN(gj) ? gjFieldName + " " : "") + (double.IsNaN(qdgc) ? qdgcFieldName + " " : "") + (double.IsNaN(zdgc) ? zdgcFieldName + " " : "") + "属性为空或格式错误",
                    });
                    continue;
                }
                foreach (IPipelineLayer pipelineLayer in _dataCheck.PipelineLayers)
                {
                    if (_worker != null && _worker.CancellationPending)
                        return list;
                    if (_gjFieldIndexDictionary.ContainsKey(pipelineLayer.Code))
                    {
                        IBasicLayerInfo lineBasicLayerInfo = pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                        if (lineBasicLayerInfo?.FeatureClass == null)
                            continue;
                        string message = Check(pipelineName, feature.OID, polyline, gj, qdgc, zdgc,
                            lineBasicLayerInfo.FeatureClass, _gjFieldIndexDictionary[pipelineLayer.Code],
                            _qdgcFieldIndexDictionary[pipelineLayer.Code], _zdgcFieldIndexDictionary[pipelineLayer.Code]);
                        if (string.IsNullOrWhiteSpace(message))
                            continue;
                        list.Add(new FeatureItem(feature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = featureClass.AliasName,
                            CheckItem = "交叉检查",
                            ErrDesc = message
                        });
                    }
                }
            }

            return list;
        }

        private string Check(string pipelineName, int oid, IPolyline polyline, double gj, double qdgc, double zdgc, IFeatureClass featureClass, int idxGj, int idxQdgc, int idxZdgc)
        {
            string message = "";
            ITopologicalOperator topologicalOperator = polyline as ITopologicalOperator;
            if (topologicalOperator == null)
            {
                return message;
            }
            IPolygon polygon = topologicalOperator.Buffer(gj * 2) as IPolygon;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = polygon;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (feature.OID == oid)
                    continue;
                IPolyline polyline2 = feature.Shape as IPolyline;
                if (polyline2 == null || polyline2.IsEmpty)
                {
                    message += $"[{pipelineName}:{feature.OID} 要素几何图形为空]";
                    continue;
                }
                double gj2 = CommonHelper.ConvertToDouble(feature.Value[idxGj]);
                double qdgc2 = CommonHelper.ConvertToDouble(feature.Value[idxQdgc]);
                double zdgc2 = CommonHelper.ConvertToDouble(feature.Value[idxZdgc]);
                if (double.IsNaN(gj2) || double.IsNaN(qdgc2) || double.IsNaN(zdgc2))
                {
                    message += $"[{pipelineName}:字段属性错误]";
                    continue;
                }
                if (IsUnusal(polyline, gj, qdgc, zdgc, polyline2, gj2, qdgc2, zdgc2))
                {
                    message += $"[{pipelineName}:管线可能相交]";
                }
            }
            Marshal.ReleaseComObject(featureCursor);
            return message;
        }

        private bool IsUnusal(IPolyline polyline, double gj, double qdgc, double zdgc, IPolyline polyline2, double gj2, double qdgc2, double zdgc2)
        {
            IGeometry geometry = CreateGeometry(gj, polyline, qdgc, zdgc);
            IGeometry geometry2 = CreateGeometry(gj2, polyline2, qdgc2, zdgc2);
            IProximityOperator3D proximityOperator3D = geometry as IProximityOperator3D;
            double distance3D = proximityOperator3D.ReturnDistance3D(geometry2);
            return distance3D <= 0;
        }

        public static IGeometry CreateGeometry(double radius, IPolyline polyline, double qdgc, double zdgc)
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

        private static IPointCollection CreatePointCollectionForCircle(double radius)
        {
            object missing = Type.Missing;
            radius = radius / 2;
            IPointCollection pointCollection = new PolygonClass();
            double angle = 2 * Math.PI / 36;
            for (int i = 0; i < 36; i++)
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
    }
}

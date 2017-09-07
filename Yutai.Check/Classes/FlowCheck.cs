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
    class FlowCheck : IGeometryCheck
    {
        private IDataCheck _dataCheck;
        private BackgroundWorker _worker;

        public FlowCheck(IDataCheck dataCheck)
        {
            _dataCheck = dataCheck;
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
                    IBasicLayerInfo pointLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Point);
                    IBasicLayerInfo lineLayerInfo =
                        pipelineLayer.Layers.FirstOrDefault(c => c.DataType == enumPipelineDataType.Line);
                    if (pointLayerInfo == null || lineLayerInfo == null || pointLayerInfo.FeatureClass == null || lineLayerInfo.FeatureClass == null)
                        continue;
                    list.AddRange(Check(pipelineLayer.Name, lineLayerInfo.FeatureClass, lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC), lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC), lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ), lineLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.LX)));
                }
            }

            return list;
        }

        private List<FeatureItem> Check(string pipelineName, IFeatureClass lineFeatureClass, string qdgcFieldName, string zdgcFieldName, string gjFieldName, string lxFieldName)
        {
            List<FeatureItem> list = new List<FeatureItem>();
            int idxQdgc = lineFeatureClass.FindField(qdgcFieldName);
            int idxZdgc = lineFeatureClass.FindField(zdgcFieldName);
            int idxGj = lineFeatureClass.FindField(gjFieldName);
            int idxLx = lineFeatureClass.FindField(lxFieldName);
            if (idxQdgc < 0 || idxZdgc < 0 || idxGj < 0)
            {
                list.Add(new FeatureItem()
                {
                    PipelineName = pipelineName,
                    PipeLayerName = lineFeatureClass.AliasName,
                    CheckItem = "流向检查",
                    ErrDesc = "字段配置错误 字段 " + (idxQdgc < 0 ? qdgcFieldName + " " : "") + (idxZdgc < 0 ? zdgcFieldName + " " : "") + (idxGj < 0 ? gjFieldName + " " : "") + (idxLx < 0 ? lxFieldName + " " : "") + "不存在",
                });
                return list;
            }
            IFeatureCursor lineFeatureCursor = lineFeatureClass.Search(null, false);
            IFeature lineFeature;
            while ((lineFeature = lineFeatureCursor.NextFeature()) != null)
            {
                if (_worker != null && _worker.CancellationPending)
                    return list;
                IPolyline polyline = lineFeature.Shape as IPolyline;
                if (polyline == null || polyline.IsEmpty)
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "流向检查",
                        ErrDesc = "几何图形为空",
                    });
                    continue;
                }

                string lx = CommonHelper.ConvertToString(lineFeature.Value[idxLx]);

                double qdgc = CommonHelper.ConvertToDouble(lineFeature.Value[idxQdgc]);
                double zdgc = CommonHelper.ConvertToDouble(lineFeature.Value[idxZdgc]);
                double gj = CommonHelper.ConvertToDouble(lineFeature.Value[idxGj]);

                if (string.IsNullOrWhiteSpace(lx) || double.IsNaN(qdgc) || double.IsNaN(zdgc) || double.IsNaN(gj))
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "流向检查",
                        ErrDesc = "字段属性错误 字段 " + (string.IsNullOrWhiteSpace(lx) ? lxFieldName + " " : "") + (double.IsNaN(qdgc) ? qdgcFieldName + " " : "") + (double.IsNaN(zdgc) ? zdgcFieldName + " " : "") + (double.IsNaN(gj) ? gjFieldName + " " : "") + "属性为空或格式错误",
                    });
                    continue;
                }

                if (lx != _dataCheck.DataCheckConfig.ZxValue || lx != _dataCheck.DataCheckConfig.NxValue)
                {
                    list.Add(new FeatureItem(lineFeature)
                    {
                        PipelineName = pipelineName,
                        PipeLayerName = lineFeatureClass.AliasName,
                        CheckItem = "流向检查",
                        ErrDesc = "流向字段值格式错误",
                    });
                    continue;
                }

                if (lx == _dataCheck.DataCheckConfig.ZxValue)
                {
                    bool isGcyc = qdgc < zdgc;
                    bool isGjyc = HasBigger(lineFeatureClass, polyline.FromPoint, lineFeature.OID, idxGj, gj) ||
                                  HasSmaller(lineFeatureClass, polyline.ToPoint, lineFeature.OID, idxGj, gj);
                    if (isGjyc || isGcyc)
                    {
                        list.Add(new FeatureItem(lineFeature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = lineFeatureClass.AliasName,
                            CheckItem = "流向检查",
                            ErrDesc = (isGcyc ? "高程异常 " : "") + (isGjyc ? "管径异常 " : ""),
                        });
                        continue;
                    }
                }
                if (lx == _dataCheck.DataCheckConfig.NxValue)
                {
                    bool isGcyc = qdgc > zdgc;
                    bool isGjyc = HasBigger(lineFeatureClass, polyline.ToPoint, lineFeature.OID, idxGj, gj) ||
                                  HasSmaller(lineFeatureClass, polyline.FromPoint, lineFeature.OID, idxGj, gj);
                    if (isGjyc || isGcyc)
                    {
                        list.Add(new FeatureItem(lineFeature)
                        {
                            PipelineName = pipelineName,
                            PipeLayerName = lineFeatureClass.AliasName,
                            CheckItem = "流向检查",
                            ErrDesc = (isGcyc ? "高程异常 " : "") + (isGjyc ? "管径异常 " : ""),
                        });
                        continue;
                    }
                }
            }
            Marshal.ReleaseComObject(lineFeatureCursor);

            return list;
        }

        private bool HasBigger(IFeatureClass featureClass, IPoint point, int oid, int idxGj, double gj)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (feature.OID == oid)
                    continue;
                double pGj = CommonHelper.ConvertToDouble(feature.Value[idxGj]);
                if (double.IsNaN(gj))
                    return true;
                if (pGj > gj)
                    return true;
            }
            Marshal.ReleaseComObject(featureCursor);
            return false;
        }

        private bool HasSmaller(IFeatureClass featureClass, IPoint point, int oid, int idxGj, double gj)
        {
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = point;
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (feature.OID == oid)
                    continue;
                double pGj = CommonHelper.ConvertToDouble(feature.Value[idxGj]);
                if (double.IsNaN(gj))
                    return true;
                if (gj > pGj)
                    return true;
            }
            Marshal.ReleaseComObject(featureCursor);
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline3D;

namespace Yutai.Pipeline3D
{
    public class PipeElevHelper
    {
        private PipeClassInfo _pointClassInfo;
        private PipeClassInfo _lineClassInfo;
        private double _searchTolerance;
        public PipeElevHelper()
        {
            _searchTolerance = 0.0001;
        }

        #region Properties

        public double SearchTolerance
        {
            get { return _searchTolerance; }
            set { _searchTolerance = value; }
        }
        public PipeClassInfo PointClassInfo
        {
            get { return _pointClassInfo; }
            set { _pointClassInfo = value; }
        }
        public PipeClassInfo LineClassInfo
        {
            get { return _lineClassInfo; }
            set { _lineClassInfo = value; }
        }
        #endregion
        #region methods
        public void AddPointClass(IFeatureClass pClass, string groundElev)
        {
            _pointClassInfo = new PipeClassInfo(pClass);
            _pointClassInfo.SetSpecialField(groundElev, PipeFieldType.StartGroundElevation);
        }

        public void AddLineClass(IFeatureClass pClass, string startGroundElev, string endGroundElev)
        {
            _lineClassInfo = new PipeClassInfo(pClass);
            _lineClassInfo.SetSpecialField(startGroundElev, PipeFieldType.StartGroundElevation);
            _lineClassInfo.SetSpecialField(endGroundElev, PipeFieldType.EndGroundElevation);
        }

        public void LinkElevation()
        {
            if (_pointClassInfo == null || _lineClassInfo == null) return;
            IDataset pDS = _lineClassInfo.PipeClass as IDataset;
            IWorkspace2 pWorkspace2 = pDS.Workspace as IWorkspace2;
            IWorkspaceEdit2 pWorkspaceEdit = pWorkspace2 as IWorkspaceEdit2;
            if (pWorkspaceEdit == null)
                return;
            pWorkspaceEdit.StartEditing(false);
            pWorkspaceEdit.StartEditOperation();
            try
            {
                IFeatureCursor pCursor = _lineClassInfo.PipeClass.Search(null, false);
                IFeature pLineFeature = null;
                ISpatialFilter pSearchFilter = new SpatialFilterClass();

                while ((pLineFeature = pCursor.NextFeature()) != null)
                {
                    IPolyline pLine = pLineFeature.Shape as IPolyline;
                    if (pLine == null)
                        continue;
                    IPoint pStartPoint = pLine.FromPoint;
                    IPoint pEndPoint = pLine.ToPoint;
                    //开始发现起点的点
                    IEnvelope envelope = pStartPoint.Envelope;
                    envelope.Expand(_searchTolerance, _searchTolerance, false);
                    pSearchFilter.Geometry = envelope;
                    pSearchFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                    IFeatureCursor pFindCursor = _pointClassInfo.PipeClass.Search(((IQueryFilter)pSearchFilter), false);
                    IFeature pPointFeature = pFindCursor.NextFeature();
                    if (pPointFeature != null)
                    {
                        double elevation = Convert.ToDouble(pPointFeature.Value[_pointClassInfo.PointElevationField.Index] is DBNull ? 0 : pPointFeature.Value[_pointClassInfo.PointElevationField.Index]);
                        double depth =
                            Convert.ToDouble(pLineFeature.Value[_lineClassInfo.StartUnderGroundElevationField.Index] is DBNull ? 0 : pLineFeature.Value[_lineClassInfo.StartUnderGroundElevationField.Index]);
                        pLineFeature.Value[_lineClassInfo.StartGroundElevationField.Index] = elevation - depth;

                    }
                    Marshal.ReleaseComObject(pFindCursor);
                    envelope = pEndPoint.Envelope;
                    envelope.Expand(_searchTolerance, _searchTolerance, false);
                    pSearchFilter.Geometry = envelope;
                    pSearchFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                    pFindCursor = _pointClassInfo.PipeClass.Search(((IQueryFilter)pSearchFilter), false);
                    pPointFeature = pFindCursor.NextFeature();
                    if (pPointFeature != null)
                    {
                        double elevation = Convert.ToDouble(pPointFeature.Value[_pointClassInfo.PointElevationField.Index] is DBNull ? 0 : pPointFeature.Value[_pointClassInfo.PointElevationField.Index]);
                        double depth =
                            Convert.ToDouble(pLineFeature.Value[_lineClassInfo.EndUnderGroundElevationField.Index] is DBNull ? 0 : pLineFeature.Value[_lineClassInfo.EndUnderGroundElevationField.Index]);
                        pLineFeature.Value[_lineClassInfo.EndGroundElevationField.Index] = elevation - depth;
                    }
                    pLineFeature.Store();
                    Marshal.ReleaseComObject(pFindCursor);
                }
                Marshal.ReleaseComObject(pCursor);
            }
            finally
            {
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
            }
        }
        #endregion
    }
}

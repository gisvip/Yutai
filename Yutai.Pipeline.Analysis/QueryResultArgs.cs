using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Analysis
{
    public class QueryResultArgs : EventArgs
    {
        private IFeatureCursor _cursor;
        private IFeatureSelection _selection;
        private ISpatialFilter _spatialFilter;

        public QueryResultArgs()
        {
            _cursor = null;
            _selection = null;
        }

        //public QueryResultArgs(IFeatureCursor cursor, IFeatureSelection pSelection)
        //{
        //    _cursor = cursor;
        //    _selection = pSelection;
        //}

        public QueryResultArgs(IFeatureCursor cursor, IFeatureSelection pSelection, ISpatialFilter spatialFilter)
        {
            _cursor = cursor;
            _selection = pSelection;
            _spatialFilter = spatialFilter;
        }

        public IFeatureCursor Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        public IFeatureSelection Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        public ISpatialFilter SpatialFilter
        {
            get { return _spatialFilter; }
            set { _spatialFilter = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    class Point3DCylinder : Geometry3DBase, I3DPoint
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly double _depth;
        private readonly double _diameter;

        public Point3DCylinder(double x, double y, double z, double depth, double diameter, int division) : base(division)
        {
            _x = x;
            _y = y;
            _z = z;
            _depth = depth;
            _diameter = diameter;
        }

        public override IGeometry CreateGeometry()
        {
            IPointCollection pointCollection = new PolygonClass();
            IZAware zAware = pointCollection as IZAware;
            zAware.ZAware = true;

            double angle = 2 * Math.PI / _division;
            for (int i = 0; i < _division; i++)
            {
                IPoint point = new PointClass();
                point.X = _diameter * Math.Cos(angle * i) / 2;
                point.Y = _diameter * Math.Sin(angle * i) / 2;
                point.Z = 0;
                pointCollection.AddPoint(point);
            }
            ((IPolygon)pointCollection).Close();

            IConstructMultiPatch patch = new MultiPatchClass();
            patch.ConstructExtrude(0 - _depth, pointCollection as IGeometry);
            ITransform3D transform3D = patch as ITransform3D;
            transform3D.Move3D(_x, _y, _z);
            return patch as IGeometry;
        }
    }
}

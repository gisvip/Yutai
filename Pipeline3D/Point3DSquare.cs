using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    class Point3DSquare : Geometry3DBase, I3DPoint
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly double _depth;
        private double _length;
        private double _width;
        private double _radian;
        public Point3DSquare(double x, double y, double z, double depth, double length, double width, double radian, int division) : base(division)
        {
            _x = x;
            _y = y;
            _z = z;
            _depth = depth;
            _length = length;
            _width = width;
            _radian = radian;
        }

        public override IGeometry CreateGeometry()
        {
            IPointCollection pointCollection = new PolygonClass();
            IZAware zAware = pointCollection as IZAware;
            zAware.ZAware = true;

            IPoint point = new PointClass();
            point.X = -_width/ 2;
            point.Y = -_length / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = -_width / 2;
            point.Y = _length / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = _width / 2;
            point.Y = _length / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = _width / 2;
            point.Y = -_length / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            ((IPolygon)pointCollection).Close();

            IConstructMultiPatch patch = new MultiPatchClass();
            patch.ConstructExtrude(0 - _depth, pointCollection as IGeometry);
            ITransform3D transform3D = patch as ITransform3D;
            IVector3D vector3D = ConstructVector3D(0, 0, 1);
            transform3D.RotateVector3D(vector3D, _radian);
            transform3D.Move3D(_x, _y, _z);
            return patch as IGeometry;
        }

        public IVector3D ConstructVector3D(double xComponent, double yComponent, double zComponent)
        {
            IVector3D vector3D = new Vector3DClass();
            vector3D.SetComponents(xComponent, yComponent, zComponent);

            return vector3D;
        }

    }
}

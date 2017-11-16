using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    class Line3DSquare : Geometry3DBase, I3DLine
    {
        private readonly IPolyline _polyline;
        private readonly double _qdgc;
        private readonly double _zdgc;
        private readonly double _width;
        private readonly double _height;

        public Line3DSquare(IPolyline polyline, double qdgc, double zdgc, double width, double height, int division) : base(division)
        {
            _polyline = polyline;
            _qdgc = qdgc;
            _zdgc = zdgc;
            _width = width;
            _height = height;
        }

        public override IGeometry CreateGeometry()
        {
            IPointCollection pointCollection = new PolygonClass();
            IZAware zAware = pointCollection as IZAware;
            zAware.ZAware = true;

            IPoint point = new PointClass();
            point.X = -_width / 2;
            point.Y = -_height / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = -_width / 2;
            point.Y = _height / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = _width / 2;
            point.Y = _height / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);

            point = new PointClass();
            point.X = _width / 2;
            point.Y = -_height / 2;
            point.Z = 0;
            pointCollection.AddPoint(point);
            ((IPolygon)pointCollection).Close();

            IConstructMultiPatch patch = new MultiPatchClass();
            patch.ConstructExtrude(_polyline.Length, pointCollection as IGeometry);
            
            IVector3D vectorZ = new Vector3DClass();
            vectorZ.SetComponents(0, 0, 1);
            IVector3D vector3D = new Vector3DClass();
            vector3D.SetComponents(_polyline.ToPoint.X - _polyline.FromPoint.X, _polyline.ToPoint.Y - _polyline.FromPoint.Y, _zdgc - _qdgc);
            double rotateAngle = Math.Acos(vector3D.ZComponent / vector3D.Magnitude);
            IVector3D vectorAxis = vectorZ.CrossProduct(vector3D) as IVector3D;
            
            ITransform3D transform3D = patch as ITransform3D;
            transform3D.RotateVector3D(vectorAxis, rotateAngle);
            transform3D.Move3D(_polyline.FromPoint.X, _polyline.FromPoint.Y, _qdgc);
            return patch as IGeometry;
        }

        public IGeometry CreateStartSphere()
        {
            return null;
        }

        public IGeometry CreateEndSphere()
        {
            return null;
        }
    }
}

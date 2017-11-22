using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    class Line3DCylinder : Geometry3DBase, I3DLine
    {
        private readonly IPolyline _polyline;
        private readonly double _qdgc;
        private readonly double _zdgc;
        private readonly double _diameter;
        private readonly double _expand = 1.4;

        public Line3DCylinder(IPolyline polyline, double qdgc, double zdgc, double diameter, int division) : base(division)
        {
            _polyline = polyline;
            _qdgc = qdgc;
            _zdgc = zdgc;
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
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_polyline.FromPoint.X, _polyline.FromPoint.Y);
            centerPoint.Z = _qdgc;
            return CreateSphere(centerPoint, _diameter * _expand / 2.0);
        }

        public IGeometry CreateEndSphere()
        {
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(_polyline.ToPoint.X, _polyline.ToPoint.Y);
            centerPoint.Z = _zdgc;
            return CreateSphere(centerPoint, _diameter * _expand / 2.0);
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
                    double s = (lon - minLon) / lonRange;
                    if (bFlipS)
                    {
                        s = 1 + (s * -1);
                    }
                    if (s <= 0) s = 0.001;
                    else if (s >= 1)
                        s = 0.999;

                    double t = (maxLat - lat) / latRange;
                    if (bFlipT)
                        t = 1 + (t * -1);
                    if (t <= 0) t = 0.001;
                    else if (t >= 1)
                        t = 0.999;

                    double m = 0.0;
                    pGE.PackTexture2D(s, t, out m);
                    if (bSmooth)
                    {
                        pVector.Normalize();
                        pGE.PackNormal(pVector, out m);
                    }
                    pt.M = m;

                    pStrip.AddPoint(pt, ref missing, ref missing);
                    if ((lat != -90) && (lat != 90))
                    {
                        azi = (lon + xStep) * Math.PI / 180.00;
                        inc = lat * Math.PI / 180.00;
                        pVector.PolarSet(-azi, inc, radius);
                        pt = new PointClass();
                        pt.X = centerPoint.X + pVector.XComponent;
                        pt.Y = centerPoint.Y + pVector.YComponent;
                        pt.Z = centerPoint.Z + pVector.ZComponent;
                        s = (lon + xStep - minLon) / lonRange;
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

        public double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.00;
        }
    }
}

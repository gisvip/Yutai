using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    class Point3DSphere : Geometry3DBase, I3DPoint
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly double _depth;
        private readonly double _diameter;

        public Point3DSphere(double x, double y, double z, double depth, double diameter, int division) : base(division)
        {
            _x = x;
            _y = y;
            _z = z;
            _depth = depth;
            _diameter = diameter;
        }

        public override IGeometry CreateGeometry()
        {
            return CreateSphere(_x, _y, _z - _depth - _diameter/2, _diameter/2);
        }

        private IGeometry CreateSphere(double x, double y, double z, double radius, double minLon = 0.0, double maxLon = 360.0, double minLat = -90.0, double maxLat = 90.0, double stepAngle = 18.0, bool bSmooth = false, bool bFlipS = false, bool bFlipT = false)
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
                    pt.X = x + pVector.XComponent;
                    pt.Y = y + pVector.YComponent;
                    pt.Z = z + pVector.ZComponent;
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
                        pt.X = x + pVector.XComponent;
                        pt.Y = y + pVector.YComponent;
                        pt.Z = z + pVector.ZComponent;
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

        private double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.00;
        }
    }
}

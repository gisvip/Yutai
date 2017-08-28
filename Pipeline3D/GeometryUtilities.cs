using System;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    public static class GeometryUtilities
    {
        private static object _missing = Type.Missing;

        public static void MakeZAware(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }

        public static IVector3D ConstructVector3D(double xComponent, double yComponent, double zComponent)
        {
            IVector3D vector3D = new Vector3DClass();
            vector3D.SetComponents(xComponent, yComponent, zComponent);

            return vector3D;
        }

        public static double GetRadians(double decimalDegrees)
        {
            return decimalDegrees * (Math.PI / 180); 
        }

        public static IPoint ConstructPoint3D(double x, double y, double z)
        {
            IPoint point = ConstructPoint2D(x, y);
            point.Z = z;

            MakeZAware(point as IGeometry);

            return point;
        }

        public static IPoint ConstructPoint2D(double x, double y)
        {
            IPoint point = new PointClass();
            point.X = x;
            point.Y = y;

            return point;
        }

        public static IGeometryCollection ConstructMultiPatchOutline(IGeometry multiPatchGeometry)
        {
            IGeometryCollection outlineGeometryCollection = new GeometryBagClass();

            IGeometryCollection multiPatchGeometryCollection = multiPatchGeometry as IGeometryCollection; 

            for (int i = 0; i < multiPatchGeometryCollection.GeometryCount; i++)
            {
                IGeometry geometry = multiPatchGeometryCollection.get_Geometry(i);

                switch(geometry.GeometryType)
                {
                    case (esriGeometryType.esriGeometryTriangleStrip):
                        outlineGeometryCollection.AddGeometryCollection(ConstructTriangleStripOutline(geometry));
                        break;

                    case (esriGeometryType.esriGeometryTriangleFan):
                        outlineGeometryCollection.AddGeometryCollection(ConstructTriangleFanOutline(geometry));
                        break;

                    case (esriGeometryType.esriGeometryTriangles):
                        outlineGeometryCollection.AddGeometryCollection(ConstructTrianglesOutline(geometry));
                        break;

                    case (esriGeometryType.esriGeometryRing):
                        outlineGeometryCollection.AddGeometry(ConstructRingOutline(geometry), ref _missing, ref _missing);
                        break;

                    default:
                        throw new Exception("Unhandled Geometry Type. " + geometry.GeometryType);
                }
            }

            return outlineGeometryCollection;
        }

        public static IGeometryCollection ConstructTriangleStripOutline(IGeometry triangleStripGeometry)
        {
            IGeometryCollection outlineGeometryCollection = new GeometryBagClass();

            IPointCollection triangleStripPointCollection = triangleStripGeometry as IPointCollection;

            // TriangleStrip: a linked strip of triangles, where every vertex (after the first two) completes a new triangle.
            //                A new triangle is always formed by connecting the new vertex with its two immediate predecessors.

            for (int i = 2; i < triangleStripPointCollection.PointCount; i++)
            {
                IPointCollection outlinePointCollection = new PolylineClass();

                outlinePointCollection.AddPoint(triangleStripPointCollection.get_Point(i - 2), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleStripPointCollection.get_Point(i - 1), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleStripPointCollection.get_Point(i), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleStripPointCollection.get_Point(i - 2), ref _missing, ref _missing); //Simulate: Polygon.Close

                IGeometry outlineGeometry = outlinePointCollection as IGeometry;

                MakeZAware(outlineGeometry);

                outlineGeometryCollection.AddGeometry(outlineGeometry, ref _missing, ref _missing);
            }

            return outlineGeometryCollection;
        }

        public static IGeometryCollection ConstructTriangleFanOutline(IGeometry triangleFanGeometry)
        {
            IGeometryCollection outlineGeometryCollection = new GeometryBagClass();
            
            IPointCollection triangleFanPointCollection = triangleFanGeometry as IPointCollection;

            // TriangleFan: a linked fan of triangles, where every vertex (after the first two) completes a new triangle. 
            //              A new triangle is always formed by connecting the new vertex with its immediate predecessor 
            //              and the first vertex of the part.

            for (int i = 2; i < triangleFanPointCollection.PointCount; i++)
            {
                IPointCollection outlinePointCollection = new PolylineClass();

                outlinePointCollection.AddPoint(triangleFanPointCollection.get_Point(0), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleFanPointCollection.get_Point(i - 1), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleFanPointCollection.get_Point(i), ref _missing, ref _missing);
                outlinePointCollection.AddPoint(triangleFanPointCollection.get_Point(0), ref _missing, ref _missing); //Simulate: Polygon.Close

                IGeometry outlineGeometry = outlinePointCollection as IGeometry;

                MakeZAware(outlineGeometry);

                outlineGeometryCollection.AddGeometry(outlineGeometry, ref _missing, ref _missing);
            }

            return outlineGeometryCollection;
        }

        public static IGeometryCollection ConstructTrianglesOutline(IGeometry trianglesGeometry)
        {
            IGeometryCollection outlineGeometryCollection = new GeometryBagClass();

            IPointCollection trianglesPointCollection = trianglesGeometry as IPointCollection;

            // Triangles: an unlinked set of triangles, where every three vertices completes a new triangle.

            if ((trianglesPointCollection.PointCount % 3) != 0)
            {
                throw new Exception("Triangles Geometry Point Count Must Be Divisible By 3. " + trianglesPointCollection.PointCount);
            }
            else
            {
                for (int i = 0; i < trianglesPointCollection.PointCount; i+=3)
                {
                    IPointCollection outlinePointCollection = new PolylineClass();

                    outlinePointCollection.AddPoint(trianglesPointCollection.get_Point(i), ref _missing, ref _missing);
                    outlinePointCollection.AddPoint(trianglesPointCollection.get_Point(i + 1), ref _missing, ref _missing);
                    outlinePointCollection.AddPoint(trianglesPointCollection.get_Point(i + 2), ref _missing, ref _missing);
                    outlinePointCollection.AddPoint(trianglesPointCollection.get_Point(i), ref _missing, ref _missing); //Simulate: Polygon.Close

                    IGeometry outlineGeometry = outlinePointCollection as IGeometry;

                    MakeZAware(outlineGeometry);

                    outlineGeometryCollection.AddGeometry(outlineGeometry, ref _missing, ref _missing);
                }
            }

            return outlineGeometryCollection;
        }

        public static IGeometry ConstructRingOutline(IGeometry ringGeometry)
        {
            IGeometry outlineGeometry = new PolylineClass();

            IPointCollection outlinePointCollection = outlineGeometry as IPointCollection;

            IPointCollection ringPointCollection = ringGeometry as IPointCollection;

            for (int i = 0; i < ringPointCollection.PointCount; i++)
            {
                outlinePointCollection.AddPoint(ringPointCollection.get_Point(i), ref _missing, ref _missing);
            }

            outlinePointCollection.AddPoint(ringPointCollection.get_Point(0), ref _missing, ref _missing); //Simulate: Polygon.Close

            MakeZAware(outlineGeometry);

            return outlineGeometry;
        }
    }
}
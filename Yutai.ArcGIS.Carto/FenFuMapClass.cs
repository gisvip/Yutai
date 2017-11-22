using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.DesignLib;

namespace Yutai.ArcGIS.Carto
{
    internal class FenFuMapClass : BaseMap
    {
        private double double_2 = 0.0;
        private double double_3 = 0.0;
        private IList<IElement> ilist_0 = new List<IElement>();
        private string _mapRow3Col1Text = "";
        private string _mapRow1Col2Text = "";
        private string _mapRow3Col2Text = "";
        private string _mapRow1Col3Text = "";
        private string _mapRow2Col3Text = "";
        private string _mapRow3Col3Text = "";
        private string _mapScaleText = "";
        private string _mapRightUpText = "";
        private string _mapRightLowText = "";
        private string _mapLeftLowText = "";
        private string _mapLeftBorderOutText = "";
        private string _mapRow1Col1Text = "";
        private string _mapRow2Col1Text = "";

        public FenFuMapClass()
        {
            MapType = "分幅图";
            InOutDist = 80.0;
            TitleDist = 150.0;
        }

        public override void Draw()
        {
            var num = 0;
            IElement element = null;
            IEnvelope envelope = null;
            m_pActiveView.FocusMap.ReferenceScale = m_ReferenceScale;
            DrawInsideFrame();
            DrawOutFrame();
            DrawTitle();
            DrawRemark();
            DrawGrid();
            DrawJionTab();
            method_4();
            if (NeedLegend)
                DrawLegend();
            if (m_pActiveView != null)
            {
                GraphicsLayer = new CompositeGraphicsLayerClass();
                var graphicsLayer = m_GraphicsLayer as ILayer;
                graphicsLayer.SpatialReference = m_pActiveView.FocusMap.SpatialReference;
                graphicsLayer.Name = "制图层";
                (m_GraphicsLayer as IGeoDatasetSchemaEdit).AlterSpatialReference(
                    m_pActiveView.FocusMap.SpatialReference);
                var container = m_GraphicsLayer as IGraphicsContainer;
                element = method_8();
                if (element != null)
                    container.AddElement(element, 0);
                for (num = 0; num < ilist_0.Count; num++)
                    container.AddElement(ilist_0[num], 0);
                m_pActiveView.FocusMap.AddLayer(graphicsLayer);
                envelope = ExpandByDist(InOutDist, InOutDist, InOutDist + double_3,
                    InOutDist + double_2);
                if (envelope != null)
                    m_pActiveView.Extent = envelope;
                m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_GraphicsLayer,
                    m_pActiveView.Extent);
            }
            method_11();
        }

        public override void DrawBackFrame()
        {
        }

        public void DrawGrid()
        {
            var str = "";
            var str2 = "";
            ITextElement element = null;
            ITextElement element2 = null;
            ITextElement element3 = null;
            ITextElement element4 = null;
            var num = 40.0;
            var num2 = 20.0;
            var num3 = 10.0;
            var num4 = 13;
            var num5 = 10;
            IPoint point = null;
            IElement item = null;
            ITextSymbol symbol = new TextSymbolClass();
            ITextSymbol symbol2 = new TextSymbolClass();
            ITextSymbol symbol3 = new TextSymbolClass();
            ITextSymbol symbol4 = new TextSymbolClass();
            ITextSymbol symbol5 = new TextSymbolClass();
            ITextSymbol symbol6 = new TextSymbolClass();
            var num6 = 0;
            var num7 = 0;
            var num8 = 1000;
            var x = 0.0;
            var y = 0.0;
            IPoint point2 = new PointClass();
            var missing = Type.Missing;
            IElementProperties2 properties = null;
            IMarkerElement element6 = null;
            ISymbol symbol7 = new SimpleMarkerSymbolClass();
            var symbol8 = symbol7 as ISimpleMarkerSymbol;
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol8.Size = 10.0;
            symbol8.Style = esriSimpleMarkerStyle.esriSMSCross;
            symbol8.Color = color;
            var num11 = 0.0;
            var num12 = 0.0;
            var num13 = 0.0;
            var num14 = 0.0;
            var num15 = 0;
            var num16 = 0;
            var num17 = 0;
            var num18 = 0;
            var num19 = 0;
            var num20 = 0.0;
            var num21 = 0.0;
            var num22 = 0.0;
            var num23 = 0.0;
            try
            {
                symbol = FontStyle((double) num4, esriTextHorizontalAlignment.esriTHARight,
                    esriTextVerticalAlignment.esriTVABottom);
                symbol2 = FontStyle((double) num5, esriTextHorizontalAlignment.esriTHALeft,
                    esriTextVerticalAlignment.esriTVABottom);
                symbol3 = FontStyle((double) num5, esriTextHorizontalAlignment.esriTHARight,
                    esriTextVerticalAlignment.esriTVATop);
                symbol4 = FontStyle((double) num4, esriTextHorizontalAlignment.esriTHALeft,
                    esriTextVerticalAlignment.esriTVATop);
                symbol5 = FontStyle((double) num5, esriTextHorizontalAlignment.esriTHARight,
                    esriTextVerticalAlignment.esriTVABottom);
                symbol6 = FontStyle((double) num4, esriTextHorizontalAlignment.esriTHALeft,
                    esriTextVerticalAlignment.esriTVABottom);
                if (LeftUp.Y < RightUp.Y)
                {
                    y = Math.Truncate(LeftUp.Y);
                    num23 = RightUp.Y + InOutDist + 1.0;
                }
                else
                {
                    y = Math.Truncate(RightUp.Y);
                    num23 = LeftUp.Y + InOutDist + 1.0;
                }
                num15 = (int) (y%(double) num8);
                if (num15 != 0)
                    num12 = y - num15;
                else
                    num12 = y;
                if (LeftUp.X > LeftLow.X)
                {
                    x = Math.Truncate((double) (LeftUp.X + 1.0));
                    num20 = LeftLow.X - InOutDist - 1.0;
                }
                else
                {
                    x = Math.Truncate((double) (LeftLow.X + 1.0));
                    num20 = LeftUp.X - InOutDist - 1.0;
                }
                num15 = (int) (x%(double) num8);
                if (num15 != 0)
                    num11 = x + (num8 - num15);
                else
                    num11 = x;
                if (LeftLow.Y < RightLow.Y)
                {
                    y = Math.Truncate(LeftLow.Y);
                    num21 = LeftLow.Y - InOutDist - 1.0;
                }
                else
                {
                    y = Math.Truncate(RightLow.Y);
                    num21 = RightLow.Y - InOutDist - 1.0;
                }
                num15 = (int) (y%(double) num8);
                if (num15 != 0)
                    num14 = y + (num8 - num15);
                else
                    num14 = y;
                if (RightUp.X > RightLow.X)
                {
                    x = Math.Truncate(RightLow.X);
                    num22 = RightUp.X + InOutDist + 1.0;
                }
                else
                {
                    x = Math.Truncate(RightUp.X);
                    num22 = RightLow.X + InOutDist + 1.0;
                }
                num15 = (int) (x%(double) num8);
                if (num15 != 0)
                    num13 = x - num15;
                else
                    num13 = x;
                num16 = (int) (num12 - num14)/num8;
                num17 = (int) (num13 - num11)/num8;
                for (num18 = 0; num18 <= num16; num18++)
                {
                    y = num12 - num18*num8;
                    for (num19 = 0; num19 <= num17; num19++)
                    {
                        x = num11 + num19*num8;
                        point2.PutCoords(x, y);
                        item = new MarkerElementClass
                        {
                            Geometry = point2
                        };
                        element6 = item as IMarkerElement;
                        element6.Symbol = symbol8;
                        properties = item as IElementProperties2;
                        properties.Type = "公里网";
                        ilist_0.Add(item);
                    }
                }
                IPolygon polygon = new PolygonClass();
                new PolygonElementClass();
                polygon = method_5();
                var num1 = LeftUp.X - InOutDist;
                var num24 = LeftUp.X;
                var num25 = RightUp.X;
                var num26 = RightUp.X + InOutDist;
                IPoint inPoint = null;
                IPoint point4 = null;
                IPoint point5 = null;
                IPoint point6 = null;
                IPolyline polyline = new PolylineClass();
                IPolyline polyline2 = new PolylineClass();
                IPolyline polyline3 = new PolylineClass();
                IPoint point7 = null;
                IPoint point8 = null;
                IPointCollection points = null;
                IPointCollection points2 = null;
                IElement element7 = null;
                IElement element8 = null;
                ILineElement element9 = null;
                var symbol9 = method_2();
                for (num18 = 0; num18 <= num16; num18++)
                {
                    y = num12 - num18*num8;
                    point7 = new PointClass();
                    point8 = new PointClass();
                    point7.PutCoords(num20, y);
                    point8.PutCoords(num22, y);
                    points = polyline3 as IPointCollection;
                    if (points.PointCount >= 1)
                        points.RemovePoints(0, points.PointCount);
                    points.AddPoint(point7, ref missing, ref missing);
                    points.AddPoint(point8, ref missing, ref missing);
                    points = method_6(polyline3, polygon);
                    inPoint = points.get_Point(0);
                    point4 = points.get_Point(1);
                    point5 = points.get_Point(2);
                    point6 = points.get_Point(3);
                    element7 = new LineElementClass();
                    element9 = element7 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline as IPointCollection;
                    if (points2.PointCount >= 1)
                        points2.RemovePoints(0, points2.PointCount);
                    points2.AddPoint(inPoint, ref missing, ref missing);
                    points2.AddPoint(point4, ref missing, ref missing);
                    element7.Geometry = polyline;
                    element8 = new LineElementClass();
                    element9 = element8 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline2 as IPointCollection;
                    if (points2.PointCount >= 1)
                        points2.RemovePoints(0, points2.PointCount);
                    points2.AddPoint(point5, ref missing, ref missing);
                    points2.AddPoint(point6, ref missing, ref missing);
                    element8.Geometry = polyline2;
                    ilist_0.Add(element7);
                    ilist_0.Add(element8);
                    element = new TextElementClass();
                    element2 = new TextElementClass();
                    element3 = new TextElementClass();
                    element4 = new TextElementClass();
                    num6 = (int) Math.Truncate((double) (y/100000.0));
                    str = num6.ToString();
                    num7 = (int) Math.Truncate((double) ((y - num6*100000)/1000.0));
                    str2 = num7.ToString();
                    if (str2.Length < 2)
                        str2 = "0" + str2;
                    element.Text = str2;
                    element2.Text = str;
                    element3.Text = str2;
                    element4.Text = str;
                    point = new PointClass();
                    point.PutCoords(inPoint.X, inPoint.Y + num2);
                    item = element as IElement;
                    item.Geometry = point;
                    element.Symbol = symbol;
                    ilist_0.Add(item);
                    if (num18 == 0 || num18 == num16)
                    {
                        point = new PointClass();
                        point.PutCoords(point4.X, point4.Y + num);
                        item = element2 as IElement;
                        item.Geometry = point;
                        element2.Symbol = symbol2;
                        ilist_0.Add(item);
                    }
                    point = new PointClass();
                    point.PutCoords(point5.X, point5.Y + num2);
                    item = element3 as IElement;
                    item.Geometry = point;
                    element3.Symbol = symbol;
                    ilist_0.Add(item);
                    if (num18 == 0 || num18 == num16)
                    {
                        point = new PointClass();
                        point.PutCoords(point6.X, point6.Y + num);
                        item = element4 as IElement;
                        item.Geometry = point;
                        element4.Symbol = symbol2;
                        ilist_0.Add(item);
                    }
                }
                for (num18 = 0; num18 <= num17; num18++)
                {
                    x = num11 + num18*num8;
                    point7 = new PointClass();
                    point8 = new PointClass();
                    point7.PutCoords(x, num23);
                    point8.PutCoords(x, num21);
                    points = polyline3 as IPointCollection;
                    if (points.PointCount >= 1)
                        points.RemovePoints(0, points.PointCount);
                    points.AddPoint(point7, ref missing, ref missing);
                    points.AddPoint(point8, ref missing, ref missing);
                    points = method_6(polyline3, polygon);
                    inPoint = points.get_Point(0);
                    point4 = points.get_Point(1);
                    point5 = points.get_Point(2);
                    point6 = points.get_Point(3);
                    element7 = new LineElementClass();
                    element9 = element7 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline as IPointCollection;
                    if (points2.PointCount >= 1)
                        points2.RemovePoints(0, points2.PointCount);
                    points2.AddPoint(inPoint, ref missing, ref missing);
                    points2.AddPoint(point4, ref missing, ref missing);
                    element7.Geometry = polyline;
                    element8 = new LineElementClass();
                    element9 = element8 as ILineElement;
                    element9.Symbol = symbol9;
                    points2 = polyline2 as IPointCollection;
                    if (points2.PointCount >= 1)
                        points2.RemovePoints(0, points2.PointCount);
                    points2.AddPoint(point5, ref missing, ref missing);
                    points2.AddPoint(point6, ref missing, ref missing);
                    element8.Geometry = polyline2;
                    ilist_0.Add(element7);
                    ilist_0.Add(element8);
                    element = new TextElementClass();
                    element2 = new TextElementClass();
                    element3 = new TextElementClass();
                    element4 = new TextElementClass();
                    num6 = (int) Math.Truncate((double) (x/100000.0));
                    str = num6.ToString();
                    str2 = ((int) Math.Truncate((double) ((x - num6*100000)/1000.0))).ToString();
                    if (str2.Length < 2)
                        str2 = "0" + str2;
                    element.Text = str2;
                    element2.Text = str;
                    element3.Text = str2;
                    element4.Text = str;
                    point = new PointClass();
                    point.PutCoords(inPoint.X, inPoint.Y - num3);
                    item = element as IElement;
                    item.Geometry = point;
                    element.Symbol = symbol4;
                    ilist_0.Add(item);
                    if (num18 == 0 || num18 == num17)
                    {
                        point = new PointClass();
                        point.PutCoords(inPoint.X, inPoint.Y - num3);
                        item = element2 as IElement;
                        item.Geometry = point;
                        element2.Symbol = symbol3;
                        ilist_0.Add(item);
                    }
                    point = new PointClass();
                    point.PutCoords(point6.X, point6.Y + num3);
                    item = element3 as IElement;
                    item.Geometry = point;
                    element3.Symbol = symbol6;
                    ilist_0.Add(item);
                    if (num18 == 0 || num18 == num17)
                    {
                        point = new PointClass();
                        point.PutCoords(point6.X, point6.Y + num3);
                        item = element4 as IElement;
                        item.Geometry = point;
                        element4.Symbol = symbol5;
                        ilist_0.Add(item);
                    }
                }
                inPoint = new PointClass();
                point4 = new PointClass();
                point5 = new PointClass();
                IPolyline polyline4 = new PolylineClass();
                IElement element10 = null;
                inPoint.PutCoords(LeftUp.X, LeftUp.Y + InOutDist);
                point4.PutCoords(LeftUp.X, LeftUp.Y);
                point5.PutCoords(LeftUp.X - InOutDist, LeftUp.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                    points.RemovePoints(0, points.PointCount);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                ilist_0.Add(element10);
                inPoint.PutCoords(RightUp.X, RightUp.Y + InOutDist);
                point4.PutCoords(RightUp.X, RightUp.Y);
                point5.PutCoords(RightUp.X + InOutDist, RightUp.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                    points.RemovePoints(0, points.PointCount);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                ilist_0.Add(element10);
                inPoint.PutCoords(RightLow.X, RightLow.Y - InOutDist);
                point4.PutCoords(RightLow.X, RightLow.Y);
                point5.PutCoords(RightLow.X + InOutDist, RightLow.Y);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                    points.RemovePoints(0, points.PointCount);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                ilist_0.Add(element10);
                inPoint.PutCoords(LeftLow.X - InOutDist, LeftLow.Y);
                point4.PutCoords(LeftLow.X, LeftLow.Y);
                point5.PutCoords(LeftLow.X, LeftLow.Y - InOutDist);
                element10 = new LineElementClass();
                element9 = element10 as ILineElement;
                element9.Symbol = symbol9;
                points = polyline4 as IPointCollection;
                if (points.PointCount >= 1)
                    points.RemovePoints(0, points.PointCount);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                element10.Geometry = polyline4;
                ilist_0.Add(element10);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawInsideFrame()
        {
            IElement item = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            var points = polyline as IPointCollection;
            var missing = Type.Missing;
            try
            {
                if (!(LeftUp == null || LeftUp.IsEmpty))
                    points.AddPoint(LeftUp, ref missing, ref missing);
                if (!(LeftLow == null || LeftLow.IsEmpty))
                    points.AddPoint(LeftLow, ref missing, ref missing);
                if (!(RightLow == null || RightLow.IsEmpty))
                    points.AddPoint(RightLow, ref missing, ref missing);
                if (!(RightUp == null || RightUp.IsEmpty))
                    points.AddPoint(RightUp, ref missing, ref missing);
                if (!(LeftUp == null || LeftUp.IsEmpty))
                    points.AddPoint(LeftUp, ref missing, ref missing);
                item.Geometry = polyline;
                properties = item as IElementProperties2;
                properties.Type = "内框";
                symbol = method_2();
                element2 = item as ILineElement;
                element2.Symbol = symbol;
                ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void DrawJionTab()
        {
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = new TextElementClass();
            ITextElement element3 = new TextElementClass();
            ITextElement element4 = new TextElementClass();
            ITextElement element5 = new TextElementClass();
            ITextElement element6 = new TextElementClass();
            ITextElement element7 = new TextElementClass();
            ITextElement element8 = new TextElementClass();
            ITextElement element9 = new TextElementClass();
            ILineElement element10 = null;
            ILineSymbol symbol = null;
            var num = 390.0;
            var num2 = 210.0;
            var num3 = num2/3.0;
            var num4 = num/3.0;
            var num5 = 20.0 + InOutDist;
            double_3 = num2;
            var missing = Type.Missing;
            IElement element11 = new LineElementClass();
            IElement element12 = new LineElementClass();
            IElement element13 = new LineElementClass();
            IElement element14 = new LineElementClass();
            IElement element15 = new LineElementClass();
            IPolyline polyline = new PolylineClass();
            IPolyline polyline2 = new PolylineClass();
            IPolyline polyline3 = new PolylineClass();
            IPolyline polyline4 = new PolylineClass();
            IPolyline polyline5 = new PolylineClass();
            var points = polyline as IPointCollection;
            IPoint inPoint = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IPoint point5 = new PointClass();
            try
            {
                symbol = method_2();
                element10 = element11 as ILineElement;
                element10.Symbol = symbol;
                element10 = element12 as ILineElement;
                element10.Symbol = symbol;
                element10 = element13 as ILineElement;
                element10.Symbol = symbol;
                element10 = element14 as ILineElement;
                element10.Symbol = symbol;
                element10 = element15 as ILineElement;
                element10.Symbol = symbol;
                point3.PutCoords(LeftUp.X, LeftUp.Y + num5);
                inPoint.PutCoords(LeftUp.X, LeftUp.Y + num5 + num2);
                point4.PutCoords(LeftUp.X + num, LeftUp.Y + num5);
                point5.PutCoords(LeftUp.X + num, LeftUp.Y + num5 + num2);
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(point5, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
                element11.Geometry = polyline;
                ilist_0.Add(element11);
                IPoint point6 = new PointClass();
                IPoint point7 = new PointClass();
                point6.PutCoords(inPoint.X, inPoint.Y - num3);
                point7.PutCoords(point5.X, point5.Y - num3);
                points = polyline2 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element12.Geometry = polyline2;
                ilist_0.Add(element12);
                point6.PutCoords(inPoint.X, inPoint.Y - num3*2.0);
                point7.PutCoords(point5.X, point5.Y - num3*2.0);
                points = polyline3 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element13.Geometry = polyline3;
                ilist_0.Add(element13);
                point6.PutCoords(inPoint.X + num4, inPoint.Y);
                point7.PutCoords(inPoint.X + num4, point3.Y);
                points = polyline4 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element14.Geometry = polyline4;
                ilist_0.Add(element14);
                point6.PutCoords(inPoint.X + num4*2.0, inPoint.Y);
                point7.PutCoords(inPoint.X + num4*2.0, point3.Y);
                points = polyline5 as IPointCollection;
                points.AddPoint(point6, ref missing, ref missing);
                points.AddPoint(point7, ref missing, ref missing);
                element15.Geometry = polyline5;
                ilist_0.Add(element15);
                IPolygon polygon = new PolygonClass();
                IElement element16 = new PolygonElementClass();
                var element17 = element16 as IPolygonElement;
                ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass();
                var element18 = element17 as IFillShapeElement;
                IRgbColor color = new RgbColorClass
                {
                    Red = 0,
                    Green = 0,
                    Blue = 0
                };
                symbol2.Outline = method_2();
                symbol2.Color = color;
                symbol2.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                element18.Symbol = symbol2;
                points = polygon as IPointCollection;
                point6.PutCoords(inPoint.X + num4, inPoint.Y - num3);
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + num4*2.0, inPoint.Y - num3);
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + num4*2.0, inPoint.Y - num3*2.0);
                points.AddPoint(point6, ref missing, ref missing);
                point6.PutCoords(inPoint.X + num4, inPoint.Y - num3*2.0);
                points.AddPoint(point6, ref missing, ref missing);
                polygon.Close();
                element16.Geometry = polygon;
                ilist_0.Add(element16);
                element2.Text = _mapRow1Col1Text;
                element3.Text = _mapRow2Col1Text;
                element4.Text = _mapRow3Col1Text;
                element5.Text = _mapRow1Col2Text;
                element6.Text = _mapRow3Col2Text;
                element7.Text = _mapRow1Col3Text;
                element8.Text = _mapRow2Col3Text;
                element9.Text = _mapRow3Col3Text;
                item = element2 as IElement;
                point.PutCoords(inPoint.X + num4/2.0, inPoint.Y - num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element3 as IElement;
                point.PutCoords(inPoint.X + num4/2.0, inPoint.Y - 3.0*num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element4 as IElement;
                point.PutCoords(inPoint.X + num4/2.0, inPoint.Y - 5.0*num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element5 as IElement;
                point.PutCoords(inPoint.X + 3.0*num4/2.0, inPoint.Y - num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element6 as IElement;
                point.PutCoords(inPoint.X + 3.0*num4/2.0, inPoint.Y - 5.0*num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element7 as IElement;
                point.PutCoords(inPoint.X + 5.0*num4/2.0, inPoint.Y - num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element8 as IElement;
                point.PutCoords(inPoint.X + 5.0*num4/2.0, inPoint.Y - 3.0*num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element9 as IElement;
                point.PutCoords(inPoint.X + 5.0*num4/2.0, inPoint.Y - 5.0*num3/2.0);
                item.Geometry = point;
                ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawLegend()
        {
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
            new PointClass();
        }

        public override void DrawOutFrame()
        {
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            IElement item = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            var points = polyline as IPointCollection;
            var missing = Type.Missing;
            try
            {
                if (!(LeftUp == null || LeftUp.IsEmpty))
                {
                    inPoint.PutCoords(LeftUp.X - InOutDist, LeftUp.Y + InOutDist);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                if (!(LeftLow == null || LeftLow.IsEmpty))
                {
                    point2.PutCoords(LeftLow.X - InOutDist, LeftLow.Y - InOutDist);
                    points.AddPoint(point2, ref missing, ref missing);
                }
                if (!(RightLow == null || RightLow.IsEmpty))
                {
                    point4.PutCoords(RightLow.X + InOutDist, RightLow.Y - InOutDist);
                    points.AddPoint(point4, ref missing, ref missing);
                }
                if (!(RightUp == null || RightUp.IsEmpty))
                {
                    point3.PutCoords(RightUp.X + InOutDist, RightUp.Y + InOutDist);
                    points.AddPoint(point3, ref missing, ref missing);
                }
                if (!(LeftUp == null || LeftUp.IsEmpty))
                {
                    inPoint.PutCoords(LeftUp.X - InOutDist, LeftUp.Y + InOutDist);
                    points.AddPoint(inPoint, ref missing, ref missing);
                }
                item.Geometry = polyline;
                properties = item as IElementProperties2;
                properties.Type = "外框";
                symbol = method_3();
                element2 = item as ILineElement;
                element2.Symbol = symbol;
                ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawRemark()
        {
            var num = 45.0;
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = new TextElementClass();
            ITextElement element3 = new TextElementClass();
            ITextElement element4 = new TextElementClass();
            ITextElement element5 = new TextElementClass();
            ITextElement element6 = new TextElementClass();
            ITextElement element7 = new TextElementClass();
            ITextSymbol symbol = null;
            var num2 = 0.0;
            var num3 = 0.0;
            try
            {
                element2.Text = _mapRightUpText;
                symbol = FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight,
                    esriTextVerticalAlignment.esriTVABottom);
                element2.Symbol = symbol;
                element3.Text = _mapRightLowText;
                symbol = FontStyle(15.0, esriTextHorizontalAlignment.esriTHARight,
                    esriTextVerticalAlignment.esriTVATop);
                element3.Symbol = symbol;
                element4.Text = _mapLeftLowText;
                symbol = FontStyle(15.0, esriTextHorizontalAlignment.esriTHALeft,
                    esriTextVerticalAlignment.esriTVATop);
                element4.Symbol = symbol;
                element5.Text = toVerticalStr(_mapLeftBorderOutText);
                symbol = FontStyle(20.0, esriTextHorizontalAlignment.esriTHALeft,
                    esriTextVerticalAlignment.esriTVATop);
                element5.Symbol = symbol;
                element7.Text = _mapLeftBorderOutText;
                element7.Symbol = symbol;
                calculateTextDelta(element5, out num3, out num2);
                element6.Text = _mapScaleText;
                symbol = FontStyle(15.0, esriTextHorizontalAlignment.esriTHACenter,
                    esriTextVerticalAlignment.esriTVATop);
                element6.Symbol = symbol;
                item = element2 as IElement;
                point.PutCoords(RightUp.X, RightUp.Y + InOutDist + num);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element3 as IElement;
                point.PutCoords(RightLow.X, RightLow.Y - InOutDist - num);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element4 as IElement;
                point.PutCoords(LeftLow.X, LeftLow.Y - InOutDist - num);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element5 as IElement;
                point.PutCoords(LeftLow.X - InOutDist - num, LeftLow.Y + num2);
                item.Geometry = point;
                ilist_0.Add(item);
                item = element6 as IElement;
                point.PutCoords((LeftLow.X + RightLow.X)/2.0, LeftLow.Y - InOutDist - num);
                item.Geometry = point;
                ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void DrawTitle()
        {
            try
            {
                IPoint point = new PointClass();
                point.PutCoords((LeftUp.X + RightUp.X)/2.0, RightUp.Y + InOutDist + TitleDist);
                IElement item = new TextElementClass
                {
                    Geometry = point
                };
                var element2 = item as ITextElement;
                element2.Text = MapTM + "\n" + MapTH;
                var properties = element2 as IElementProperties2;
                var symbol = FontStyle(25.0, esriTextHorizontalAlignment.esriTHACenter,
                    esriTextVerticalAlignment.esriTVABottom);
                element2.Symbol = symbol;
                properties.Type = "图名";
                ilist_0.Add(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //转换到竖排文字
        private string toVerticalStr(string inStr)
        {
            var str = "\n";
            var startIndex = 0;
            var str2 = "";
            for (startIndex = 0; startIndex < inStr.Trim().Length - 1; startIndex++)
                str2 = str2 + inStr.Substring(startIndex, 1) + str;
            return str2;
        }

        //计算文字大小对应的坐标值
        private void calculateTextDelta(ITextElement pTextElement, out double xDelta, out double yDelta)
        {
            var xSize = 0.0;
            var ySize = 0.0;
            xDelta = 0.0;
            yDelta = 0.0;
            var num3 = 2.54;
            m_pActiveView.FocusMap.ReferenceScale = m_ReferenceScale;
            try
            {
                m_pActiveView.ScreenDisplay.StartDrawing(m_pActiveView.ScreenDisplay.hDC, 0);
                pTextElement.Symbol.GetTextSize(m_pActiveView.ScreenDisplay.hDC,
                    m_pActiveView.ScreenDisplay.DisplayTransformation, pTextElement.Text, out xSize, out ySize);
                m_pActiveView.ScreenDisplay.FinishDrawing();
                xDelta = xSize*(num3/72.0)/100.0*m_ReferenceScale;
                yDelta = ySize*(num3/72.0)/100.0*m_ReferenceScale;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private IGeometry createExpandKMGeometry()
        {
            var x = 0.0;
            var y = 0.0;
            var num3 = 0.0;
            var num4 = 0.0;
            IPoint inPoint = null;
            IPoint point2 = null;
            IPoint point3 = null;
            IPoint point4 = null;
            IPointCollection points = null;
            IGeometry geometry = new RingClass();
            var missing = Type.Missing;
            try
            {
                inPoint = new PointClass();
                point2 = new PointClass();
                point3 = new PointClass();
                point4 = new PointClass();
                x = LeftLow.X - 1000.0;
                y = LeftLow.Y - 1000.0;
                num3 = RightUp.X + 1000.0;
                num4 = RightUp.Y + 1000.0;
                inPoint.PutCoords(x, num4);
                point2.PutCoords(num3, num4);
                point3.PutCoords(num3, y);
                point4.PutCoords(x, y);
                points = (IPointCollection) geometry;
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                points.AddPoint(inPoint, ref missing, ref missing);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return geometry;
        }

        private void method_11()
        {
            if (ilist_0 != null)
                ilist_0.Clear();
        }

        private ILineSymbol method_2()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 1.0;
            return symbol2;
        }

        private ILineSymbol method_3()
        {
            ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color;
            symbol2.Width = 2.0;
            return symbol2;
        }

        private void method_4()
        {
            var str = "";
            var str2 = "\x00b0";
            var str3 = "'";
            var str4 = "″";
            var str5 = "";
            var num = 0L;
            var num2 = 0;
            var num3 = 0;
            var num4 = 0.0;
            var num5 = 0.0;
            var num6 = 0.0;
            var num7 = 0.0;
            var x = 0.0;
            var y = 0.0;
            var flag = false;
            var num10 = 0.0;
            var num11 = 0.0;
            IPoint point = new PointClass();
            IElement item = null;
            ITextElement element2 = null;
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft,
                esriTextVerticalAlignment.esriTVABottom);
            symbol2 = FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter,
                esriTextVerticalAlignment.esriTVABottom);
            symbol4 = FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            x = LeftLow.X;
            y = LeftLow.Y;
            var tools = new THTools();
            str = MapTH.Trim();
            if (str != "")
            {
                if (str.Contains("-"))
                    flag = tools.FileName2BL_cqtx(str, ref num5, ref num4, ref num6, ref num7);
                else
                    flag = tools.FileName2BL_tx(str, out num5, out num4, out num6, out num7);
                if (flag)
                {
                    THTools.DEG2DDDMMSS(num4, ref num, ref num2, ref num3);
                    str5 = num.ToString() + str2 + num2.ToString() + str3 + num3.ToString() + str4;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString();
                    element2.Symbol = symbol;
                    calculateTextDelta(element2, out num10, out num11);
                    element2.Text = str5;
                    point.PutCoords(x - num10, y - InOutDist);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = str5;
                    element2.Symbol = symbol2;
                    point.PutCoords(LeftUp.X - num10, LeftUp.Y + InOutDist);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    num4 += num6;
                    THTools.DEG2DDDMMSS(num4, ref num, ref num2, ref num3);
                    str5 = num.ToString() + str2 + num2.ToString() + str3 + num3.ToString() + str4;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2 + str4;
                    element2.Symbol = symbol;
                    calculateTextDelta(element2, out num10, out num11);
                    element2.Text = str5;
                    point.PutCoords(RightLow.X - num10, RightLow.Y - InOutDist);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = str5;
                    element2.Symbol = symbol2;
                    point.PutCoords(RightUp.X - num10, RightUp.Y + InOutDist);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    THTools.DEG2DDDMMSS(num5, ref num, ref num2, ref num3);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(LeftLow.X - InOutDist/2.0, LeftLow.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Symbol = symbol4;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    point.PutCoords(LeftLow.X - InOutDist*9.0/10.0, LeftLow.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(LeftUp.X - InOutDist/2.0, LeftUp.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    element2.Symbol = symbol4;
                    point.PutCoords(LeftUp.X - InOutDist*9.0/10.0, LeftUp.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    num5 += num7;
                    THTools.DEG2DDDMMSS(num5, ref num, ref num2, ref num3);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(RightLow.X + InOutDist/2.0, RightLow.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Symbol = symbol4;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    point.PutCoords(RightLow.X + InOutDist*1.0/10.0, RightLow.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    calculateTextDelta(element2, out num10, out num11);
                    double_2 = num10;
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num.ToString() + str2;
                    element2.Symbol = symbol3;
                    point.PutCoords(RightUp.X + InOutDist/2.0, RightUp.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                    item = new TextElementClass();
                    element2 = item as ITextElement;
                    element2.Text = num2.ToString() + str3 + num3.ToString() + str4;
                    element2.Symbol = symbol4;
                    point.PutCoords(RightUp.X + InOutDist*1.0/10.0, RightUp.Y);
                    item.Geometry = point;
                    ilist_0.Add(item);
                }
            }
        }

        private IPolygon method_5()
        {
            IGeometryCollection geometrys = new PolygonClass();
            IRing inGeometry = new RingClass();
            IRing ring2 = new RingClass();
            IPointCollection points = null;
            IPointCollection points2 = null;
            var missing = Type.Missing;
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            points = inGeometry as IPointCollection;
            points.AddPoint(LeftUp, ref missing, ref missing);
            points.AddPoint(RightUp, ref missing, ref missing);
            points.AddPoint(RightLow, ref missing, ref missing);
            points.AddPoint(LeftLow, ref missing, ref missing);
            inGeometry.Close();
            geometrys.AddGeometry(inGeometry, ref missing, ref missing);
            inPoint.PutCoords(LeftUp.X - InOutDist, LeftUp.Y + InOutDist);
            point4.PutCoords(LeftLow.X - InOutDist, LeftLow.Y - InOutDist);
            point3.PutCoords(RightLow.X + InOutDist, RightLow.Y - InOutDist);
            point2.PutCoords(RightUp.X + InOutDist, RightUp.Y + InOutDist);
            points2 = ring2 as IPointCollection;
            points2.AddPoint(inPoint, ref missing, ref missing);
            points2.AddPoint(point2, ref missing, ref missing);
            points2.AddPoint(point3, ref missing, ref missing);
            points2.AddPoint(point4, ref missing, ref missing);
            ring2.Close();
            geometrys.AddGeometry(ring2, ref missing, ref missing);
            return geometrys as IPolygon;
        }

        private IPointCollection method_6(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            var @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return multipoint as IPointCollection;
        }

        private ISymbol method_7()
        {
            IFillSymbol symbol = null;
            ILineSymbol symbol2 = null;
            IColor color = null;
            color = new RgbColorClass
            {
                NullColor = true
            };
            symbol = new SimpleFillSymbolClass();
            symbol2 = new SimpleLineSymbolClass();
            symbol.Color = color;
            IRgbColor color2 = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            symbol2.Color = color2;
            symbol2.Width = 1.0;
            symbol.Outline = symbol2;
            return symbol as ISymbol;
        }

        private IElement method_8()
        {
            var missing = Type.Missing;
            IPointCollection points = null;
            IElement element = new PolygonElementClass();
            var element2 = element as IFillShapeElement;
            new RgbColorClass();
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = null;
            IGeometryCollection geometrys = new PolygonClass();
            IGeometry geometry3 = null;
            try
            {
                points = (IPointCollection) inGeometry;
                points.AddPoint(LeftLow, ref missing, ref missing);
                points.AddPoint(RightLow, ref missing, ref missing);
                points.AddPoint(RightUp, ref missing, ref missing);
                points.AddPoint(LeftUp, ref missing, ref missing);
                points.AddPoint(LeftLow, ref missing, ref missing);
                geometry2 = createExpandKMGeometry();
                if (!geometry2.IsEmpty)
                {
                    geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                    geometrys.AddGeometry(geometry2, ref missing, ref missing);
                }
                geometry3 = geometrys as IGeometry;
                if (!geometry3.IsEmpty)
                {
                    geometry3.SpatialReference = geometry2.SpatialReference;
                    element.Geometry = geometry3;
                    element2.Symbol = GetBackStyle();
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return element;
        }

        private IEnvelope ExpandByDist(double leftX, double rightX, double topY, double bottomY)
        {
            IPoint inPoint = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            var missing = Type.Missing;
            IEnvelope envelope = null;
            IPointCollection points = null;
            IPolygon polygon = new PolygonClass();
            try
            {
                inPoint.PutCoords(LeftLow.X - leftX, LeftLow.Y - bottomY);
                point2.PutCoords(RightLow.X + rightX, RightLow.Y - bottomY);
                point3.PutCoords(RightUp.X + rightX, RightUp.Y + topY);
                point4.PutCoords(LeftUp.X - leftX, LeftUp.Y + topY);
                points = (IPointCollection) polygon;
                points.AddPoint(inPoint, ref missing, ref missing);
                points.AddPoint(point2, ref missing, ref missing);
                points.AddPoint(point3, ref missing, ref missing);
                points.AddPoint(point4, ref missing, ref missing);
                polygon.Close();
                if (!polygon.IsEmpty)
                    envelope = polygon.Envelope;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return envelope;
        }

        public string MapLeftBorderOutText
        {
            set { _mapLeftBorderOutText = value; }
        }

        public string MapLeftLowText
        {
            set { _mapLeftLowText = value; }
        }

        public string MapRightLowText
        {
            set { _mapRightLowText = value; }
        }

        public string MapRightUpText
        {
            set { _mapRightUpText = value; }
        }

        public string MapRow1Col1Text
        {
            set { _mapRow1Col1Text = value; }
        }

        public string MapRow1Col2Text
        {
            set { _mapRow1Col2Text = value; }
        }

        public string MapRow1Col3Text
        {
            set { _mapRow1Col3Text = value; }
        }

        public string MapRow2Col1Text
        {
            set { _mapRow2Col1Text = value; }
        }

        public string MapRow2Col3Text
        {
            set { _mapRow2Col3Text = value; }
        }

        public string MapRow3Col1Text
        {
            set { _mapRow3Col1Text = value; }
        }

        public string MapRow3Col2Text
        {
            set { _mapRow3Col2Text = value; }
        }

        public string MapRow3Col3Text
        {
            set { _mapRow3Col3Text = value; }
        }

        public string MapScaleText
        {
            set { _mapScaleText = value; }
        }
    }
}
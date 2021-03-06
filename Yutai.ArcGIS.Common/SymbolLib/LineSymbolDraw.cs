using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class LineSymbolDraw : StyleDraw
    {
        private bool bool_0 = false;

        public LineSymbolDraw(ISymbol isymbol_0) : base(isymbol_0)
        {
        }

        public LineSymbolDraw(ISymbol isymbol_0, bool bool_1) : base(isymbol_0)
        {
            this.bool_0 = bool_1;
        }

        private void method_0(int int_0, Rectangle rectangle_0, double double_0, double double_1)
        {
            IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            tagRECT tagRECT;
            tagRECT.left = rectangle_0.Left;
            tagRECT.right = rectangle_0.Right;
            tagRECT.bottom = rectangle_0.Bottom;
            tagRECT.top = rectangle_0.Top;
            displayTransformation.set_DeviceFrame(ref tagRECT);
            displayTransformation.Bounds = envelope;
            displayTransformation.Resolution = double_0;
            displayTransformation.ReferenceScale = 1.0;
            displayTransformation.ScaleRatio = double_1;
            ISymbol symbol = this.m_pStyle as ISymbol;
            symbol.SetupDC(int_0, displayTransformation);
            if (symbol is IPictureLineSymbol)
            {
                if (((IPictureLineSymbol) symbol).Picture == null)
                {
                    return;
                }
            }
            else if (symbol is IMarkerLineSymbol || symbol is IHashLineSymbol)
            {
                ITemplate template = ((ILineProperties) symbol).Template;
                if (template != null)
                {
                    bool flag = false;
                    int i = 0;
                    while (i < template.PatternElementCount)
                    {
                        double num;
                        double num2;
                        template.GetPatternElement(i, out num, out num2);
                        if (num + num2 <= 0.0)
                        {
                            i++;
                        }
                        else
                        {
                            flag = true;
                            IL_140:
                            if (flag)
                            {
                                object value = System.Reflection.Missing.Value;
                                IPointCollection pointCollection = new Polyline();
                                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                                if (this.bool_0)
                                {
                                    point.PutCoords((double) (rectangle_0.Left + 2), (double) (rectangle_0.Bottom + 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double) ((rectangle_0.Width - 4)/3 + rectangle_0.Left + 2),
                                        (double) (rectangle_0.Top - 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double) ((rectangle_0.Width - 4)/3*2 + rectangle_0.Left + 2),
                                        (double) (rectangle_0.Bottom + 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double) (rectangle_0.Right - 2), (double) (rectangle_0.Top - 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                }
                                else
                                {
                                    point.PutCoords((double) (rectangle_0.Left + 3),
                                        (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double) (rectangle_0.Right - 3),
                                        (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                }
                                symbol.Draw((IGeometry) pointCollection);
                                symbol.ResetDC();
                            }
                            return;
                        }
                    }
                    ;
                }
            }
        }

        public override void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1)
        {
            if (this.bool_0)
            {
                this.method_0(int_0, rectangle_0, double_0, double_1);
            }
            else
            {
                IStyleGalleryClass styleGalleryClass = new LineSymbolStyleGalleryClass();
                tagRECT tagRECT = default(tagRECT);
                double num = (double) rectangle_0.Width*double_1;
                double num2 = (double) rectangle_0.Height*double_1;
                num -= (double) rectangle_0.Width;
                num2 -= (double) rectangle_0.Height;
                rectangle_0.Inflate((int) num, (int) num2);
                tagRECT.left = rectangle_0.Left;
                tagRECT.right = rectangle_0.Right;
                tagRECT.top = rectangle_0.Top;
                tagRECT.bottom = rectangle_0.Bottom;
                styleGalleryClass.Preview(this.m_pStyle, int_0, ref tagRECT);
            }
        }
    }
}
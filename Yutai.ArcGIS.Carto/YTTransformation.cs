using System;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    internal class YTTransformation
    {
        private IActiveView _pFocusMapActiveView = null;
        private IActiveView _pActiveView = null;

        public YTTransformation(IActiveView pActiveView)
        {
            if (pActiveView is IPageLayout)
            {
                this._pFocusMapActiveView = pActiveView.FocusMap as IActiveView;
                this._pActiveView = pActiveView;
            }
            else
            {
                this._pFocusMapActiveView = pActiveView;
            }
        }

        public void TextWidth(ITextElement pTextElement, out double xDelta, out double yDelta)
        {
            IActiveView view = this._pActiveView;
            if (view == null)
            {
                view = this._pFocusMapActiveView;
            }
            double xSize = 0.0;
            double ySize = 0.0;
            xDelta = 0.0;
            yDelta = 0.0;
            double num3 = 2.54;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                view.ScreenDisplay.StartDrawing(view.ScreenDisplay.hDC, 0);
                pTextElement.Symbol.GetTextSize(view.ScreenDisplay.hDC, view.ScreenDisplay.DisplayTransformation,
                    pTextElement.Text, out xSize, out ySize);
                (pTextElement as IElement).QueryBounds(view.ScreenDisplay, bounds);
                view.ScreenDisplay.FinishDrawing();
                if (view is IPageLayout)
                {
                    xDelta = xSize*(num3/72.0);
                    yDelta = ySize*(num3/72.0);
                }
                else
                {
                    xDelta = ((xSize*(num3/72.0))/100.0)*view.FocusMap.ReferenceScale;
                    yDelta = ((ySize*(num3/72.0))/100.0)*view.FocusMap.ReferenceScale;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public IPoint ToMapPoint(IPoint pPoint)
        {
            int num;
            int num2;
            if (this._pActiveView == null)
            {
                return pPoint;
            }
            this._pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(pPoint, out num, out num2);
            return this._pFocusMapActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
        }

        public IPoint ToMapPoint(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return this.ToMapPoint(point);
        }

        public IPoint ToPageLayoutPoint(IPoint pPoint)
        {
            int num;
            int num2;
            if (this._pActiveView == null)
            {
                return pPoint;
            }
            this._pFocusMapActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(pPoint, out num, out num2);
            return this._pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
        }

        public IPoint ToPageLayoutPoint(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return this.ToPageLayoutPoint(point);
        }

        public IEnvelope MapExtent
        {
            get { return this._pFocusMapActiveView.Extent; }
        }
    }
}
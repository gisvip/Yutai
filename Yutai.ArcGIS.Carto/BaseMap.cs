using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    internal abstract class BaseMap
    {
        private bool _needLegend = false;
        private bool _needGrid = false;
        private double _inOutDist = 0.0;
        private double _titleDist = 0.0;
        private IPoint _leftUp = null;
        private IPoint _leftLow = null;
        private IPoint _rightUp = null;
        private IPoint _rightLow = null;
        protected ICompositeGraphicsLayer m_GraphicsLayer = null;
        protected bool m_IsAdminSys = true;
        protected IActiveView m_pActiveView = null;
        protected double m_ReferenceScale = 10000.0;
        private string _mapType = null;
        private string _mapTM = "";
        private string _mapTH = "";
        protected string styleFile = "";

        protected BaseMap()
        {
        }

        public bool Delete()
        {
            return true;
        }

        public abstract void Draw();

        public virtual void DrawBackFrame()
        {
        }

        public abstract void DrawInsideFrame();
        public abstract void DrawLegend();
        public abstract void DrawOutFrame();
        public abstract void DrawRemark();
        public abstract void DrawTitle();

        protected ITextSymbol FontStyle(double size, esriTextHorizontalAlignment textHorizontalAlignment,
            esriTextVerticalAlignment textVerticalAlignment)
        {
            ITextSymbol symbol = new TextSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Blue = 0,
                Red = 0,
                Green = 0
            };
            symbol.Size = size;
            symbol.Color = color;
            symbol.HorizontalAlignment = textHorizontalAlignment;
            symbol.VerticalAlignment = textVerticalAlignment;
            return symbol;
        }

        public IFillSymbol GetBackStyle()
        {
            ISimpleFillSymbol symbol = new SimpleFillSymbolClass();
            new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass
            {
                Red = 255,
                Blue = 255,
                Green = 255
            };
            symbol.Color = color;
            symbol.Outline = null;
            return symbol;
        }

        public bool Load()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public IActiveView ActiveView
        {
            set { this.m_pActiveView = value; }
        }

        public ICompositeGraphicsLayer GraphicsLayer
        {
            set { this.m_GraphicsLayer = value; }
        }

        public double InOutDist
        {
            get { return this._inOutDist; }
            set { this._inOutDist = value; }
        }

        public IPoint LeftLow
        {
            get { return this._leftLow; }
            set { this._leftLow = value; }
        }

        public IPoint LeftUp
        {
            get { return this._leftUp; }
            set { this._leftUp = value; }
        }

        public double MapReferenceScale
        {
            set { this.m_ReferenceScale = value; }
        }

        public string MapTH
        {
            get { return this._mapTH; }
            set { this._mapTH = value; }
        }

        public string MapTM
        {
            get { return this._mapTM; }
            set { this._mapTM = value; }
        }

        public string MapType
        {
            get { return this._mapType; }
            set { this._mapType = value; }
        }

        public bool NeedGrid
        {
            get { return this._needGrid; }
            set { this._needGrid = value; }
        }

        public bool NeedLegend
        {
            get { return this._needLegend; }
            set { this._needLegend = value; }
        }

        public IPoint RightLow
        {
            get { return this._rightLow; }
            set { this._rightLow = value; }
        }

        public IPoint RightUp
        {
            get { return this._rightUp; }
            set { this._rightUp = value; }
        }

        public string StyleFile
        {
            get { return this.styleFile; }
            set { this.styleFile = value; }
        }

        public double TitleDist
        {
            get { return this._titleDist; }
            set { this._titleDist = value; }
        }
    }
}
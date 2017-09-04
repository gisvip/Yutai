using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    
    public class CmdViewSwipe : YutaiCommand
    {
        ILayerEffectProperties pEffectLayer = new CommandsEnvironmentClass();
        public CmdViewSwipe(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            ILayer pSwipeLayer = _context.CurrentLayer;
            pEffectLayer.SwipeLayer = pSwipeLayer;//设置卷帘图层
            ICommand pCommand = new ControlsMapSwipeToolClass();
            pCommand.OnCreate(_context.MapControl);//绑定工具
            _context.MapControl.CurrentTool = pCommand as ITool;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "卷帘视图";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_view_swipe;
            base.m_name = "View_StartSwipe";
            base._key = "View_StartSwipe";
            base.m_toolTip = "对当前图层实现卷帘效果";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
    public class CmdSwipeTool : YutaiTool
    {
        private Bitmap bitmap_0;
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private Graphics graphics_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private int int_6;

        private Cursor _cursor;
        private Cursor _cursor1;

        public CmdSwipeTool(IAppContext context)
        {
            OnCreate(context);
        }


        public override void OnClick()
        {
            this.bool_0 = false;
            this.bool_1 = false;
            this.int_0 = 0;
            this.int_1 = 0;
            this.bool_2 = false;
            this.int_4 = 0;
            this.int_5 = 0;
            this.int_6 = 0;
            this.bitmap_0 = null;
            this.graphics_0 = null;

            _context.SetCurrentTool(this);
        }

        private bool method_0(int x1, int y1, int x2, int y2)
        {
            bool flag = true;
            if (Math.Abs(x2) < Math.Abs(x1))
            {
                flag = false;
            }
            if (Math.Abs(y2) < Math.Abs(y1))
            {
                flag = false;
            }
            return flag;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "卷帘";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_view_swipe;
            //_cursor = new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.Hand.cur"));
            //_cursor1 =
            //    new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.MoveHand.cur"));
            base.m_name = "View_Swipe";
            base._key = "View_Swipe";
            base.m_toolTip = "卷帘";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }


        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                this.StopSwipe();
                this.PrepareSwipe(_context.ActiveView as IActiveView);
                this.bool_0 = false;
                this.bool_1 = true;
                this.int_0 = x;
                this.int_1 = y;
            }
            else
            {
                this.bool_1 = false;
            }
        }



        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            if (this.bool_1)
            {
                int num = (_context.ActiveView as IActiveView).get_ScreenCacheID(esriViewDrawPhase.esriViewBackground, null);
                this.int_2 = x - this.int_0;
                this.int_3 = y - this.int_1;
                if (!this.method_0(this.int_5, this.int_6, this.int_2, this.int_3))
                {
                    tagRECT deviceFrame = (_context.ActiveView as IActiveView).ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                    tagRECT cacheRect = (_context.ActiveView as IActiveView).ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                    (_context.ActiveView as IActiveView).ScreenDisplay.DrawCache((_context.ActiveView as IActiveView).ScreenDisplay.hDC, (short)num, ref deviceFrame, ref cacheRect);
                }
                IntPtr hwnd = new IntPtr(this.int_4);
                Graphics.FromHwnd(hwnd).DrawImageUnscaled(this.bitmap_0, this.int_0, this.int_1, this.int_2, this.int_3);
                this.int_5 = this.int_2;
                this.int_6 = this.int_3;
                this.bool_2 = true;
            }
        }



        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            this.bool_1 = false;
        }

        public bool PrepareSwipe(IActiveView iactiveView_0)
        {
            if (iactiveView_0 is IMap)
            {
                IMap map = iactiveView_0 as IMap;
                if (map.LayerCount <= 0)
                {
                    return false;
                }
                this.int_4 = iactiveView_0.ScreenDisplay.hWnd;
                tagRECT deviceFrame = iactiveView_0.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                int right = deviceFrame.right;
                int bottom = deviceFrame.bottom;
                if (this.bitmap_0 != null)
                {
                    this.bitmap_0.Dispose();
                    this.graphics_0.Dispose();
                    this.graphics_0 = null;
                }
                this.bitmap_0 = new Bitmap(deviceFrame.right - deviceFrame.left, deviceFrame.bottom - deviceFrame.top);
                this.graphics_0 = Graphics.FromImage(this.bitmap_0);
                ITrackCancel trackCancel = new CancelTrackerClass();
                IEnvelope extent = iactiveView_0.Extent;
                tagRECT pixelBounds = new tagRECT
                {
                    bottom = bottom,
                    left = 0,
                    right = right,
                    top = 0
                };
                ILayer currentLayer = _context.CurrentLayer;
                currentLayer.Visible = false;
                IntPtr hdc = this.graphics_0.GetHdc();
                iactiveView_0.Output(hdc.ToInt32(), (int)iactiveView_0.ScreenDisplay.DisplayTransformation.Resolution, ref pixelBounds, extent, trackCancel);
                this.graphics_0.ReleaseHdc(hdc);
                currentLayer.Visible = true;
                currentLayer = null;
                return true;
            }
            return false;
        }

        public bool StopSwipe()
        {
            if (this.bitmap_0 != null)
            {
                this.bitmap_0.Dispose();
                this.bitmap_0 = null;
                this.graphics_0.Dispose();
                this.graphics_0 = null;
            }
            this.int_5 = 0;
            this.int_6 = 0;
            return true;
        }



    }
}
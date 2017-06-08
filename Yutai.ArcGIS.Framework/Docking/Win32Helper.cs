﻿using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal static class Win32Helper
    {
        public static Control ControlAtPoint(Point pt)
        {
            return Control.FromChildHandle(NativeMethods.WindowFromPoint(pt));
        }

        public static uint MakeLong(int low, int high)
        {
            return (uint) ((high << 0x10) + low);
        }
    }
}

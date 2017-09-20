using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraTreeList;


namespace Yutai.UI.Controls
{
    public abstract class TreeViewBase : TreeList
    {
        private ToolTipController _lastTooltip = new ToolTipController();
        

       // public event EventHandler<ToolTipEventArgs> PrepareToolTip;

        protected TreeViewBase()
        {
           
            
        }

       
    }
}
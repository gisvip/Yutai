using System.ComponentModel;


namespace Yutai.UI.Controls
{
    public class ToolTipEventArgs : CancelEventArgs
    {
        public ToolTipEventArgs(ToolTipInfo info)
        {
            ToolTip = info;
        }

        public ToolTipInfo ToolTip { get; private set; }
    }

    public class ToolTipInfo
    {
        public string Title { get; set; }
        public string Text { get; set; }
        
    }
}
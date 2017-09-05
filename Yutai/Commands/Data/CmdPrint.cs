using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;

namespace Yutai.Commands.Data
{
    public class CmdPrint : YutaiCommand
    {
     
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdPrint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            CMapPrinter cMapPrinter;
            cMapPrinter = new CMapPrinter(this._context.FocusMap);
            cMapPrinter.showPrintUI("打印地图");

        }
      
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打印";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_print;
            base.m_name = "File_Print";
            base._key = "File_Print";
            base.m_toolTip = "打印";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
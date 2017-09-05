using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Data
{
    public class CmdPrintPageSetup : YutaiCommand
    {
        private IPageLayoutControl ipageLayoutControl_0 = null;
        public override bool Enabled
        {
            get
            {
                bool flag;
             
                 
                if (this._context.MainView.ControlType == GISControlType.PageLayout)
                {
                    this.ipageLayoutControl_0 = this._context.MainView.PageLayoutControl as IPageLayoutControl;
                    flag = true;
                    return flag;
                }
                 
                flag = false;
                return flag;
            }
        }
        public CmdPrintPageSetup(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            this.ipageLayoutControl_0 = this._context.Hook as IPageLayoutControl;
            if (this.ipageLayoutControl_0 != null)
            {
                try
                {
                    FormPrinterSetup formPrinterSetup = new FormPrinterSetup();
                    formPrinterSetup.setPageLayout(ref this.ipageLayoutControl_0);
                    formPrinterSetup.ShowDialog();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show("无法启动打印!");
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "打印设置";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_pagesetup;
            base.m_name = "File_PrintPageSetup";
            base._key = "File_PrintPageSetup";
            base.m_toolTip = "打印设置";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
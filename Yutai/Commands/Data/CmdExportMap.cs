using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Controls.Controls.Export;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Data
{
    public class CmdExportMap : YutaiCommand
    {

        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdExportMap(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            try
            {
                frmExportMap _frmExportMap = new frmExportMap()
                {
                    ActiveView = this._context.ActiveView
                };
                _frmExportMap.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "导出地图";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_export_map;
            base.m_name = "File_ExportMap";
            base._key = "File_ExportMap";
            base.m_toolTip = "导出地图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
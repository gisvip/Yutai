using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;

namespace Yutai.Commands.Data
{
    public class CmdAddData : YutaiCommand
    {
        private AddDataHelper pHelper = null;

        private IList ilist_0 = null;
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null;
            }
        }
        public CmdAddData(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {

           
            frmOpenFile _frmOpenFile = new frmOpenFile()
            {
                Text = "添加数据",
                AllowMultiSelect = true
            };
            _frmOpenFile.AddFilter(new MyGxFilterDatasets(), true);
            if (_frmOpenFile.DoModalOpen() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.pHelper = new AddDataHelper(this.GetMap() as IActiveView);
                this.ilist_0 = _frmOpenFile.SelectedItems;
                this.pHelper.m_pApp = _context;
                this.pHelper.LoadData(this.ilist_0);
                
                Cursor.Current = Cursors.Default;
            }

        }


        private void method_2()
        {
            Cursor.Current = Cursors.Default;
            (this.GetMap() as IActiveView).ScreenDisplay.UpdateWindow();
        }

      


        private IMap GetMap()
        {
            return this._context.FocusMap;
        }

        private void method_1()
        {
            this.pHelper.InvokeMethod(this.ilist_0);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "添加数据";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_add_data;
            base.m_name = "File_AddData";
            base._key = "File_AddData";
            base.m_toolTip = "添加数据";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}

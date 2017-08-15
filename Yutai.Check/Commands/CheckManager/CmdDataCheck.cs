﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Check.Classes;
using Yutai.Check.Forms;
using Yutai.Check.Services;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Commands.CheckManager
{
    class CmdDataCheck : YutaiTool
    {
        private CheckPlugin _plugin;
        private FrmDataCheck _frmDataCheck;
        private CheckResultDockPanelService _dockPanelService;
        private IDictionary<int, IField> _selectedFields;
        private List<FeatureItem> _featureItems;
        private IDataCheck _dataCheck;
        private IFeatureLayer _featureLayer;

        public CmdDataCheck(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _dataCheck = new DataCheckBase(_context, _plugin.PipeConfig);
            if (_frmDataCheck == null)
                _frmDataCheck = new FrmDataCheck(_dataCheck);
            if (_frmDataCheck.ShowDialog() != DialogResult.OK)
                return;
            _dataCheck.CheckPipelineList = _frmDataCheck.GetCheckPipeline();
            _featureItems = _dataCheck.Check(_frmDataCheck.GetCheckItems());

            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<CheckResultDockPanelService>();
            _dockPanelService.View.FeatureItems = _featureItems;
            _dockPanelService.View.ReloadData();

            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "数据检查";
            base.m_category = "Check_Pipeline";
            base.m_name = "Check_Pipeline_DataCheck";
            base._key = "Check_Pipeline_DataCheck";
            base.m_toolTip = "数据检查。";
            base.m_checked = false;
            base.m_message = "数据检查";
            base._itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                return true;
            }
        }
    }
}

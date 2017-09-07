using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<FeatureItem> _featureItems;
        private IDataCheck _dataCheck;
        private WaitForm _waitForm;
        public CmdDataCheck(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
            _waitForm = new WaitForm();
            _waitForm.Worker.DoWork += BackgroundWorkerOnDoWork;
            _waitForm.Worker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<CheckResultDockPanelService>();
            _dockPanelService.View.FeatureItems = _featureItems;
            _dockPanelService.View.ReloadData();

            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker == null)
                    return;
                _dataCheck.ProgressChanged += (o, s) =>
                {
                    worker.ReportProgress(0, s);
                };
                _dataCheck.CheckPipelineList = _frmDataCheck.GetCheckPipeline();
                _featureItems = _dataCheck.Check(_frmDataCheck.GetCheckItems(), worker);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _dataCheck = new DataCheckBase(_context, _plugin.PipeConfig, _plugin.DataCheckConfig);
            if (_frmDataCheck == null)
                _frmDataCheck = new FrmDataCheck(_dataCheck);
            _frmDataCheck.InitCheckItem(_plugin);
            if (_frmDataCheck.ShowDialog() != DialogResult.OK)
                return;

            _waitForm.TopMost = true;
            _waitForm.Show();
            _waitForm.Worker.RunWorkerAsync();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "数据检查";
            base.m_category = "Check_Pipeline";
            base.m_bitmap = Properties.Resources.icon_Check;
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

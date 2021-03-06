﻿using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Template.Forms;

namespace Yutai.Plugins.Template.Commands
{
    class CmdCreateFeatureDatasetByTemplate : YutaiCommand
    {
        private TemplatePlugin _plugin;



        public CmdCreateFeatureDatasetByTemplate(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as TemplatePlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_template_fc;
            this.m_caption = "创建要素组";
            this.m_category = "Template";
            this.m_message = "创建要素组";
            this.m_name = "Template_CreateFeatureDataset";
            this._key = "Template_CreateFeatureDataset";
            this.m_toolTip = "创建要素组";
            _context = hook as IAppContext;
            _itemType = RibbonItemType.Button;
            _needUpdateEvent = true;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }



        public override void OnClick()
        {
            frmQuickCreateFeatureDataset frm = new frmQuickCreateFeatureDataset(_context, _plugin);
            frm.ShowDialog();
        }
    }
}
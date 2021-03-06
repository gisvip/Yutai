﻿// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdInvertSelection.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/08  18:46
// 更新时间 :  2017/06/08  18:46

using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdInvertSelection : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdInvertSelection(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "切换选择";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedSelection.mnuInvertSelection";
            base._key = "tedSelection.mnuInvertSelection";
            base.m_toolTip = "切换选择";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        public override void OnClick()
        {
            ITableView pGridView = _view.CurrentGridView;
            if (pGridView == null)
                return;
            if (pGridView.CurrentOID == -1)
                return;
            _view.CurrentGridView.VirtualGridView.InvertSelection();
        }
    }
}
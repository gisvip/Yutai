﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Commands;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Enums;
using Yutai.Plugins.TableEditor.Menu;
using Yutai.UI.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class TableEditorDockPanel : DockPanelControlBase, ITableEditorView
    {
        private readonly IAppContext _context;
        private List<YutaiCommand> _commands;
        public TableEditorDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            InitMenu();
            GridViews = new Dictionary<int, IGridView>();
            MapView = new MapView(_context.MapControl.Map);
        }

        ~TableEditorDockPanel()
        {
        }

        private void InitMenu()
        {
            CreateCommands();
            foreach (YutaiCommand yutaiCommand in _commands)
            {
                AddCommand(yutaiCommand);
            }
        }

        private void AddCommand(YutaiCommand command)
        {
            if (command is YutaiMenuCommand)
            {
                ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton();
                toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripDropDownButton.Name = command.Name;
                toolStripDropDownButton.Text = command.Caption;
                toolStrip.Items.Add(toolStripDropDownButton);
            }
            else if (command is YutaiSeparatorCommand)
            {
                if (string.IsNullOrWhiteSpace(command.Key))
                {
                    toolStrip.Items.Add(new ToolStripSeparator());
                }
                else
                {
                    ToolStripDropDownButton dropDown = toolStrip.Items[command.Key] as ToolStripDropDownButton;
                    dropDown.DropDownItems.Add(new ToolStripSeparator());
                }
            }
            else
            {
                string[] names = command.Name.Split('.');
                if (names.Length == 1)
                {
                    ToolStripMenuItem menu = new ToolStripMenuItem
                    {
                        Text = command.Caption,
                        Name = command.Name,
                        ToolTipText = command.Tooltip,
                        Image = command.Image,
                    };
                    menu.Click += command.OnClick;
                    toolStrip.Items.Add(menu);
                }
                else if (names.Length == 2)
                {
                    ToolStripDropDownButton dropDown = toolStrip.Items[names[0]] as ToolStripDropDownButton;
                    ToolStripMenuItem menu = new ToolStripMenuItem
                    {
                        Text = command.Caption,
                        Name = command.Name,
                        ToolTipText = command.Tooltip,
                        Image = command.Image,
                    };
                    menu.Click += command.OnClick;
                    dropDown.DropDownItems.Add(menu);
                }
            }
        }

        private void CreateCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>()
                {
                    new YutaiMenuCommand("tedSelection", "tedSelection","tedSelection", "选择",""),
                    new CmdZoomToCurrentFeature(_context, this),
                    new CmdZoomToSelectedFeatures(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdBuildQuery(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdSelectAll(_context, this),
                    new CmdSelectNone(_context, this),
                    new CmdInvertSelection(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdExportAll(_context, this),
                    new CmdExportSelection(_context, this),
                    new YutaiMenuCommand("tedFields", "tedFields","tedFields", "字段",""),
                    new YutaiMenuCommand("tedTools", "tedTools","tedTools", "工具",""),
                    new YutaiSeparatorCommand(),
                    new YutaiMenuCommand("tedLayout", "tedLayout","tedLayout", "布局",""),
                };
            }
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get
            {
                yield break;
            }
        }

        public IEnumerable<Control> Buttons
        {
            get
            {
                yield break;
            }
        }

        public IMapView MapView { get; }
        public Dictionary<int, IGridView> GridViews { get; set; }

        public TabControl MainTabControl
        {
            get { return tabControl; }
        }

        public TabPage CurTabPage
        {
            get { return tabControl.SelectedTab; }
            set { tabControl.SelectedTab = value; }
        }

        public TabPage CreateTabPage(IFeatureLayer featureLayer)
        {
            return CreateTabPage(featureLayer.FeatureClass);
        }

        public TabPage CreateTabPage(IFeatureClass featureClass)
        {
            return CreateTabPage(featureClass.AliasName, featureClass.ObjectClassID);
        }

        public TabPage CreateTabPage(string name, int handle)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name = string.Format("{0}_{1}", name, handle);
            tabPage.Text = name;
            tabPage.Tag = handle;
            tabControl.Controls.Add(tabPage);
            return tabPage;
        }

        public void ActivatePage(int handle)
        {
            int pageCount = tabControl.TabPages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                TabPage tabPage = tabControl.TabPages[i];
                if ((int)tabPage.Tag == handle)
                    tabPage.Select();
            }
        }

        public void ClosePage(int handle)
        {
            int pageCount = tabControl.TabPages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                TabPage tabPage = tabControl.TabPages[i];
                if ((int)tabPage.Tag == handle)
                {
                    tabControl.TabPages.Remove(tabPage);
                    GridViews.Remove(handle);
                }
            }
        }

        public void ClosePage()
        {
            int handle = (int)CurTabPage.Tag;
            tabControl.TabPages.Remove(CurTabPage);
            GridViews.Remove(handle);
        }
        
        public void OpenTable(IFeatureLayer featureLayer)
        {
            if (GridViews.ContainsKey(featureLayer.FeatureClass.ObjectClassID))
            {
                ActivatePage(featureLayer.FeatureClass.ObjectClassID);
                return;
            }

            TableEditorGrid tableEditorGrid = new TableEditorGrid(_context, this);
            tableEditorGrid.FeatureLayer = featureLayer;
            tableEditorGrid.Dock = DockStyle.Fill;
            TabPage tabPage = CreateTabPage(featureLayer.FeatureClass);
            tabPage.Controls.Add(tableEditorGrid);
            tabControl.SelectedTab = tabPage;

            GridViews.Add(featureLayer.FeatureClass.ObjectClassID, tableEditorGrid);
        }

        public void Clear()
        {
            tabControl.TabPages.Clear();
        }
        
        public IGridView CurrentGridView
        {
            get
            {
                if (CurTabPage == null)
                    return null;
                int handle = (int)CurTabPage.Tag;
                return GridViews[handle];

            }
        }

        public override Bitmap Image { get { return Properties.Resources.icon_attribute_table; } }
        public override string Caption
        {
            get { return "属性表"; }
            set { Caption = value; }
        }
        public override DockPanelState DefaultDock { get { return DockPanelState.Bottom; } }
        public override string DockName { get { return DefaultDockName; } }
        public virtual string DefaultNestDockName { get { return ""; } }
        public const string DefaultDockName = "Plug_TableEditor_View";
    }
}

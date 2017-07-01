﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Plugins.Editor.Menu
{
    internal class MenuGenerator
    {
        private readonly IAppContext _context;
        private readonly YutaiCommands _commands;
        private readonly object _menuManager;
        private readonly EditorPlugin _plugin;


        public MenuGenerator(IAppContext context, EditorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            // if (pluginManager == null) throw new ArgumentNullException("pluginManager");

            _plugin = plugin;
            _context = context;

            _menuManager = _context.MainView.RibbonManager;
            _commands = new YutaiCommands(_context, plugin.Identity);
            _commands.Plugin = plugin;
            InitMenus();
        }

        public List<string> GetMenuKeys()
        {
            return _commands.GetKeys();
        }

        private void InitMenus()
        {
            XmlDocument doc = new XmlDocument();
            //检测项目文档里面是否有插件的界面配置，如果没有，则使用默认配置，如果有，则使用配置文件里面的配置
            Guid dllGuid = new Guid("4a3bcaab-9d3e-4ca7-a19d-7ee08fb0629e");
            string resString = "Yutai.Plugins.Editor.Menu.MenuLayout.xml";
            XmlPlugin plugin =
                ((ISecureContext) _context).YutaiProject.Plugins.FirstOrDefault(
                    c => c.Guid == dllGuid);
            if (plugin != null)
            {
                if (string.IsNullOrEmpty(plugin.MenuXML))
                {
                    doc.Load(base.GetType().Assembly.GetManifestResourceStream(resString));
                }
                else
                {
                    FileInfo info = new FileInfo(FileHelper.GetFullPath(plugin.MenuXML));
                    if (info.Exists)
                        doc.Load(FileHelper.GetFullPath(plugin.MenuXML));
                    else
                        doc.Load(base.GetType().Assembly.GetManifestResourceStream(resString));
                }
            }
            else
            {
                doc.Load(base.GetType().Assembly.GetManifestResourceStream(resString));
            }

            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl) _menuManager,
                _context.MainView.RibbonStatusBar as RibbonStatusBar, doc);
        }
    }
}
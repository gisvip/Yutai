using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;
using Yutai.UI.Controls;
using Yutai.Views;

namespace Yutai.Controls
{
    public class ConfigTreeView : TreeList
    {
        private ConfigViewModel _model;
        private ImageCollection _images;

        public ConfigTreeView()
        {
            AfterExpand += ConfigTreeView_AfterExpand;
        }

        private void ConfigTreeView_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            foreach (TreeListNode node in Nodes)
            {
                if (node != e.Node)
                {
                    node.Expanded = false;
                }
            }
            e.Node.Selected = true;

            //SelectedNode = e.Node;
        }

        public void Initialize(ConfigViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");
            _model = model;

            //创建数据列
            TreeListColumn column1 = Columns.AddField("ImageIndex");
            column1.Caption = "";
            TreeListColumn column2 = Columns.AddField("ParentKey");
            column2.Visible = false;
            TreeListColumn column3 = Columns.AddVisible("Page");
            column3.Caption = "配置项";

            AddAllPages();
             SelectImageList= _images.Images;
            ImageIndexFieldName = "ImageIndex";
            ParentFieldName = "ParentKey";
            

        }

        private void AddAllPages()
        {
            int i = 0;

            foreach (var page in _model.Pages)
            {
                var node = AppendNode(new object[] {i.ToString(), page.PageName, page.ParentKey}, null);
                _images.AddImage(page.Icon,i.ToString());
                page.Tag = node;
                node.Tag = page;
            }
        }

       

        public void SetSelectedPage(string pageKey)
        {
            TreeListNode selectedNode = null;
          

            var page = _model.Pages.FirstOrDefault(p => p.Key == pageKey);
            if (page != null)
            {
                var node = NodeForPage(page);
                if (node != null)
                {
                    selectedNode = node;
                }
            }

            if (selectedNode != null)
            {
                selectedNode.Selected = true;
            }
            else
            {
                Nodes[0].Selected = true;
            }
        }


        public void RestoreSelectedNode(string lastPageKey)
        {
            TreeListNode selectedNode = null;

            if (lastPageKey == null)
            {
                lastPageKey = string.Empty;
            }

            foreach (var page in _model.Pages)
            {
                if (page.Key.ContainsIgnoreCase(lastPageKey))
                {
                    var node = NodeForPage(page);
                    if (node != null)
                    {
                        selectedNode = node;
                        if (selectedNode.ParentNode != null)
                        {
                            selectedNode.ParentNode.Expanded=true;
                        }

                        break;
                    }
                }
            }

            if (selectedNode != null)
            {
                selectedNode.Selected = true;
            }
            else
            {
                Nodes[0].Selected = true;
            }
        }

        private TreeListNode NodeForPage(IConfigPage page)
        {
            return page.Tag as TreeListNode;
        }
        
    }
}
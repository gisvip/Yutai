using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.Pipeline.Editor.Helper
{
    public class GxDialogHelper
    {
        public static IWorkspace SelectPersonalWorkspaceDialog()
        {
            frmOpenFile frm = new frmOpenFile();
            frm.AllowMultiSelect = false;
            frm.AddFilter(new MyGxFilterPersonalGeodatabases(), true);
            frm.Text = @"选择个人地理数据库";
            if (frm.DoModalOpen() == DialogResult.OK)
            {
                IGxObject gxObject = frm.Items.get_Element(0) as ArcGIS.Catalog.IGxObject;
                if (gxObject is IGxDatabase)
                {
                    IGxDatabase gxDatabase = gxObject as IGxDatabase;
                    return gxDatabase.Workspace;
                }
            }
            return null;
        }

        public static IWorkspace SelectWorkspaceDialog()
        {
            frmOpenFile frm = new frmOpenFile();
            frm.AllowMultiSelect = false;
            frm.AddFilter(new MyGxFilterWorkspaces(), true);
            frm.Text = @"选择地理数据库";
            if (frm.DoModalOpen() == DialogResult.OK)
            {
                IGxObject gxObject = frm.Items.get_Element(0) as ArcGIS.Catalog.IGxObject;
                if (gxObject is IGxDatabase)
                {
                    IGxDatabase gxDatabase = gxObject as IGxDatabase;
                    return gxDatabase.Workspace;
                }
            }
            return null;
        }
        
        public static IFeatureClass SelectFeatureClassDialog()
        {
            frmOpenFile frm = new frmOpenFile();
            frm.AllowMultiSelect = false;
            frm.AddFilter(new MyGxFilterFeatureClasses(), true);
            frm.Text = @"选择要素类";
            if (frm.DoModalOpen() == DialogResult.OK)
            {
                IGxObject gxObject = frm.Items.get_Element(0) as ArcGIS.Catalog.IGxObject;
                IFeatureClassName className = gxObject.InternalObjectName as IFeatureClassName;
                return ((IName) className).Open() as IFeatureClass;
            }
            return null;
        }
    }
}

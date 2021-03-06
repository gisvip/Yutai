﻿using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.VCT;

namespace Yutai.ArcGIS.Catalog
{
    public class GxVCTObject : IGxObject, IGxObjectContainer, IGxVCTObject, IGxFile, IGxObjectUI
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;

        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IWorkspace iworkspace_0 = null;
        internal VctFactory m_ConvertFactory;
        internal IWorkspace objws = null;
        private string string_0 = "";

        internal string TemplatePath = "";

        public GxVCTObject()
        {
            this.Category = "VCT数据";
        }

        public IGxObject AddChild(IGxObject igxObject_2)
        {
            this.igxObjectArray_0.Insert(-1, igxObject_2);
            return igxObject_2;
        }

        public void Attach(IGxObject igxObject_2, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_2;
            this.igxCatalog_0 = igxCatalog_1;
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).AddChild(this);
            }
        }

        public void Close(bool bool_0)
        {
        }

        public void DeleteChild(IGxObject igxObject_2)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_2)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectDeleted(this);
            }
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
            }
            this.igxObject_0 = null;
            this.igxCatalog_0 = null;
        }

        public void Edit()
        {
        }

        internal string GetTemplatePath()
        {
            if (this.TemplatePath.Length == 0)
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
                    Guid.NewGuid().ToString().Replace("-", ""));
                Directory.CreateDirectory(path);
                this.TemplatePath = path;
                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                this.objws = factory.OpenFromFile(path, 0);
            }
            return this.TemplatePath;
        }

        internal IWorkspace GetTemplateShapefile()
        {
            if (this.objws == null)
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
                    Guid.NewGuid().ToString().Replace("-", ""));
                Directory.CreateDirectory(path);
                this.TemplatePath = path;
                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                this.objws = factory.OpenFromFile(path, 0);
            }
            return this.objws;
        }

        internal IWorkspace GetTemplateTextWorksoace()
        {
            if (this.iworkspace_0 == null)
            {
                string templatePath = this.GetTemplatePath();
                this.iworkspace_0 = this.IWorkspaceFactory_Create_Example_Access(templatePath);
            }
            return this.iworkspace_0;
        }

        public IWorkspace IWorkspaceFactory_Create_Example_Access(string string_5)
        {
            IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
            IName name2 = (IName) factory.Create(string_5, "MyNewFileGDB.gdb", null, 0);
            return (IWorkspace) name2.Open();
        }

        private void method_0()
        {
            if (this.m_ConvertFactory == null)
            {
                this.m_ConvertFactory = new VctFactory();
            }
            foreach (ICoConvert convert in this.m_ConvertFactory.Create(this.string_0))
            {
                GxVCTLayerObject obj2 = new GxVCTLayerObject
                {
                    VCTLayer = convert
                };
                obj2.Attach(this, this.igxCatalog_0);
            }
        }

        public void New()
        {
        }

        public void Open()
        {
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.igxCatalog_0.ObjectRefreshed(this);
        }

        public void Save()
        {
        }

        public void SearchChildren(string string_5, IGxObjectArray igxObjectArray_1)
        {
        }

        public bool AreChildrenViewable
        {
            get { return true; }
        }

        public string BaseName { get; set; }

        public string Category { get; set; }

        public IEnumGxObject Children
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    this.method_0();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public string FullName { get; set; }

        public bool HasChildren
        {
            get { return true; }
        }

        public IName InternalObjectName
        {
            get { return null; }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public Bitmap LargeImage
        {
            get { return ImageLib.GetSmallImage(6); }
        }

        public Bitmap LargeSelectedImage
        {
            get { return ImageLib.GetSmallImage(7); }
        }

        public string Name { get; set; }

        public IGxObject Parent { get; set; }

        public string Path
        {
            get { return this.string_0; }
            set
            {
                this.string_0 = value;
                this.Name = System.IO.Path.GetFileNameWithoutExtension(this.string_0);
                this.FullName = this.string_0;
                this.BaseName = this.Name;
            }
        }

        public Bitmap SmallImage
        {
            get { return ImageLib.GetSmallImage(6); }
        }

        public Bitmap SmallSelectedImage
        {
            get { return ImageLib.GetSmallImage(7); }
        }
    }
}
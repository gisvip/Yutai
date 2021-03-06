﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Shared;
using ESRI.ArcGIS.DataSourcesFile;
using Yutai.Pipeline.Config.Helpers;


namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineConfig : IPipelineConfig
    {
        private List<IPipelineTemplate> _templates;
        private List<IPipelineLayer> _layers;
        private string _xmlFile;
        private IMap _map;
        private IActiveViewEvents_Event viewEvents;
        private string _configDatabaseName;
        private IFeatureWorkspace _workspace;
        private List<IPipelineLayer> _dbLayers;
        private List<string> _styleFiles;
        private List<ICommonConfig> _commonConfigs;
        private List<IFunctionLayer> _functionLayers;
        private string _projectFile;

        public PipelineConfig()
        {
            _templates = new List<IPipelineTemplate>();
            _layers = new List<IPipelineLayer>();
            _functionLayers = new List<IFunctionLayer>();
            _styleFiles = new List<string>();
            _commonConfigs = new List<ICommonConfig>();
        }

        public string ConfigDatabaseName
        {
            get { return _configDatabaseName; }
            set { _configDatabaseName = value; }
        }

        public List<string> StyleFiles
        {
            get { return _styleFiles; }
            set { _styleFiles = value; }
        }

        public List<ICommonConfig> CommonConfigs
        {
            get { return _commonConfigs; }
            set { _commonConfigs = value; }
        }

        public IFeatureWorkspace Workspace
        {
            get { return _workspace; }
            set { _workspace = value; }
        }

        public List<IPipelineTemplate> Templates
        {
            get { return _templates; }
            set { _templates = value; }
        }

        public List<IPipelineLayer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public List<IFunctionLayer> FunctionLayers
        {
            get { return _functionLayers; }
            set { _functionLayers = value; }
        }

        public string XmlFile
        {
            get { return _xmlFile; }
            set { _xmlFile = value; }
        }

        public void LoadFromXml(string fileName)
        {
            _templates.Clear();
            _layers.Clear();
            _styleFiles.Clear();
            _xmlFile = fileName;
            //首先读取Template
            XmlDocument doc = new XmlDocument();
            doc.Load(_xmlFile);

            _configDatabaseName = doc.SelectSingleNode("/PipelineConfig/ConfigDatabase").InnerText;
            InitStyleFiles(doc);
            InitCommonConfig(doc);
            string fullPath = FileHelper.GetFullPath(_configDatabaseName);
            _workspace = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetAccessWorkspace(fullPath) as IFeatureWorkspace;
            XmlNodeList nodes = doc.SelectNodes("/PipelineConfig/LayerTemplates/Template");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    IPipelineTemplate template = new PipelineTemplate(node);
                    _templates.Add(template);
                }
            nodes = doc.SelectNodes("/PipelineConfig/PipelineLayers/PipelineLayer");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    IPipelineLayer layer = new PipelineLayer(node, _templates);
                    _layers.Add(layer);
                }

            nodes = doc.SelectNodes("/PipelineConfig/FunctionLayers/FunctionLayer");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    IFunctionLayer layer = new FunctionLayer(node);
                    _functionLayers.Add(layer);
                }
        }

        private void InitCommonConfig(XmlDocument doc)
        {
            XmlNodeList nodeList = doc.SelectNodes("/PipelineConfig/CommonConfigs/CommonConfig");
            foreach (XmlNode node in nodeList)
            {
                ICommonConfig config = new CommonConfig(node);
                _commonConfigs.Add(config);
            }
        }

        private void InitStyleFiles(XmlDocument doc)
        {
            XmlNodeList nodeList = doc.SelectNodes("/PipelineConfig/Styles/Style");
            foreach (XmlNode node in nodeList)
            {
                string fullPath = FileHelper.GetFullPath(node.InnerText);
                _styleFiles.Add(fullPath);
            }
            if (_styleFiles.Count == 0)
            {
                string fullPath = Application.StartupPath + "\\Styles";
                FileInfo fileInfo = new FileInfo(fullPath + "\\ESRI.ServerStyle");

                if (fileInfo.Exists)
                {
                    _styleFiles.Add(fileInfo.FullName);
                }
                fileInfo = new FileInfo(fullPath + "\\YTPipeline.ServerStyle");

                if (fileInfo.Exists)
                {
                    _styleFiles.Add(fileInfo.FullName);
                }
            }
        }

        public void Clear()
        {
            _layers.Clear();
            _templates.Clear();
            _functionLayers.Clear();
            _projectFile = "";
            if (this.viewEvents != null)
            {
                viewEvents.ItemAdded -= ViewEventsOnItemAdded;
                viewEvents.ItemDeleted -= ViewEventsOnItemDeleted;
            }
        }

        public bool LinkMap(IMap pMap)
        {
            try
            {
                if (this.viewEvents != null)
                {
                    viewEvents.ItemAdded -= ViewEventsOnItemAdded;
                    viewEvents.ItemDeleted -= ViewEventsOnItemDeleted;
                }
            }
            catch (System.Exception ex)
            {
            }
            IActiveView pView = pMap as IActiveView;
            viewEvents = pView as IActiveViewEvents_Event;
            viewEvents.ItemAdded += ViewEventsOnItemAdded;
            viewEvents.ItemDeleted += ViewEventsOnItemDeleted;
            _map = pMap;
            LinkFeatureLayers();
            return true;
        }

        public bool IsPipelineLayer(string classAliasName)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass.AliasName == classAliasName);
                if (layer != null) return true;
            }
            return false;
        }

        public bool IsPipelineLayer(IFeatureClass pClass)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass == pClass);
                if (layer != null) return true;
            }
            return false;
        }

        public bool IsPipelineLayer(string classAliasName, enumPipelineDataType dataType)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                try
                {
                    foreach (IBasicLayerInfo basicLayer in pipelineLayer.Layers)
                    {
                        if (basicLayer.AliasName.Equals(classAliasName) && basicLayer.DataType == dataType) return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public IPipelineLayer GetPipelineLayer(string classAliasName, enumPipelineDataType dataType)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo basicLayer in pipelineLayer.Layers)
                {
                    if (basicLayer.AliasName.Equals(classAliasName) && basicLayer.DataType == dataType) return pipelineLayer;
                }
            }
            return null;
        }

        public IPipelineLayer GetPipelineLayer(IFeatureClass pClass)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo basicLayer in pipelineLayer.Layers)
                {
                    if (basicLayer.FeatureClass==pClass) return pipelineLayer;
                }
            }
            return null;
        }

        public IBasicLayerInfo GetBasicLayerInfo(IFeatureClass pClass)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo basicLayer in pipelineLayer.Layers)
                {
                    if (basicLayer.FeatureClass == pClass) return basicLayer;
                }
            }
            return null;
        }

        public IBasicLayerInfo GetBasicLayerInfo(string pClassAliasName)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo basicLayer in pipelineLayer.Layers)
                {
                    if (basicLayer.AliasName == pClassAliasName) return basicLayer;
                }
            }
            return null;
        }

        public IYTField GetSpecialField(string classAliasName, string typeWord)
        {
            IBasicLayerInfo layer = GetBasicLayerInfo(classAliasName);
            if (layer == null) return null;
            IYTField field = layer.Fields.FirstOrDefault(c => c.TypeName == typeWord);
            return field;
        }

        public IFunctionLayer GetFunctionLayer(IFeatureClass featureClass)
        {
            return _functionLayers.FirstOrDefault(c => c.AliasName == featureClass.AliasName);
        }

        public IFunctionLayer GetFunctionLayer(string aliasName)
        {
            return _functionLayers.FirstOrDefault(c => c.AliasName == aliasName);
        }

        public IFunctionLayer GetFunctionLayer(enumFunctionLayerType type)
        {
            return _functionLayers.FirstOrDefault(c => c.FunctionType == type);
        }

        public bool IsFunctionLayer(string classAliasName)
        {
            IFunctionLayer layer = _functionLayers.FirstOrDefault(c => c.AliasName == classAliasName);
            return layer != null;
        }

        public bool IsFunctionLayer(IFeatureClass featureClass)
        {
            IFunctionLayer layer = _functionLayers.FirstOrDefault(c => c.AliasName == featureClass.AliasName);
            return layer != null;
        }

        public bool IsFunctionLayer(string classAliasName, enumFunctionLayerType type)
        {
            IFunctionLayer layer = _functionLayers.FirstOrDefault(c => c.AliasName == classAliasName);
            return layer != null && layer.FunctionType == type;
        }

        //! 目前只是按照名字对照去识别管线图层，后期可能需要动态的识别管线图层，并进行判断确定是否为合法的管线图层
        private void LinkFeatureLayers()
        {
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo layerInfo in pipelineLayer.Layers)
                {
                    IFeatureLayer pLayer = CommonHelper.GetFeatureLayerByFeatureClassName(_map, layerInfo.Name);
                    if (pLayer != null)
                    {
                        //开始判断字段
                        string[] validateNames = layerInfo.ValidateKeys.Split('/');
                        for (int i = 0; i < validateNames.Length; i++)
                        {
                            int fieldIndex = pLayer.FeatureClass.Fields.FindField(validateNames[i]);
                            if (fieldIndex < 0) continue;
                        }
                        layerInfo.FeatureClass = pLayer.FeatureClass;
                        continue;
                    }
                }
            }
            foreach (IFunctionLayer functionLayer in _functionLayers)
            {
                IFeatureLayer layer = CommonHelper.GetFeatureLayerByFeatureClassName(_map, functionLayer.Name);
                if (layer != null)
                {
                    functionLayer.FeatureClass = layer.FeatureClass;
                }
            }
        }

        private void ViewEventsOnItemDeleted(object item)
        {
            LinkFeatureLayers();
        }

        private void ViewEventsOnItemAdded(object item)
        {
            LinkFeatureLayers();
        }

        public IFeatureLayer GetFeatureLayerByLayerName(IMap map, string layerName)
        {
            IEnumLayer enumLayer = map.Layers[null, true];
            enumLayer.Reset();
            ILayer layer;
            while ((layer = enumLayer.Next()) != null)
            {
                if (layer is IFeatureLayer && layer.Name == layerName)
                {
                    return layer as IFeatureLayer;
                }
            }
            return null;
        }

        public void SaveToXml(string fileName)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("PipelineConfig");
            XmlNode dbNode = doc.CreateElement("ConfigDatabase");
            dbNode.InnerText = _configDatabaseName;
            rootNode.AppendChild(dbNode);
            XmlNode configNode = doc.CreateElement("CommonConfigs");
            for (int i = 0; i < _commonConfigs.Count; i++)
            {
                XmlNode subNode = _commonConfigs[i].ToXml(doc);
                configNode.AppendChild(subNode);
            }
            rootNode.AppendChild(configNode);
            XmlNode layersNode = doc.CreateElement("PipelineLayers");
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layersNode.AppendChild(pipelineLayer.ToXml(doc));
            }
            rootNode.AppendChild(layersNode);
            XmlNode templatesNode = doc.CreateElement("LayerTemplates");
            foreach (IPipelineTemplate pipelineTemplate in _templates)
            {
                templatesNode.AppendChild(pipelineTemplate.ToXml(doc));
            }
            rootNode.AppendChild(templatesNode);
            XmlNode functionsNode = doc.CreateElement("FunctionLayers");
            foreach (IFunctionLayer functionLayer in _functionLayers)
            {
                functionsNode.AppendChild(functionLayer.ToXml(doc));
            }
            rootNode.AppendChild(functionsNode);
            doc.AppendChild(rootNode);
            doc.Save(fileName);
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("PipelineConfig");
            XmlNode layersNode = doc.CreateElement("PipelineLayers");
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layersNode.AppendChild(pipelineLayer.ToXml(doc));
            }
            rootNode.AppendChild(layersNode);
            XmlNode templatesNode = doc.CreateElement("LayerTemplates");
            foreach (IPipelineTemplate pipelineTemplate in _templates)
            {
                templatesNode.AppendChild(pipelineTemplate.ToXml(doc));
            }
            rootNode.AppendChild(templatesNode);
            XmlNode functionsNode = doc.CreateElement("FunctionLayers");
            foreach (IFunctionLayer functionLayer in _functionLayers)
            {
                functionsNode.AppendChild(functionLayer.ToXml(doc));
            }
            rootNode.AppendChild(functionsNode);
            doc.AppendChild(rootNode);
            doc.Save(_xmlFile);
        }


        //! 依据数据库内配置识别当前地图图层
        public void OrganizeMap(IMap pMap)
        {
            _layers.Clear();
            if (_dbLayers == null || _dbLayers.Count == 0)
                _dbLayers = ReadLayersFromDatabase();

            //先读取Map里面的图层，并按照Workspace进行组织，有点类似启动编辑时候的整理
            IArray arrayClass = ConfigHelper.OrganizeMapWorkspaceAndLayer(pMap);

            //图层已经按照Workspace组织，在一个Workspace里面，不允许有同名称的管线图层存在，而且在配置里面，不是依据图层，而是依据要素类，因为在实际中有可能一个要素类被渲染成好几个图层
            for (int i = 0; i < arrayClass.Count; i++)
            {
                PipeWorkspaceInfo workspaceInfo = arrayClass.Element[i] as PipeWorkspaceInfo;

                for (int j = 0; j < workspaceInfo.ClassArray.Count; j++)
                {
                    IFeatureClass pClass = workspaceInfo.ClassArray.Element[j] as IFeatureClass;
                    string className = ((IDataset) pClass).Name;
                    string shortName = ConfigHelper.GetClassShortName(className);
                    bool findExist = false;
                    foreach (IPipelineLayer existLayer in _layers)
                    {
                        // if(existLayer.Workspace.ConnectionProperties != workspaceInfo.Workspace.ConnectionProperties)
                        if (existLayer.Workspace.PathName != workspaceInfo.Workspace.PathName)
                        {
                            continue;
                        }
                        bool back = existLayer.OrganizeFeatureClass(pClass);
                        if (back)
                        {
                            findExist = true;
                            break;
                        }
                        IPipelineLayer newLayer = existLayer.NewOrganizeFeatureClass(pClass);

                        if (newLayer != null)
                        {
                            newLayer.Workspace = workspaceInfo.Workspace;
                            _layers.Add(newLayer);
                            findExist = true;
                            break;
                        }
                    }
                    if (findExist) continue;
                    foreach (IPipelineLayer dbLayer in _dbLayers)
                    {
                        IPipelineLayer newLayer = dbLayer.NewOrganizeFeatureClass(pClass);

                        if (newLayer != null)
                        {
                            IPipelineLayer pSaveLayer = newLayer.Clone(true);
                            newLayer.Workspace = workspaceInfo.Workspace;
                            _layers.Add(newLayer);
                            break;
                        }
                    }
                }
            }
            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                IPipelineLayer oneLayer = _layers[i];
                for (int j = oneLayer.Layers.Count - 1; j >= 0; j--)
                {
                    IBasicLayerInfo layerInfo = oneLayer.Layers[j];
                    if (layerInfo.FeatureClass == null) oneLayer.Layers.Remove(layerInfo);
                }
                if (oneLayer.Layers == null || oneLayer.Layers.Count == 0)
                {
                    _layers.Remove(oneLayer);
                }
            }
        }

        public List<IPipelineLayer> ReadLayersFromDatabase()
        {
            List<IPipelineLayer> layers = new List<IPipelineLayer>();
            ITable pCodeTable = _workspace.OpenTable("YT_PIPE_CODE");
            ITableSort tableSort = new TableSortClass();
            tableSort.Table = pCodeTable;
            tableSort.Fields = "Priority";
            tableSort.Sort(null);

            ICursor pCursor = tableSort.Rows;
            IRow pRow = pCursor.NextRow();
            int codeIdx = pCursor.FindField("PipeCode");
            int classIdx = pCursor.FindField("ClassCode");
            int nameIdx = pCursor.FindField("PipeName");
            int autoIdx = pCursor.FindField("AutoNames");
            int priIdx = pCursor.FindField("Priority");
            while (pRow != null)
            {
                IPipelineLayer oneLayer = new PipelineLayer()
                {
                    Code = pRow.Value[codeIdx].ToString(),
                    Name = pRow.Value[nameIdx].ToString(),
                    AutoNames = pRow.Value[autoIdx].ToString(),
                    Layers = new List<IBasicLayerInfo>(),
                    ClassCode = pRow.Value[classIdx].ToString()
                };
                layers.Add(oneLayer);
                pRow = pCursor.NextRow();
            }
            Marshal.ReleaseComObject(pCursor);
            Marshal.ReleaseComObject(tableSort);
            Marshal.ReleaseComObject(pCodeTable);

            List<IYTDomain> domains = new List<IYTDomain>();
            pCodeTable = _workspace.OpenTable("YT_PIPE_DOMAIN");
            pCursor = pCodeTable.Search(null, false);
            pRow = pCursor.NextRow();
            nameIdx = pCursor.FindField("DomainName");
            autoIdx = pCursor.FindField("DomainValues");

            while (pRow != null)
            {
                string domainName = pRow.Value[nameIdx].ToString();
                string domainValues = pRow.Value[autoIdx].ToString();
                IYTDomain onedomain = new YTDomain(domainName, domainValues);
                domains.Add(onedomain);
                pRow = pCursor.NextRow();
            }
            Marshal.ReleaseComObject(pCursor);
            Marshal.ReleaseComObject(pCodeTable);

            List<IPipelineTemplate> templates = new List<IPipelineTemplate>();
            //! 先读取模板
            pCodeTable = _workspace.OpenTable("YT_PIPE_FIELD");
            tableSort = new TableSortClass();
            tableSort.Table = pCodeTable;
            tableSort.Fields = "TemplateName";

            tableSort.Sort(null);
            pCursor = tableSort.Rows;
            string oldTemplate = "";
            int[] fieldIndexes = new int[10];
            pRow = pCursor.NextRow();
            fieldIndexes[0] = pRow.Fields.FindField("TemplateName");
            fieldIndexes[1] = pRow.Fields.FindField("TypeName");
            fieldIndexes[2] = pRow.Fields.FindField("FieldName");
            fieldIndexes[3] = pRow.Fields.FindField("FieldAliasName");
            fieldIndexes[4] = pRow.Fields.FindField("FieldType");
            fieldIndexes[5] = pRow.Fields.FindField("FieldLength");
            fieldIndexes[6] = pRow.Fields.FindField("FieldPrecision");
            fieldIndexes[7] = pRow.Fields.FindField("AllowNull");
            fieldIndexes[8] = pRow.Fields.FindField("AutoValues");
            fieldIndexes[9] = pRow.Fields.FindField("IsKey");
            //  fieldIndexes[10] = pRow.Fields.FindField("Domains");


            IPipelineTemplate oneTemplate = null;
            while (pRow != null)
            {
                string templateName = pRow.Value[fieldIndexes[0]].ToString();
                if (!templateName.Equals(oldTemplate))
                {
                    if (oneTemplate != null)
                    {
                        templates.Add(oneTemplate);
                    }
                    oneTemplate = new PipelineTemplate() {Name = templateName, Fields = new List<IYTField>()};
                    oldTemplate = templateName;
                }
                IYTField field = new YTField()
                {
                    TypeName = pRow.Value[fieldIndexes[1]].ToString(),
                    Name = pRow.Value[fieldIndexes[2]].ToString(),
                    AliasName = pRow.Value[fieldIndexes[3]].ToString(),
                    Length = Convert.ToInt32(pRow.Value[fieldIndexes[5]].ToString()),
                    Precision = Convert.ToInt32(pRow.Value[fieldIndexes[6]].ToString()),
                    AllowNull = Convert.ToInt32(pRow.Value[fieldIndexes[7]].ToString()) == -1 ? true : false,
                    AutoNames = pRow.Value[fieldIndexes[8]].ToString(),
                    FieldType = FieldHelper.ConvertFromString(pRow.Value[fieldIndexes[4]].ToString())
                };
                oneTemplate.Fields.Add(field);
                pRow = pCursor.NextRow();
            }
            if (oneTemplate != null)
            {
                templates.Add(oneTemplate);
            }
            Marshal.ReleaseComObject(pCursor);
            Marshal.ReleaseComObject(tableSort);
            Marshal.ReleaseComObject(pCodeTable);

            List<IBasicLayerInfo> basicInfos = new List<IBasicLayerInfo>();

            pCodeTable = _workspace.OpenTable("YT_PIPE_LAYER");
            tableSort = new TableSortClass();
            tableSort.Table = pCodeTable;
            tableSort.Fields = "Priority,LayerName";
            tableSort.Sort(null);
            pCursor = tableSort.Rows;
            pRow = pCursor.NextRow();
            fieldIndexes = new int[8];

            fieldIndexes[0] = pRow.Fields.FindField("PipeCode");
            fieldIndexes[1] = pRow.Fields.FindField("BasicName");
            fieldIndexes[2] = pRow.Fields.FindField("LayerName");
            fieldIndexes[3] = pRow.Fields.FindField("AutoNames");
            fieldIndexes[4] = pRow.Fields.FindField("Priority");
            fieldIndexes[5] = pRow.Fields.FindField("DataType");
            fieldIndexes[6] = pRow.Fields.FindField("Template");
            fieldIndexes[7] = pRow.Fields.FindField("Domains");
            while (pRow != null)
            {
                string pipeCode = pRow.Value[fieldIndexes[0]].ToString();
                IPipelineLayer oneLayer = layers.Find(c => c.Code == pipeCode);
                if (oneLayer == null)
                {
                    pRow = pCursor.NextRow();
                    continue;
                }
                enumPipelineDataType dataType =
                    Yutai.Pipeline.Config.Helpers.EnumHelper.ConvertDataTypeFromString(
                        pRow.Value[fieldIndexes[5]].ToString().Trim());
                IBasicLayerInfo basicLayer = new BasicLayerInfo()
                {
                    Name = pRow.Value[fieldIndexes[1]].ToString(),
                    AliasName = pRow.Value[fieldIndexes[2]].ToString(),
                    AutoNames = pRow.Value[fieldIndexes[3]].ToString(),
                    DataType = dataType,
                    TemplateName = pRow.Value[fieldIndexes[6]].ToString(),
                    Fields = new List<IYTField>()
                };
                if (pRow.Value[fieldIndexes[6]] != null)
                {
                    IPipelineTemplate template = templates.Find(c => c.Name == basicLayer.TemplateName);
                    if (template != null)
                    {
                        foreach (IYTField field in template.Fields)
                        {
                            basicLayer.Fields.Add(new YTField(field));
                        }
                    }
                }

                string domainStr = pRow.Value[fieldIndexes[7]] == DBNull.Value
                    ? string.Empty
                    : pRow.Value[fieldIndexes[7]].ToString();
                if (!string.IsNullOrEmpty(domainStr))
                {
                    //获得Domainzhi值
                    string[] domainPairs = domainStr.Split('/');
                    for (int j = 0; j < domainPairs.Length; j++)
                    {
                        string[] domainPair = domainPairs[j].Split(':');
                        string fieldName = domainPair[0];
                        string domainName = domainPair[1];
                        IYTDomain findDomain = domains.FirstOrDefault(c => c.DomainName == domainName);
                        if (findDomain != null)
                        {
                            IYTField pField = basicLayer.Fields.FirstOrDefault(c => c.TypeName == fieldName);
                            if (pField != null)
                                pField.Domain = new YTDomain(findDomain.DomainName, findDomain.DomainValues);
                        }
                    }
                }

                oneLayer.Layers.Add(basicLayer);
                pRow = pCursor.NextRow();
            }
            Marshal.ReleaseComObject(pCursor);
            Marshal.ReleaseComObject(tableSort);
            Marshal.ReleaseComObject(pCodeTable);
            return layers;
        }

        public string ProjectFile
        {
            get { return _projectFile; }
            set { _projectFile = value; }
        }
    }
}
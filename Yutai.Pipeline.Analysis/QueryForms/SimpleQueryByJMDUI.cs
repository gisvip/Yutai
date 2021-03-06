﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleQueryByJMDUI : Form
    {
        public IGeometry m_Geo;

        public IMapControl3 MapControl;

        public IAppContext m_context;

        public IPipelineConfig pPipeCfg;

        private PipelineAnalysisPlugin _plugin;

        public PipelineAnalysisPlugin Plugin
        {
            set { _plugin = value; }
        }

        public SimpleQueryByJMDUI()
        {
            this.InitializeComponent();
        }

        private void SimpleQueryByJMDUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.Layer[i];
                if (this.GetLayer(ipLay))
                {
                    break;
                }
            }
            if (this.SelectLayer == null)
            {
                base.Close();
            }
            else
            {
                this.FillValueBox();
            }
        }

        private bool GetLayer(ILayer ipLay)
        {
            bool result;
            if (ipLay is IFeatureLayer)
            {
                IFeatureLayer featureLayer = ipLay as IFeatureLayer;
                if (pPipeCfg.IsFunctionLayer(featureLayer.FeatureClass.AliasName, enumFunctionLayerType.Jmd))
                {
                    this.SelectLayer = (ipLay as IFeatureLayer);
                    result = true;
                    return result;
                }
            }
            else if (ipLay is IGroupLayer)
            {
                result = this.GetGroupLayer((IGroupLayer) ipLay);
                return result;
            }
            result = false;
            return result;
        }

        private bool GetGroupLayer(ILayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer) iGLayer;
            bool result;
            if (compositeLayer == null)
            {
                result = false;
            }
            else
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer ipLay = compositeLayer.Layer[i];
                    if (this.GetLayer(ipLay))
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        private void FillValueBox()
        {
            if (this.SelectLayer == null)
            {
                MessageBox.Show(@"此图层不存在");
            }
            else
            {
                IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                IFunctionLayer functionLayer = pPipeCfg.GetFunctionLayer(featureClass);
                IQueryFilter queryFilter = new QueryFilter();
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();
                this.comboBox1.Items.Clear();
                string text = functionLayer.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.JMDMC);
                int num = featureClass.Fields.FindField(text);
                if (num == -1)
                {
                    this.button1.Enabled = false;
                    MessageBox.Show(@"没有找到字段！");
                }
                else
                {
                    this.button1.Enabled = true;
                    this.comboBox1.Items.Clear();
                    while (feature != null)
                    {
                        object obj = feature.Value[num];
                        string text2;
                        if (obj == null || Convert.IsDBNull(obj))
                        {
                            text2 = "";
                        }
                        else
                        {
                            text2 = obj.ToString();
                        }
                        if (!this.comboBox1.Items.Contains(text2) && text2 != "")
                        {
                            this.comboBox1.Items.Add(text2);
                        }
                        feature = featureCursor.NextFeature();
                    }
                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.SelectLayer == null)
            {
                MessageBox.Show(@"项目地界层不存在!");
            }
            else
            {
                IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                ISpatialFilter spatialFilter = new SpatialFilter();
                string text = this.comboBox1.Text;
                string text2;
                if (!this.checkBox1.Checked)
                {
                    text2 = "名称 = ";
                    text2 += " '";
                    text2 += text;
                    text2 += "'";
                }
                else
                {
                    text2 = "名称 LIKE ";
                    IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
                    if (dataset.Workspace.Type == (esriWorkspaceType) 2)
                    {
                        text2 += " '%";
                        text2 += text;
                        text2 += "%'";
                    }
                    else
                    {
                        text2 += " '*";
                        text2 += text;
                        text2 += "*'";
                    }
                }
                if (this.comboBox1.Text == "")
                {
                    text2 = "名称 IS NULL ";
                }
                spatialFilter.WhereClause = text2;
                IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
                //修改为插件事件，因为结果显示窗体为插件拥有。
                _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection) this.SelectLayer, spatialFilter));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
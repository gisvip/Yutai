﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class MapTemplateTypePage : UserControl
    {
        private IContainer components = null;


        public MapTemplateTypePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoStandard.Checked)
            {
                this.MapTemplate.MapFramingType = MapFramingType.StandardFraming;
            }
            else
            {
                this.MapTemplate.MapFramingType = MapFramingType.AnyFraming;
            }
        }

        private void MapTemplateTypePage_Load(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoStandard_CheckedChanged(object sender, EventArgs e)
        {
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }
    }
}
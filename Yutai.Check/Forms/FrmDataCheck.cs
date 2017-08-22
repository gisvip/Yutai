using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Check.Classes;
using Yutai.Check.Enums;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Forms
{
    public partial class FrmDataCheck : Form
    {
        private IDataCheck _dataCheck;
        public FrmDataCheck(IDataCheck dataCheck)
        {
            InitializeComponent();
            _dataCheck = dataCheck;
            this.checkedListBoxPipelineLayers.DataSource = dataCheck.PipelineLayers;
            this.checkedListBoxPipelineLayers.DisplayMember = "Name";
            this.checkedListBoxPipelineLayers.ValueMember = "Code";
        }

        public void InitCheckItem(CheckPlugin plugin)
        {
            this.checkBoxFieldFull.Enabled = true;
            this.checkBoxFieldRepeat.Enabled = true;
            this.checkBoxHylink.Enabled = true;

            this.checkBoxSinglePoint.Enabled = true;
            this.checkBoxSingleLine.Enabled = true;
            this.checkBoxPointRepeat.Enabled = true;
            this.checkBoxLineRepeat.Enabled = true;

            if (plugin.DataCheckConfig != null)
            {
                this.checkBoxCoord.Enabled = true;
                this.checkBoxElevation.Enabled = true;
            }

            this.checkBoxRelation.Enabled = true;
            this.checkBoxFlow.Enabled = true;
        }

        public List<EnumCheckItem> GetCheckItems()
        {
            List<EnumCheckItem> items = new List<EnumCheckItem>();

            if (checkBoxFieldFull.Checked)
                items.Add(EnumCheckItem.P_FieldFull);
            if (checkBoxFieldRepeat.Checked)
                items.Add(EnumCheckItem.P_FieldRepeat);
            if (checkBoxHylink.Checked)
                items.Add(EnumCheckItem.P_Hylink);
            if (checkBoxSinglePoint.Checked)
                items.Add(EnumCheckItem.G_SinglePoint);
            if (checkBoxSingleLine.Checked)
                items.Add(EnumCheckItem.G_SingleLine);
            if (checkBoxPointRepeat.Checked)
                items.Add(EnumCheckItem.G_PointRepeat);
            if (checkBoxLineRepeat.Checked)
                items.Add(EnumCheckItem.G_LineRepeat);
            if (checkBoxCoord.Checked)
                items.Add(EnumCheckItem.G_Coord);
            if (checkBoxRelation.Checked)
                items.Add(EnumCheckItem.G_Relation);
            if (checkBoxElevation.Checked)
                items.Add(EnumCheckItem.G_Elevation);
            if (checkBoxFlow.Checked)
                items.Add(EnumCheckItem.G_Flow);

            return items;
        }

        public List<string> GetCheckPipeline()
        {
            List<string> list = new List<string>();
            foreach (object checkedItem in this.checkedListBoxPipelineLayers.CheckedItems)
            {
                IPipelineLayer pipelineLayer = checkedItem as IPipelineLayer;
                if (pipelineLayer == null)
                    continue;
                list.Add(pipelineLayer.Code);
            }
            return list;
        }
    }
}

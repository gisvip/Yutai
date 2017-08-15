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
        public FrmDataCheck(IDataCheck dataCheck)
        {
            InitializeComponent();
            this.checkedListBoxPipelineLayers.DataSource = dataCheck.PipelineLayers;
            this.checkedListBoxPipelineLayers.DisplayMember = "Name";
            this.checkedListBoxPipelineLayers.ValueMember = "Code";
        }
        
        public List<EnumCheckItem> GetCheckItems()
        {
            List<EnumCheckItem> items = new List<EnumCheckItem>();

            if (checkBoxFieldFull.Checked)
                items.Add(EnumCheckItem.P_FieldFull);

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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmAddLayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnCancel;

        private Button btnOK;

        private Label label1;

        private DataGridView dataGridView1;

        private DataGridViewTextBoxColumn LayerName;

        private DataGridViewComboBoxColumn Privilege;

        private Button btnAll;

        private Button btnEditGrant;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem 无ToolStripMenuItem;

        private ToolStripMenuItem 浏览ToolStripMenuItem;

        private ToolStripMenuItem 编辑ToolStripMenuItem;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAddLayer));
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label1 = new Label();
            this.dataGridView1 = new DataGridView();
            this.LayerName = new DataGridViewTextBoxColumn();
            this.Privilege = new DataGridViewComboBoxColumn();
            this.btnAll = new Button();
            this.btnEditGrant = new Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.无ToolStripMenuItem = new ToolStripMenuItem();
            this.浏览ToolStripMenuItem = new ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize)this.dataGridView1).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(209, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(128, 239);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "图层列表";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] layerName = new DataGridViewColumn[] { this.LayerName, this.Privilege };
            this.dataGridView1.Columns.AddRange(layerName);
            this.dataGridView1.Location = new Point(10, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(240, 195);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);
            this.LayerName.HeaderText = "图层名";
            this.LayerName.Name = "LayerName";
            this.LayerName.ReadOnly = true;
            this.Privilege.HeaderText = "权限";
            this.Privilege.Items.AddRange(new object[] { "无", "可见", "可编辑" });
            this.Privilege.Name = "Privilege";
            this.btnAll.Location = new Point(256, 54);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(67, 23);
            this.btnAll.TabIndex = 43;
            this.btnAll.Text = "修改所有";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new EventHandler(this.btnAll_Click);
            this.btnEditGrant.Location = new Point(256, 25);
            this.btnEditGrant.Name = "btnEditGrant";
            this.btnEditGrant.Size = new System.Drawing.Size(67, 23);
            this.btnEditGrant.TabIndex = 42;
            this.btnEditGrant.Text = "修改选择";
            this.btnEditGrant.UseVisualStyleBackColor = true;
            this.btnEditGrant.Click += new EventHandler(this.btnEditGrant_Click);
            ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.无ToolStripMenuItem, this.浏览ToolStripMenuItem, this.编辑ToolStripMenuItem };
            this.contextMenuStrip1.Items.AddRange(toolStripItemArray);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            this.无ToolStripMenuItem.Name = "无ToolStripMenuItem";
            this.无ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.无ToolStripMenuItem.Text = "无";
            this.无ToolStripMenuItem.Click += new EventHandler(this.无ToolStripMenuItem_Click);
            this.浏览ToolStripMenuItem.Name = "浏览ToolStripMenuItem";
            this.浏览ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.浏览ToolStripMenuItem.Text = "浏览";
            this.浏览ToolStripMenuItem.Click += new EventHandler(this.浏览ToolStripMenuItem_Click);
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new EventHandler(this.编辑ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(335, 273);
            base.Controls.Add(this.btnAll);
            base.Controls.Add(this.btnEditGrant);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddLayer";
            this.Text = "图层";
            base.Load += new EventHandler(this.frmAddLayer_Load);
            ((ISupportInitialize)this.dataGridView1).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
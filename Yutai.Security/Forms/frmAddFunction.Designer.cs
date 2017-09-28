using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmAddFunction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label label1;

        private CheckedListBox clbFunctions;

        private Button btnSelectAll;

        private Button btnInvertSelect;

        private Button btnUnSelect;

        private Button btnCancel;

        private Button btnOK;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAddFunction));
            this.label1 = new Label();
            this.clbFunctions = new CheckedListBox();
            this.btnSelectAll = new Button();
            this.btnInvertSelect = new Button();
            this.btnUnSelect = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "功能列表";
            this.clbFunctions.FormattingEnabled = true;
            this.clbFunctions.Location = new Point(5, 33);
            this.clbFunctions.Name = "clbFunctions";
            this.clbFunctions.Size = new System.Drawing.Size(193, 196);
            this.clbFunctions.TabIndex = 1;
            this.clbFunctions.SelectedIndexChanged += new EventHandler(this.clbFunctions_SelectedIndexChanged);
            this.clbFunctions.ItemCheck += new ItemCheckEventHandler(this.clbFunctions_ItemCheck);
            this.btnSelectAll.Location = new Point(204, 33);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnInvertSelect.Location = new Point(204, 62);
            this.btnInvertSelect.Name = "btnInvertSelect";
            this.btnInvertSelect.Size = new System.Drawing.Size(75, 23);
            this.btnInvertSelect.TabIndex = 3;
            this.btnInvertSelect.Text = "反选";
            this.btnInvertSelect.UseVisualStyleBackColor = true;
            this.btnInvertSelect.Click += new EventHandler(this.btnInvertSelect_Click);
            this.btnUnSelect.Location = new Point(205, 91);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUnSelect.TabIndex = 4;
            this.btnUnSelect.Text = "取消选择";
            this.btnUnSelect.UseVisualStyleBackColor = true;
            this.btnUnSelect.Click += new EventHandler(this.btnUnSelect_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(204, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(123, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(292, 273);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnUnSelect);
            base.Controls.Add(this.btnInvertSelect);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.clbFunctions);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddFunction";
            this.Text = "功能";
            base.Load += new EventHandler(this.frmAddFunction_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
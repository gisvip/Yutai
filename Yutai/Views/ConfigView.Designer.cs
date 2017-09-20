using Yutai.Controls;
using Yutai.UI.Controls;

namespace Yutai.Views
{
    partial class ConfigView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.configPageControl1 = new Yutai.UI.Controls.ConfigPageControl();
            this.treeViewAdv1 = new Yutai.Controls.ConfigTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      
            this.btnCancel.BackColor = System.Drawing.Color.White;
          
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          
            this.btnCancel.Location = new System.Drawing.Point(722, 528);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
         
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         
            this.btnOk.BackColor = System.Drawing.Color.White;
         
            this.btnOk.Location = new System.Drawing.Point(616, 528);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 28);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确认";
         
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          
            this.btnSave.BackColor = System.Drawing.Color.White;
         
            this.btnSave.Location = new System.Drawing.Point(502, 528);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "应用";
          
            // 
            // configPageControl1
            // 
            this.configPageControl1.ConfigPage = null;
            this.configPageControl1.Description = "详细说明文本";
            this.configPageControl1.Icon = null;
            this.configPageControl1.Location = new System.Drawing.Point(213, 13);
            this.configPageControl1.Name = "configPageControl1";
            this.configPageControl1.Size = new System.Drawing.Size(608, 510);
            this.configPageControl1.TabIndex = 12;
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.Location = new System.Drawing.Point(5, 12);
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.Size = new System.Drawing.Size(208, 510);
            this.treeViewAdv1.TabIndex = 13;
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(826, 563);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.configPageControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "ConfigView";
            this.Text = "配置";
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnSave;
        private ConfigPageControl configPageControl1;
        private Yutai.Controls.ConfigTreeView treeViewAdv1;
    }
}
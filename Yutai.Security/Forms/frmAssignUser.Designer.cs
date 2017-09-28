using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmAssignUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ListView lvStaffRole;

        private ColumnHeader columnHeader3;

        private ColumnHeader columnHeader4;

        private Label label2;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAssignUser));
            this.lvStaffRole = new ListView();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.label2 = new Label();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader3, this.columnHeader4 };
            this.lvStaffRole.Columns.AddRange(columnHeaderArray);
            this.lvStaffRole.FullRowSelect = true;
            this.lvStaffRole.Location = new Point(12, 30);
            this.lvStaffRole.Name = "lvStaffRole";
            this.lvStaffRole.Size = new System.Drawing.Size(205, 128);
            this.lvStaffRole.TabIndex = 7;
            this.lvStaffRole.UseCompatibleStateImageBehavior = false;
            this.lvStaffRole.View = View.Details;
            this.lvStaffRole.SelectedIndexChanged += new EventHandler(this.lvStaffRole_SelectedIndexChanged);
            this.columnHeader3.Text = "用户名";
            this.columnHeader3.Width = 81;
            this.columnHeader4.Text = "登录名";
            this.columnHeader4.Width = 111;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "用户列表";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(139, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(58, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(236, 195);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lvStaffRole);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
           // base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAssignUser";
            this.Text = "分配用户";
            base.Load += new EventHandler(this.frmAssignUser_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
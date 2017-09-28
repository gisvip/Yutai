using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmAddStaff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        private Label label1;

        private Label label2;

        private Label label3;

        private Label label4;

        private TextBox txtUserName;

        private TextBox txtChineseName;

        private TextBox txtPassword;

        private TextBox txtValidePassword;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAddStaff));
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtUserName = new TextBox();
            this.txtChineseName = new TextBox();
            this.txtPassword = new TextBox();
            this.txtValidePassword = new TextBox();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "登录名:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "中文名:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "输入密码";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "确认密码";
            this.txtUserName.Location = new Point(65, 9);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(184, 21);
            this.txtUserName.TabIndex = 4;
            this.txtChineseName.Location = new Point(65, 40);
            this.txtChineseName.Name = "txtChineseName";
            this.txtChineseName.Size = new System.Drawing.Size(184, 21);
            this.txtChineseName.TabIndex = 5;
            this.txtPassword.Location = new Point(65, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(184, 21);
            this.txtPassword.TabIndex = 6;
            this.txtValidePassword.Location = new Point(65, 102);
            this.txtValidePassword.Name = "txtValidePassword";
            this.txtValidePassword.PasswordChar = '*';
            this.txtValidePassword.Size = new System.Drawing.Size(184, 21);
            this.txtValidePassword.TabIndex = 7;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(177, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(96, 142);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(292, 185);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtValidePassword);
            base.Controls.Add(this.txtPassword);
            base.Controls.Add(this.txtChineseName);
            base.Controls.Add(this.txtUserName);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddStaff";
            this.Text = "新建用户";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
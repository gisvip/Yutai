using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmEditStaff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        private Button btnCancel;

        private Button btnOK;

        private TextBox txtValidePassword;

        private TextBox txtPassword;

        private TextBox txtChineseName;

        private TextBox txtUserName;

        private Label label4;

        private Label label3;

        private Label label2;

        private Label label1;

        private TextBox txtOldPSW;

        private Label label5;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmEditStaff));
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.txtValidePassword = new TextBox();
            this.txtPassword = new TextBox();
            this.txtChineseName = new TextBox();
            this.txtUserName = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.txtOldPSW = new TextBox();
            this.label5 = new Label();
            base.SuspendLayout();
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(177, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(96, 166);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txtValidePassword.Location = new Point(74, 126);
            this.txtValidePassword.Name = "txtValidePassword";
            this.txtValidePassword.PasswordChar = '*';
            this.txtValidePassword.Size = new System.Drawing.Size(184, 21);
            this.txtValidePassword.TabIndex = 17;
            this.txtPassword.Location = new Point(74, 95);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(184, 21);
            this.txtPassword.TabIndex = 16;
            this.txtChineseName.Location = new Point(74, 40);
            this.txtChineseName.Name = "txtChineseName";
            this.txtChineseName.Size = new System.Drawing.Size(184, 21);
            this.txtChineseName.TabIndex = 15;
            this.txtUserName.Location = new Point(74, 9);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(184, 21);
            this.txtUserName.TabIndex = 14;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "确认新密码";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "输入新密码";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "中文名:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "登录名:";
            this.txtOldPSW.Location = new Point(74, 65);
            this.txtOldPSW.Name = "txtOldPSW";
            this.txtOldPSW.PasswordChar = '*';
            this.txtOldPSW.Size = new System.Drawing.Size(184, 21);
            this.txtOldPSW.TabIndex = 21;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "旧密码";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(292, 205);
            base.Controls.Add(this.txtOldPSW);
            base.Controls.Add(this.label5);
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
           // base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEditStaff";
            this.Text = "用户信息";
            base.Load += new EventHandler(this.frmEditStaff_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
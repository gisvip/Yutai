﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmCartoConfig
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtPassword = new TextBox();
            this.label11 = new Label();
            this.label12 = new Label();
            this.txtUser = new TextBox();
            this.label13 = new Label();
            this.txtInstance = new TextBox();
            this.label14 = new Label();
            this.txtDatabase = new TextBox();
            this.label15 = new Label();
            this.txtServer = new TextBox();
            this.button2 = new Button();
            this.btnOK = new Button();
            this.group = new GroupBox();
            this.btnSetFolder = new Button();
            this.txtFloder = new TextBox();
            this.label4 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label3 = new Label();
            this.txtMapNameField = new TextBox();
            this.label2 = new Label();
            this.txtMapNoFieldName = new TextBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.group.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtInstance);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(415, 120);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SDE数据库配置";
            this.txtPassword.Location = new Point(273, 50);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(123, 21);
            this.txtPassword.TabIndex = 9;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(209, 59);
            this.label11.Name = "label11";
            this.label11.Size = new Size(29, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "密码";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(208, 27);
            this.label12.Name = "label12";
            this.label12.Size = new Size(41, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "用户名";
            this.txtUser.Location = new Point(273, 18);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(123, 21);
            this.txtUser.TabIndex = 7;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(15, 89);
            this.label13.Name = "label13";
            this.label13.Size = new Size(29, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "实例";
            this.txtInstance.Location = new Point(79, 80);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new Size(123, 21);
            this.txtInstance.TabIndex = 5;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(14, 62);
            this.label14.Name = "label14";
            this.label14.Size = new Size(41, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "数据库";
            this.txtDatabase.Location = new Point(79, 53);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new Size(123, 21);
            this.txtDatabase.TabIndex = 3;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(15, 27);
            this.label15.Name = "label15";
            this.label15.Size = new Size(41, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "服务器";
            this.txtServer.Location = new Point(79, 24);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(123, 21);
            this.txtServer.TabIndex = 1;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(343, 377);
            this.button2.Name = "button2";
            this.button2.Size = new Size(61, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(276, 377);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(61, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.group.Controls.Add(this.btnSetFolder);
            this.group.Controls.Add(this.txtFloder);
            this.group.Controls.Add(this.label4);
            this.group.Controls.Add(this.listView1);
            this.group.Controls.Add(this.label3);
            this.group.Controls.Add(this.txtMapNameField);
            this.group.Controls.Add(this.label2);
            this.group.Controls.Add(this.txtMapNoFieldName);
            this.group.Controls.Add(this.label1);
            this.group.Location = new Point(12, 138);
            this.group.Name = "group";
            this.group.Size = new Size(415, 233);
            this.group.TabIndex = 23;
            this.group.TabStop = false;
            this.group.Text = "图幅信息表配置";
            this.btnSetFolder.Location = new Point(360, 188);
            this.btnSetFolder.Name = "btnSetFolder";
            this.btnSetFolder.Size = new Size(32, 23);
            this.btnSetFolder.TabIndex = 22;
            this.btnSetFolder.Text = "...";
            this.btnSetFolder.UseVisualStyleBackColor = true;
            this.btnSetFolder.Click += new EventHandler(this.btnSetFolder_Click);
            this.txtFloder.Location = new Point(101, 190);
            this.txtFloder.Name = "txtFloder";
            this.txtFloder.ReadOnly = true;
            this.txtFloder.Size = new Size(242, 21);
            this.txtFloder.TabIndex = 10;
            this.txtFloder.TextChanged += new EventHandler(this.txtFloder_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 193);
            this.label4.Name = "label4";
            this.label4.Size = new Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "图幅模板文件夹";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(16, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(383, 112);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "比例尺";
            this.columnHeader_0.Width = 83;
            this.columnHeader_1.Text = "图幅索引表名";
            this.columnHeader_1.Width = 150;
            this.columnHeader_2.Text = "模板文件名";
            this.columnHeader_2.Width = 88;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(209, 158);
            this.label3.Name = "label3";
            this.label3.Size = new Size(71, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "图 名 字 段";
            this.txtMapNameField.Location = new Point(286, 155);
            this.txtMapNameField.Name = "txtMapNameField";
            this.txtMapNameField.Size = new Size(113, 21);
            this.txtMapNameField.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 158);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "图幅编号字段";
            this.txtMapNoFieldName.Location = new Point(89, 155);
            this.txtMapNoFieldName.Name = "txtMapNoFieldName";
            this.txtMapNoFieldName.Size = new Size(113, 21);
            this.txtMapNoFieldName.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图幅信息表名";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(444, 412);
            base.Controls.Add(this.group);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCartoConfig";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "制图配置";
            base.Load += new EventHandler(this.frmCartoConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private Button btnOK;
        private Button btnSetFolder;
        private Button button2;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private GroupBox group;
        private GroupBox groupBox1;
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private TextBox textBox_0;
        private TextBox txtDatabase;
        private TextBox txtFloder;
        private TextBox txtInstance;
        private TextBox txtMapNameField;
        private TextBox txtMapNoFieldName;
        private TextBox txtPassword;
        private TextBox txtServer;
        private TextBox txtUser;
    }
}
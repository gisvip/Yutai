using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmUserManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ListView lvRoles;

        private ColumnHeader columnHeader1;

        private ColumnHeader columnHeader2;

        private ListView lvLayers;

        private ColumnHeader columnHeader5;

        private ColumnHeader columnHeader6;

        private Label label4;

        private Label label3;

        private Button btnDeleteUser;

        private Button btnAddUser;

        private ListView lvStaff;

        private ColumnHeader columnHeader3;

        private ColumnHeader columnHeader4;

        private Label label2;

        private Button btnDeleteRole;

        private Label label1;

        private Button btnNewRole;

        private Button button1;

        private ListView lvFunctions;

        private Button btnDeleteLayers;

        private Button btnAddLayers;

        private Button btnDeleteMenu;

        private Button btnAddMenu;

        private ColumnHeader columnHeader7;

        private TabControl tabControl1;

        private TabPage tabPage1;

        private TabPage tabPage2;

        private TabPage tabPage3;

        private Button btnEdit;

        private Button btnEditGrant;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem 无ToolStripMenuItem;

        private ToolStripMenuItem 浏览ToolStripMenuItem;

        private ToolStripMenuItem 编辑ToolStripMenuItem;

        private Button btnAll;

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
            ListViewGroup listViewGroup = new ListViewGroup("用户自身具有的图层权限", HorizontalAlignment.Left);
            ListViewGroup listViewGroup1 = new ListViewGroup("来自于角色的图层权限", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("用户自身具有的功能操作", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("源于角色的功能操作", HorizontalAlignment.Left);
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmUserManage));
            this.lvRoles = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.lvLayers = new ListView();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.label4 = new Label();
            this.label3 = new Label();
            this.btnDeleteUser = new Button();
            this.btnAddUser = new Button();
            this.lvStaff = new ListView();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.label2 = new Label();
            this.btnDeleteRole = new Button();
            this.label1 = new Label();
            this.btnNewRole = new Button();
            this.button1 = new Button();
            this.lvFunctions = new ListView();
            this.columnHeader7 = new ColumnHeader();
            this.btnDeleteLayers = new Button();
            this.btnAddLayers = new Button();
            this.btnDeleteMenu = new Button();
            this.btnAddMenu = new Button();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabPage3 = new TabPage();
            this.btnAll = new Button();
            this.btnEditGrant = new Button();
            this.btnEdit = new Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.无ToolStripMenuItem = new ToolStripMenuItem();
            this.浏览ToolStripMenuItem = new ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.lvRoles.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.lvRoles.FullRowSelect = true;
            this.lvRoles.HideSelection = false;
            this.lvRoles.Location = new Point(8, 22);
            this.lvRoles.Name = "lvRoles";
            this.lvRoles.Size = new System.Drawing.Size(322, 186);
            this.lvRoles.TabIndex = 32;
            this.lvRoles.UseCompatibleStateImageBehavior = false;
            this.lvRoles.View = View.Details;
            this.lvRoles.SelectedIndexChanged += new EventHandler(this.lvRoles_SelectedIndexChanged);
            this.columnHeader1.Text = "角色名";
            this.columnHeader1.Width = 146;
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 133;
            this.lvLayers.Columns.AddRange(new ColumnHeader[] { this.columnHeader5, this.columnHeader6 });
            this.lvLayers.FullRowSelect = true;
            listViewGroup.Header = "用户自身具有的图层权限";
            listViewGroup.Name = "listViewGroup1";
            listViewGroup1.Header = "来自于角色的图层权限";
            listViewGroup1.Name = "listViewGroup2";
            ListViewGroup[] listViewGroupArray = new ListViewGroup[] { listViewGroup, listViewGroup1 };
            this.lvLayers.Groups.AddRange(listViewGroupArray);
            this.lvLayers.HideSelection = false;
            this.lvLayers.Location = new Point(7, 26);
            this.lvLayers.Name = "lvLayers";
            this.lvLayers.Size = new System.Drawing.Size(316, 169);
            this.lvLayers.TabIndex = 29;
            this.lvLayers.UseCompatibleStateImageBehavior = false;
            this.lvLayers.View = View.Details;
            this.lvLayers.SelectedIndexChanged += new EventHandler(this.lvLayers_SelectedIndexChanged);
            this.columnHeader5.Text = "图层名";
            this.columnHeader5.Width = 177;
            this.columnHeader6.Text = "权限";
            this.columnHeader6.Width = 120;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(5, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "该用户的图层控制权限设置";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "该用户的具备的功能列表";
            this.btnDeleteUser.Enabled = false;
            this.btnDeleteUser.Location = new Point(363, 57);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteUser.TabIndex = 24;
            this.btnDeleteUser.Text = "删除";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new EventHandler(this.btnDeleteUser_Click);
            this.btnAddUser.Location = new Point(363, 28);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 23;
            this.btnAddUser.Text = "新建用户";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);
            this.lvStaff.Columns.AddRange(new ColumnHeader[] { this.columnHeader3, this.columnHeader4 });
            this.lvStaff.FullRowSelect = true;
            this.lvStaff.HideSelection = false;
            this.lvStaff.Location = new Point(3, 28);
            this.lvStaff.MultiSelect = false;
            this.lvStaff.Name = "lvStaff";
            this.lvStaff.Size = new System.Drawing.Size(354, 128);
            this.lvStaff.TabIndex = 22;
            this.lvStaff.UseCompatibleStateImageBehavior = false;
            this.lvStaff.View = View.Details;
            this.lvStaff.SelectedIndexChanged += new EventHandler(this.lvStaff_SelectedIndexChanged);
            this.columnHeader3.Text = "用户名";
            this.columnHeader3.Width = 139;
            this.columnHeader4.Text = "登录名";
            this.columnHeader4.Width = 151;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "用户列表";
            this.btnDeleteRole.Location = new Point(336, 51);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRole.TabIndex = 20;
            this.btnDeleteRole.Text = "删除";
            this.btnDeleteRole.UseVisualStyleBackColor = true;
            this.btnDeleteRole.Click += new EventHandler(this.btnDeleteRole_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "隶属角色列表";
            this.btnNewRole.Location = new Point(336, 22);
            this.btnNewRole.Name = "btnNewRole";
            this.btnNewRole.Size = new System.Drawing.Size(75, 23);
            this.btnNewRole.TabIndex = 18;
            this.btnNewRole.Text = "添加角色";
            this.btnNewRole.UseVisualStyleBackColor = true;
            this.btnNewRole.Click += new EventHandler(this.btnNewRole_Click);
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new Point(363, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.lvFunctions.Columns.AddRange(new ColumnHeader[] { this.columnHeader7 });
            listViewGroup2.Header = "用户自身具有的功能操作";
            listViewGroup2.Name = "listViewGroup1";
            listViewGroup3.Header = "源于角色的功能操作";
            listViewGroup3.Name = "listViewGroup2";
            this.lvFunctions.Groups.AddRange(new ListViewGroup[] { listViewGroup2, listViewGroup3 });
            this.lvFunctions.Location = new Point(6, 31);
            this.lvFunctions.Name = "lvFunctions";
            this.lvFunctions.Size = new System.Drawing.Size(334, 177);
            this.lvFunctions.TabIndex = 35;
            this.lvFunctions.UseCompatibleStateImageBehavior = false;
            this.lvFunctions.View = View.Details;
            this.lvFunctions.SelectedIndexChanged += new EventHandler(this.lvFunctions_SelectedIndexChanged);
            this.columnHeader7.Text = "菜单项";
            this.columnHeader7.Width = 290;
            this.btnDeleteLayers.Enabled = false;
            this.btnDeleteLayers.Location = new Point(329, 55);
            this.btnDeleteLayers.Name = "btnDeleteLayers";
            this.btnDeleteLayers.Size = new System.Drawing.Size(83, 23);
            this.btnDeleteLayers.TabIndex = 39;
            this.btnDeleteLayers.Text = "删除";
            this.btnDeleteLayers.UseVisualStyleBackColor = true;
            this.btnDeleteLayers.Click += new EventHandler(this.btnDeleteLayers_Click);
            this.btnAddLayers.Location = new Point(329, 26);
            this.btnAddLayers.Name = "btnAddLayers";
            this.btnAddLayers.Size = new System.Drawing.Size(83, 23);
            this.btnAddLayers.TabIndex = 38;
            this.btnAddLayers.Text = "添加";
            this.btnAddLayers.UseVisualStyleBackColor = true;
            this.btnAddLayers.Click += new EventHandler(this.btnAddLayers_Click);
            this.btnDeleteMenu.Enabled = false;
            this.btnDeleteMenu.Location = new Point(346, 60);
            this.btnDeleteMenu.Name = "btnDeleteMenu";
            this.btnDeleteMenu.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteMenu.TabIndex = 37;
            this.btnDeleteMenu.Text = "删除";
            this.btnDeleteMenu.UseVisualStyleBackColor = true;
            this.btnDeleteMenu.Click += new EventHandler(this.btnDeleteMenu_Click);
            this.btnAddMenu.Location = new Point(346, 31);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(75, 23);
            this.btnAddMenu.TabIndex = 36;
            this.btnAddMenu.Text = "添加";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new EventHandler(this.btnAddMenu_Click);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(3, 172);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(435, 239);
            this.tabControl1.TabIndex = 40;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnNewRole);
            this.tabPage1.Controls.Add(this.btnDeleteRole);
            this.tabPage1.Controls.Add(this.lvRoles);
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(427, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "隶属的角色设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add(this.lvFunctions);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnAddMenu);
            this.tabPage2.Controls.Add(this.btnDeleteMenu);
            this.tabPage2.Location = new Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(427, 213);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "功能设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage3.Controls.Add(this.btnAll);
            this.tabPage3.Controls.Add(this.btnEditGrant);
            this.tabPage3.Controls.Add(this.lvLayers);
            this.tabPage3.Controls.Add(this.btnDeleteLayers);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btnAddLayers);
            this.tabPage3.Location = new Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(427, 213);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "图层权限设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.btnAll.Location = new Point(329, 113);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(83, 23);
            this.btnAll.TabIndex = 41;
            this.btnAll.Text = "修改所有";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new EventHandler(this.btnAll_Click);
            this.btnEditGrant.Enabled = false;
            this.btnEditGrant.Location = new Point(329, 84);
            this.btnEditGrant.Name = "btnEditGrant";
            this.btnEditGrant.Size = new System.Drawing.Size(83, 23);
            this.btnEditGrant.TabIndex = 40;
            this.btnEditGrant.Text = "修改权限";
            this.btnEditGrant.UseVisualStyleBackColor = true;
            this.btnEditGrant.Click += new EventHandler(this.btnEditGrant_Click);
            this.btnEdit.Location = new Point(363, 86);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 41;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
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
            base.ClientSize = new System.Drawing.Size(450, 463);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnDeleteUser);
            base.Controls.Add(this.btnAddUser);
            base.Controls.Add(this.lvStaff);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmUserManage";
            this.Text = "用户管理";
            base.Load += new EventHandler(this.frmUserManage_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
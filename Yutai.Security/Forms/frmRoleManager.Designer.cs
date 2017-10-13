using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Security.Forms
{
    partial class frmRoleManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnNewRole;

        private Label label1;

        private Button btnDeleteRole;

        private Label label2;

        private ListView lvStaffRole;

        private ColumnHeader columnHeader3;

        private ColumnHeader columnHeader4;

        private Button btnAssignUser;

        private Button btnDeleteAssignUser;

        private Button btnDeleteMenu;

        private Button btnAddMenu;

        private Label label3;

        private Button btnDeleteLayers;

        private Button btnAddLayers;

        private ListView lvLayers;

        private ListView lvRoles;

        private ColumnHeader columnHeader1;

        private ColumnHeader columnHeader2;

        private ColumnHeader columnHeader5;

        private ColumnHeader columnHeader6;

        private Label label4;

        private ListView lvFunctions;

        private Button button1;

        private TabControl tabControl1;

        private TabPage tabPage1;

        private TabPage tabPage2;

        private TabPage tabPage3;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmRoleManager));
            this.btnNewRole = new Button();
            this.label1 = new Label();
            this.btnDeleteRole = new Button();
            this.label2 = new Label();
            this.lvStaffRole = new ListView();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.btnAssignUser = new Button();
            this.btnDeleteAssignUser = new Button();
            this.btnDeleteMenu = new Button();
            this.btnAddMenu = new Button();
            this.label3 = new Label();
            this.btnDeleteLayers = new Button();
            this.btnAddLayers = new Button();
            this.lvLayers = new ListView();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.label4 = new Label();
            this.lvRoles = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.lvFunctions = new ListView();
            this.button1 = new Button();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabPage3 = new TabPage();
            this.btnAll = new Button();
            this.btnEditGrant = new Button();
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
            this.btnNewRole.Location = new Point(360, 28);
            this.btnNewRole.Name = "btnNewRole";
            this.btnNewRole.Size = new System.Drawing.Size(75, 23);
            this.btnNewRole.TabIndex = 0;
            this.btnNewRole.Text = "新建角色";
            this.btnNewRole.UseVisualStyleBackColor = true;
            this.btnNewRole.Click += new EventHandler(this.btnNewRole_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "角色列表";
            this.btnDeleteRole.Location = new Point(360, 57);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRole.TabIndex = 3;
            this.btnDeleteRole.Text = "删除";
            this.btnDeleteRole.UseVisualStyleBackColor = true;
            this.btnDeleteRole.Click += new EventHandler(this.btnDeleteRole_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "隶属于该角色的用户列表";
            ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader3, this.columnHeader4 };
            this.lvStaffRole.Columns.AddRange(columnHeaderArray);
            this.lvStaffRole.FullRowSelect = true;
            this.lvStaffRole.HideSelection = false;
            this.lvStaffRole.Location = new Point(8, 31);
            this.lvStaffRole.Name = "lvStaffRole";
            this.lvStaffRole.Size = new System.Drawing.Size(330, 202);
            this.lvStaffRole.TabIndex = 5;
            this.lvStaffRole.UseCompatibleStateImageBehavior = false;
            this.lvStaffRole.View = View.Details;
            this.lvStaffRole.SelectedIndexChanged += new EventHandler(this.lvStaffRole_SelectedIndexChanged);
            this.columnHeader3.Text = "用户名";
            this.columnHeader3.Width = 119;
            this.columnHeader4.Text = "登录名";
            this.columnHeader4.Width = 145;
            this.btnAssignUser.Location = new Point(344, 31);
            this.btnAssignUser.Name = "btnAssignUser";
            this.btnAssignUser.Size = new System.Drawing.Size(75, 23);
            this.btnAssignUser.TabIndex = 6;
            this.btnAssignUser.Text = "添加用户";
            this.btnAssignUser.UseVisualStyleBackColor = true;
            this.btnAssignUser.Click += new EventHandler(this.btnAssignUser_Click);
            this.btnDeleteAssignUser.Enabled = false;
            this.btnDeleteAssignUser.Location = new Point(344, 60);
            this.btnDeleteAssignUser.Name = "btnDeleteAssignUser";
            this.btnDeleteAssignUser.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAssignUser.TabIndex = 7;
            this.btnDeleteAssignUser.Text = "删除";
            this.btnDeleteAssignUser.UseVisualStyleBackColor = true;
            this.btnDeleteAssignUser.Click += new EventHandler(this.btnDeleteAssignUser_Click);
            this.btnDeleteMenu.Enabled = false;
            this.btnDeleteMenu.Location = new Point(328, 55);
            this.btnDeleteMenu.Name = "btnDeleteMenu";
            this.btnDeleteMenu.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteMenu.TabIndex = 11;
            this.btnDeleteMenu.Text = "删除";
            this.btnDeleteMenu.UseVisualStyleBackColor = true;
            this.btnDeleteMenu.Click += new EventHandler(this.btnDeleteMenu_Click);
            this.btnAddMenu.Location = new Point(328, 26);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(75, 23);
            this.btnAddMenu.TabIndex = 10;
            this.btnAddMenu.Text = "添加";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new EventHandler(this.btnAddMenu_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "该角色的具备的功能列表";
            this.btnDeleteLayers.Enabled = false;
            this.btnDeleteLayers.Location = new Point(323, 72);
            this.btnDeleteLayers.Name = "btnDeleteLayers";
            this.btnDeleteLayers.Size = new System.Drawing.Size(83, 23);
            this.btnDeleteLayers.TabIndex = 15;
            this.btnDeleteLayers.Text = "删除";
            this.btnDeleteLayers.UseVisualStyleBackColor = true;
            this.btnDeleteLayers.Click += new EventHandler(this.btnDeleteLayers_Click);
            this.btnAddLayers.Location = new Point(323, 43);
            this.btnAddLayers.Name = "btnAddLayers";
            this.btnAddLayers.Size = new System.Drawing.Size(83, 23);
            this.btnAddLayers.TabIndex = 14;
            this.btnAddLayers.Text = "添加";
            this.btnAddLayers.UseVisualStyleBackColor = true;
            this.btnAddLayers.Click += new EventHandler(this.btnAddLayers_Click);
            ColumnHeader[]  columnHeaderArray1 = new ColumnHeader[] { this.columnHeader5, this.columnHeader6 };
            this.lvLayers.Columns.AddRange(columnHeaderArray1);
            this.lvLayers.FullRowSelect = true;
            this.lvLayers.HideSelection = false;
            this.lvLayers.Location = new Point(3, 32);
            this.lvLayers.Name = "lvLayers";
            this.lvLayers.Size = new System.Drawing.Size(317, 201);
            this.lvLayers.TabIndex = 13;
            this.lvLayers.UseCompatibleStateImageBehavior = false;
            this.lvLayers.View = View.Details;
            this.lvLayers.SelectedIndexChanged += new EventHandler(this.lvLayers_SelectedIndexChanged);
            this.columnHeader5.Text = "图层名";
            this.columnHeader5.Width = 93;
            this.columnHeader6.Text = "权限";
            this.columnHeader6.Width = 72;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "该角色的图层控制权相设置";
            ColumnHeader[]  columnHeaderArray2 = new ColumnHeader[] { this.columnHeader1, this.columnHeader2 };
            this.lvRoles.Columns.AddRange(columnHeaderArray2);
            this.lvRoles.FullRowSelect = true;
            this.lvRoles.HideSelection = false;
            this.lvRoles.Location = new Point(2, 28);
            this.lvRoles.MultiSelect = false;
            this.lvRoles.Name = "lvRoles";
            this.lvRoles.Size = new System.Drawing.Size(352, 128);
            this.lvRoles.TabIndex = 16;
            this.lvRoles.UseCompatibleStateImageBehavior = false;
            this.lvRoles.View = View.Details;
            this.lvRoles.SelectedIndexChanged += new EventHandler(this.lvRoles_SelectedIndexChanged);
            this.columnHeader1.Text = "角色名";
            this.columnHeader1.Width = 142;
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 187;
            this.lvFunctions.Location = new Point(8, 27);
            this.lvFunctions.Name = "lvFunctions";
            this.lvFunctions.Size = new System.Drawing.Size(301, 206);
            this.lvFunctions.TabIndex = 18;
            this.lvFunctions.UseCompatibleStateImageBehavior = false;
            this.lvFunctions.View = View.List;
            this.lvFunctions.SelectedIndexChanged += new EventHandler(this.lvFunctions_SelectedIndexChanged);
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new Point(340, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(2, 162);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(433, 264);
            this.tabControl1.TabIndex = 20;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lvStaffRole);
            this.tabPage1.Controls.Add(this.btnAssignUser);
            this.tabPage1.Controls.Add(this.btnDeleteAssignUser);
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(425, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "隶属于该角色的用户列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add(this.lvFunctions);
            this.tabPage2.Controls.Add(this.btnDeleteMenu);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnAddMenu);
            this.tabPage2.Location = new Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(425, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "角色可用的功能";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage3.Controls.Add(this.btnAll);
            this.tabPage3.Controls.Add(this.lvLayers);
            this.tabPage3.Controls.Add(this.btnEditGrant);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btnAddLayers);
            this.tabPage3.Controls.Add(this.btnDeleteLayers);
            this.tabPage3.Location = new Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(425, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "该角色可对图层的权限设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.btnAll.Location = new Point(323, 130);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(83, 23);
            this.btnAll.TabIndex = 43;
            this.btnAll.Text = "修改所有";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new EventHandler(this.btnAll_Click);
            this.btnEditGrant.Enabled = false;
            this.btnEditGrant.Location = new Point(323, 101);
            this.btnEditGrant.Name = "btnEditGrant";
            this.btnEditGrant.Size = new System.Drawing.Size(83, 23);
            this.btnEditGrant.TabIndex = 42;
            this.btnEditGrant.Text = "修改权限";
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
            base.ClientSize = new System.Drawing.Size(437, 467);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.lvRoles);
            base.Controls.Add(this.btnDeleteRole);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnNewRole);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRoleManager";
            this.Text = "角色管理";
            base.Load += new EventHandler(this.frmRoleManager_Load);
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
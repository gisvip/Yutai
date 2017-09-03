namespace Yutai.Pipeline.Analysis.Forms
{
    partial class frm3DBuilder
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
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNameSuf = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtSaveAt = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDefaultSectionType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDefaultDepthType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkLJD = new System.Windows.Forms.CheckBox();
            this.chkJXJ = new System.Windows.Forms.CheckBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.cmbHeightType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSectionType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDepthType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.chkExtent = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameSuf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaveAt.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(393, 444);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "生成";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(493, 444);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(4, 8);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(564, 430);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.chkExtent);
            this.xtraTabPage1.Controls.Add(this.comboBox1);
            this.xtraTabPage1.Controls.Add(this.label9);
            this.xtraTabPage1.Controls.Add(this.txtNameSuf);
            this.xtraTabPage1.Controls.Add(this.label5);
            this.xtraTabPage1.Controls.Add(this.simpleButton1);
            this.xtraTabPage1.Controls.Add(this.txtSaveAt);
            this.xtraTabPage1.Controls.Add(this.label4);
            this.xtraTabPage1.Controls.Add(this.cmbDefaultSectionType);
            this.xtraTabPage1.Controls.Add(this.label3);
            this.xtraTabPage1.Controls.Add(this.cmbDefaultDepthType);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.chkLJD);
            this.xtraTabPage1.Controls.Add(this.chkJXJ);
            this.xtraTabPage1.Controls.Add(this.cmbDivision);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(558, 401);
            this.xtraTabPage1.Text = "基本配置";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "依据源数据保存",
            "单独保存"});
            this.comboBox1.Location = new System.Drawing.Point(102, 113);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 22);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "保存方式：";
            // 
            // txtNameSuf
            // 
            this.txtNameSuf.Location = new System.Drawing.Point(102, 172);
            this.txtNameSuf.Name = "txtNameSuf";
            this.txtNameSuf.Size = new System.Drawing.Size(217, 20);
            this.txtNameSuf.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "名称后缀：";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(325, 143);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(45, 23);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "选择";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtSaveAt
            // 
            this.txtSaveAt.Location = new System.Drawing.Point(102, 146);
            this.txtSaveAt.Name = "txtSaveAt";
            this.txtSaveAt.Size = new System.Drawing.Size(217, 20);
            this.txtSaveAt.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "保存位置：";
            // 
            // cmbDefaultSectionType
            // 
            this.cmbDefaultSectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultSectionType.FormattingEnabled = true;
            this.cmbDefaultSectionType.Items.AddRange(new object[] {
            "高*宽",
            "宽*高"});
            this.cmbDefaultSectionType.Location = new System.Drawing.Point(102, 74);
            this.cmbDefaultSectionType.Name = "cmbDefaultSectionType";
            this.cmbDefaultSectionType.Size = new System.Drawing.Size(121, 22);
            this.cmbDefaultSectionType.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "默认截面类型：";
            // 
            // cmbDefaultDepthType
            // 
            this.cmbDefaultDepthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultDepthType.FormattingEnabled = true;
            this.cmbDefaultDepthType.Items.AddRange(new object[] {
            "埋深",
            "高程"});
            this.cmbDefaultDepthType.Location = new System.Drawing.Point(102, 45);
            this.cmbDefaultDepthType.Name = "cmbDefaultDepthType";
            this.cmbDefaultDepthType.Size = new System.Drawing.Size(121, 22);
            this.cmbDefaultDepthType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "默认埋深类型：";
            // 
            // chkLJD
            // 
            this.chkLJD.AutoSize = true;
            this.chkLJD.Location = new System.Drawing.Point(243, 45);
            this.chkLJD.Name = "chkLJD";
            this.chkLJD.Size = new System.Drawing.Size(62, 18);
            this.chkLJD.TabIndex = 3;
            this.chkLJD.Text = "连接段";
            this.chkLJD.UseVisualStyleBackColor = true;
            // 
            // chkJXJ
            // 
            this.chkJXJ.AutoSize = true;
            this.chkJXJ.Location = new System.Drawing.Point(243, 18);
            this.chkJXJ.Name = "chkJXJ";
            this.chkJXJ.Size = new System.Drawing.Size(86, 18);
            this.chkJXJ.TabIndex = 2;
            this.chkJXJ.Text = "检修井生成";
            this.chkJXJ.UseVisualStyleBackColor = true;
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Items.AddRange(new object[] {
            "12",
            "18",
            "24",
            "32",
            "64"});
            this.cmbDivision.Location = new System.Drawing.Point(102, 16);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 22);
            this.cmbDivision.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "圆截面等分数：";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.cmbHeightType);
            this.xtraTabPage2.Controls.Add(this.label8);
            this.xtraTabPage2.Controls.Add(this.cmbSectionType);
            this.xtraTabPage2.Controls.Add(this.label6);
            this.xtraTabPage2.Controls.Add(this.cmbDepthType);
            this.xtraTabPage2.Controls.Add(this.label7);
            this.xtraTabPage2.Controls.Add(this.treeList1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(558, 401);
            this.xtraTabPage2.Text = "数据源";
            // 
            // cmbHeightType
            // 
            this.cmbHeightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeightType.FormattingEnabled = true;
            this.cmbHeightType.Items.AddRange(new object[] {
            "管顶",
            "管底",
            "管中"});
            this.cmbHeightType.Location = new System.Drawing.Point(374, 19);
            this.cmbHeightType.Name = "cmbHeightType";
            this.cmbHeightType.Size = new System.Drawing.Size(121, 22);
            this.cmbHeightType.TabIndex = 13;
            this.cmbHeightType.SelectedIndexChanged += new System.EventHandler(this.cmbHeightType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "埋深取值：";
            // 
            // cmbSectionType
            // 
            this.cmbSectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSectionType.FormattingEnabled = true;
            this.cmbSectionType.Items.AddRange(new object[] {
            "高*宽",
            "宽*高"});
            this.cmbSectionType.Location = new System.Drawing.Point(374, 96);
            this.cmbSectionType.Name = "cmbSectionType";
            this.cmbSectionType.Size = new System.Drawing.Size(121, 22);
            this.cmbSectionType.TabIndex = 11;
            this.cmbSectionType.SelectedIndexChanged += new System.EventHandler(this.cmbSectionType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "截面类型：";
            // 
            // cmbDepthType
            // 
            this.cmbDepthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepthType.FormattingEnabled = true;
            this.cmbDepthType.Items.AddRange(new object[] {
            "埋深",
            "高程"});
            this.cmbDepthType.Location = new System.Drawing.Point(374, 56);
            this.cmbDepthType.Name = "cmbDepthType";
            this.cmbDepthType.Size = new System.Drawing.Size(121, 22);
            this.cmbDepthType.TabIndex = 9;
            this.cmbDepthType.SelectedIndexChanged += new System.EventHandler(this.cmbDepthType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "埋深类型：";
            // 
            // treeList1
            // 
            this.treeList1.CheckBoxFieldName = "Selected";
            this.treeList1.KeyFieldName = "KeyFieldID";
            this.treeList1.Location = new System.Drawing.Point(7, 13);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList1.Size = new System.Drawing.Size(264, 372);
            this.treeList1.TabIndex = 0;
            this.treeList1.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.treeList1_BeforeCheckNode);
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // chkExtent
            // 
            this.chkExtent.AutoSize = true;
            this.chkExtent.Location = new System.Drawing.Point(243, 74);
            this.chkExtent.Name = "chkExtent";
            this.chkExtent.Size = new System.Drawing.Size(74, 18);
            this.chkExtent.TabIndex = 15;
            this.chkExtent.Text = "视图范围";
            this.chkExtent.UseVisualStyleBackColor = true;
            // 
            // frm3DBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 479);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm3DBuilder";
            this.Text = "三维管线生成";
            this.Load += new System.EventHandler(this.frm3DBuilder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameSuf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaveAt.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.CheckBox chkLJD;
        private System.Windows.Forms.CheckBox chkJXJ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDefaultSectionType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDefaultDepthType;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtNameSuf;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtSaveAt;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.ComboBox cmbHeightType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSectionType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDepthType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkExtent;
    }
}
namespace Yutai.Pipeline3D.Forms
{
    partial class Frm3DBuilder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListBoxPipelineLayers = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSectionType = new System.Windows.Forms.ComboBox();
            this.cmbRotationAngleType = new System.Windows.Forms.ComboBox();
            this.cmbHeightType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numDivision = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSelectWorkspace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSaveAt = new System.Windows.Forms.TextBox();
            this._nameSuf = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSelectSquares = new System.Windows.Forms.Button();
            this.btnSelectCylinders = new System.Windows.Forms.Button();
            this.txtSquareSubs = new System.Windows.Forms.TextBox();
            this.txtCylinderSubs = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbJgggField = new System.Windows.Forms.ComboBox();
            this.cmbXzjdField = new System.Windows.Forms.ComboBox();
            this.cmbFswField = new System.Windows.Forms.ComboBox();
            this.cmbJdsdField = new System.Windows.Forms.ComboBox();
            this.cmbDmgcField = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbZdgcField = new System.Windows.Forms.ComboBox();
            this.cmbZdmsField = new System.Windows.Forms.ComboBox();
            this.cmbGjField = new System.Windows.Forms.ComboBox();
            this.cmbQdmsField = new System.Windows.Forms.ComboBox();
            this.cmbQdgcField = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDivision)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListBoxPipelineLayers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 412);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据源";
            // 
            // checkedListBoxPipelineLayers
            // 
            this.checkedListBoxPipelineLayers.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxPipelineLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxPipelineLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxPipelineLayers.FormattingEnabled = true;
            this.checkedListBoxPipelineLayers.Location = new System.Drawing.Point(3, 17);
            this.checkedListBoxPipelineLayers.Name = "checkedListBoxPipelineLayers";
            this.checkedListBoxPipelineLayers.Size = new System.Drawing.Size(205, 392);
            this.checkedListBoxPipelineLayers.TabIndex = 0;
            this.checkedListBoxPipelineLayers.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxPipelineLayers_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbSectionType);
            this.groupBox2.Controls.Add(this.cmbRotationAngleType);
            this.groupBox2.Controls.Add(this.cmbHeightType);
            this.groupBox2.Location = new System.Drawing.Point(233, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 75);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "取值信息";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "管线断面类型：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 49);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(89, 12);
            this.label18.TabIndex = 1;
            this.label18.Text = "符号角度类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "管线高程类型：";
            // 
            // cmbSectionType
            // 
            this.cmbSectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSectionType.FormattingEnabled = true;
            this.cmbSectionType.Items.AddRange(new object[] {
            "高*宽",
            "宽*高"});
            this.cmbSectionType.Location = new System.Drawing.Point(285, 20);
            this.cmbSectionType.Name = "cmbSectionType";
            this.cmbSectionType.Size = new System.Drawing.Size(76, 20);
            this.cmbSectionType.TabIndex = 0;
            // 
            // cmbRotationAngleType
            // 
            this.cmbRotationAngleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRotationAngleType.FormattingEnabled = true;
            this.cmbRotationAngleType.Items.AddRange(new object[] {
            "角度",
            "弧度"});
            this.cmbRotationAngleType.Location = new System.Drawing.Point(101, 46);
            this.cmbRotationAngleType.Name = "cmbRotationAngleType";
            this.cmbRotationAngleType.Size = new System.Drawing.Size(76, 20);
            this.cmbRotationAngleType.TabIndex = 0;
            // 
            // cmbHeightType
            // 
            this.cmbHeightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeightType.FormattingEnabled = true;
            this.cmbHeightType.Items.AddRange(new object[] {
            "管顶",
            "管中",
            "管底"});
            this.cmbHeightType.Location = new System.Drawing.Point(101, 20);
            this.cmbHeightType.Name = "cmbHeightType";
            this.cmbHeightType.Size = new System.Drawing.Size(76, 20);
            this.cmbHeightType.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numDivision);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.btnSelectWorkspace);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtSaveAt);
            this.groupBox3.Controls.Add(this._nameSuf);
            this.groupBox3.Location = new System.Drawing.Point(12, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 104);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "保存信息";
            // 
            // numDivision
            // 
            this.numDivision.Increment = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numDivision.Location = new System.Drawing.Point(101, 18);
            this.numDivision.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numDivision.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numDivision.Name = "numDivision";
            this.numDivision.Size = new System.Drawing.Size(110, 21);
            this.numDivision.TabIndex = 5;
            this.numDivision.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "圆截面等分数：";
            // 
            // btnSelectWorkspace
            // 
            this.btnSelectWorkspace.Location = new System.Drawing.Point(404, 72);
            this.btnSelectWorkspace.Name = "btnSelectWorkspace";
            this.btnSelectWorkspace.Size = new System.Drawing.Size(85, 21);
            this.btnSelectWorkspace.TabIndex = 2;
            this.btnSelectWorkspace.Text = "选择";
            this.btnSelectWorkspace.UseVisualStyleBackColor = true;
            this.btnSelectWorkspace.Click += new System.EventHandler(this.btnSelectWorkspace_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "保存位置：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称后缀：";
            // 
            // txtSaveAt
            // 
            this.txtSaveAt.Location = new System.Drawing.Point(77, 72);
            this.txtSaveAt.Name = "txtSaveAt";
            this.txtSaveAt.ReadOnly = true;
            this.txtSaveAt.Size = new System.Drawing.Size(321, 21);
            this.txtSaveAt.TabIndex = 0;
            // 
            // _nameSuf
            // 
            this._nameSuf.Location = new System.Drawing.Point(77, 45);
            this._nameSuf.Name = "_nameSuf";
            this._nameSuf.Size = new System.Drawing.Size(216, 21);
            this._nameSuf.TabIndex = 0;
            this._nameSuf.Text = "_3D";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(513, 501);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(95, 33);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.btnSelectSquares);
            this.groupBox5.Controls.Add(this.btnSelectCylinders);
            this.groupBox5.Controls.Add(this.txtSquareSubs);
            this.groupBox5.Controls.Add(this.txtCylinderSubs);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.cmbJgggField);
            this.groupBox5.Controls.Add(this.cmbXzjdField);
            this.groupBox5.Controls.Add(this.cmbFswField);
            this.groupBox5.Controls.Add(this.cmbJdsdField);
            this.groupBox5.Controls.Add(this.cmbDmgcField);
            this.groupBox5.Location = new System.Drawing.Point(233, 93);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(375, 222);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "管点配置";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "篦 子 类：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "检修井类：";
            // 
            // btnSelectSquares
            // 
            this.btnSelectSquares.Location = new System.Drawing.Point(286, 124);
            this.btnSelectSquares.Name = "btnSelectSquares";
            this.btnSelectSquares.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSquares.TabIndex = 3;
            this.btnSelectSquares.Text = "选择";
            this.btnSelectSquares.UseVisualStyleBackColor = true;
            this.btnSelectSquares.Click += new System.EventHandler(this.btnSelectSquares_Click);
            // 
            // btnSelectCylinders
            // 
            this.btnSelectCylinders.Location = new System.Drawing.Point(286, 97);
            this.btnSelectCylinders.Name = "btnSelectCylinders";
            this.btnSelectCylinders.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCylinders.TabIndex = 3;
            this.btnSelectCylinders.Text = "选择";
            this.btnSelectCylinders.UseVisualStyleBackColor = true;
            this.btnSelectCylinders.Click += new System.EventHandler(this.btnSelectCylinders_Click);
            // 
            // txtSquareSubs
            // 
            this.txtSquareSubs.Location = new System.Drawing.Point(77, 125);
            this.txtSquareSubs.Name = "txtSquareSubs";
            this.txtSquareSubs.ReadOnly = true;
            this.txtSquareSubs.Size = new System.Drawing.Size(203, 21);
            this.txtSquareSubs.TabIndex = 2;
            // 
            // txtCylinderSubs
            // 
            this.txtCylinderSubs.Location = new System.Drawing.Point(77, 98);
            this.txtCylinderSubs.Name = "txtCylinderSubs";
            this.txtCylinderSubs.ReadOnly = true;
            this.txtCylinderSubs.Size = new System.Drawing.Size(203, 21);
            this.txtCylinderSubs.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "井盖规格：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(190, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "旋 转 角：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "附 属 物：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "井底深度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "地面高程：";
            // 
            // cmbJgggField
            // 
            this.cmbJgggField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJgggField.FormattingEnabled = true;
            this.cmbJgggField.Location = new System.Drawing.Point(261, 20);
            this.cmbJgggField.Name = "cmbJgggField";
            this.cmbJgggField.Size = new System.Drawing.Size(100, 20);
            this.cmbJgggField.TabIndex = 0;
            // 
            // cmbXzjdField
            // 
            this.cmbXzjdField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXzjdField.FormattingEnabled = true;
            this.cmbXzjdField.Location = new System.Drawing.Point(261, 46);
            this.cmbXzjdField.Name = "cmbXzjdField";
            this.cmbXzjdField.Size = new System.Drawing.Size(100, 20);
            this.cmbXzjdField.TabIndex = 0;
            // 
            // cmbFswField
            // 
            this.cmbFswField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFswField.FormattingEnabled = true;
            this.cmbFswField.Location = new System.Drawing.Point(77, 72);
            this.cmbFswField.Name = "cmbFswField";
            this.cmbFswField.Size = new System.Drawing.Size(100, 20);
            this.cmbFswField.TabIndex = 0;
            // 
            // cmbJdsdField
            // 
            this.cmbJdsdField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJdsdField.FormattingEnabled = true;
            this.cmbJdsdField.Location = new System.Drawing.Point(77, 46);
            this.cmbJdsdField.Name = "cmbJdsdField";
            this.cmbJdsdField.Size = new System.Drawing.Size(100, 20);
            this.cmbJdsdField.TabIndex = 0;
            // 
            // cmbDmgcField
            // 
            this.cmbDmgcField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDmgcField.FormattingEnabled = true;
            this.cmbDmgcField.Location = new System.Drawing.Point(77, 20);
            this.cmbDmgcField.Name = "cmbDmgcField";
            this.cmbDmgcField.Size = new System.Drawing.Size(100, 20);
            this.cmbDmgcField.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.cmbZdgcField);
            this.groupBox4.Controls.Add(this.cmbZdmsField);
            this.groupBox4.Controls.Add(this.cmbGjField);
            this.groupBox4.Controls.Add(this.cmbQdmsField);
            this.groupBox4.Controls.Add(this.cmbQdgcField);
            this.groupBox4.Location = new System.Drawing.Point(233, 321);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(375, 103);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "管线配置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "终点高程：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "终点埋深：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "管    径：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "起点埋深：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "起点高程：";
            // 
            // cmbZdgcField
            // 
            this.cmbZdgcField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZdgcField.FormattingEnabled = true;
            this.cmbZdgcField.Location = new System.Drawing.Point(261, 20);
            this.cmbZdgcField.Name = "cmbZdgcField";
            this.cmbZdgcField.Size = new System.Drawing.Size(100, 20);
            this.cmbZdgcField.TabIndex = 0;
            // 
            // cmbZdmsField
            // 
            this.cmbZdmsField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZdmsField.FormattingEnabled = true;
            this.cmbZdmsField.Location = new System.Drawing.Point(261, 46);
            this.cmbZdmsField.Name = "cmbZdmsField";
            this.cmbZdmsField.Size = new System.Drawing.Size(100, 20);
            this.cmbZdmsField.TabIndex = 0;
            // 
            // cmbGjField
            // 
            this.cmbGjField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGjField.FormattingEnabled = true;
            this.cmbGjField.Location = new System.Drawing.Point(77, 72);
            this.cmbGjField.Name = "cmbGjField";
            this.cmbGjField.Size = new System.Drawing.Size(100, 20);
            this.cmbGjField.TabIndex = 0;
            // 
            // cmbQdmsField
            // 
            this.cmbQdmsField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQdmsField.FormattingEnabled = true;
            this.cmbQdmsField.Location = new System.Drawing.Point(77, 46);
            this.cmbQdmsField.Name = "cmbQdmsField";
            this.cmbQdmsField.Size = new System.Drawing.Size(100, 20);
            this.cmbQdmsField.TabIndex = 0;
            // 
            // cmbQdgcField
            // 
            this.cmbQdgcField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQdgcField.FormattingEnabled = true;
            this.cmbQdgcField.Location = new System.Drawing.Point(77, 20);
            this.cmbQdgcField.Name = "cmbQdgcField";
            this.cmbQdgcField.Size = new System.Drawing.Size(100, 20);
            this.cmbQdgcField.TabIndex = 0;
            // 
            // Frm3DBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 546);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm3DBuilder";
            this.Text = "生成三维实体";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDivision)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBoxPipelineLayers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox _nameSuf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSaveAt;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSelectWorkspace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbHeightType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSectionType;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbJgggField;
        private System.Windows.Forms.ComboBox cmbXzjdField;
        private System.Windows.Forms.ComboBox cmbJdsdField;
        private System.Windows.Forms.ComboBox cmbDmgcField;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbZdgcField;
        private System.Windows.Forms.ComboBox cmbZdmsField;
        private System.Windows.Forms.ComboBox cmbQdmsField;
        private System.Windows.Forms.ComboBox cmbQdgcField;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbGjField;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numDivision;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbFswField;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSelectSquares;
        private System.Windows.Forms.Button btnSelectCylinders;
        private System.Windows.Forms.TextBox txtSquareSubs;
        private System.Windows.Forms.TextBox txtCylinderSubs;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbRotationAngleType;
    }
}
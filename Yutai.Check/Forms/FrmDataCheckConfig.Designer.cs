namespace Yutai.Check.Forms
{
    partial class FrmDataCheckConfig
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numElevationTolerance = new System.Windows.Forms.NumericUpDown();
            this.numSurfaceTolerance = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioGroupElevationCheckType = new DevExpress.XtraEditors.RadioGroup();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numCompareLimit = new System.Windows.Forms.NumericUpDown();
            this.numCompareRadius = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNX = new System.Windows.Forms.TextBox();
            this.textBoxZX = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMultiConnect = new System.Windows.Forms.TextBox();
            this.textBoxFourConnect = new System.Windows.Forms.TextBox();
            this.textBoxThreeConnect = new System.Windows.Forms.TextBox();
            this.textBoxStraightPnt = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.richTextBoxValues = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSurfaceTolerance)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupElevationCheckType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompareLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompareRadius)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numElevationTolerance);
            this.groupBox1.Controls.Add(this.numSurfaceTolerance);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间检查设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "高程容差";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "平面容差";
            // 
            // numElevationTolerance
            // 
            this.numElevationTolerance.DecimalPlaces = 3;
            this.numElevationTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numElevationTolerance.Location = new System.Drawing.Point(65, 47);
            this.numElevationTolerance.Name = "numElevationTolerance";
            this.numElevationTolerance.Size = new System.Drawing.Size(120, 21);
            this.numElevationTolerance.TabIndex = 0;
            this.numElevationTolerance.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // numSurfaceTolerance
            // 
            this.numSurfaceTolerance.DecimalPlaces = 3;
            this.numSurfaceTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numSurfaceTolerance.Location = new System.Drawing.Point(65, 20);
            this.numSurfaceTolerance.Name = "numSurfaceTolerance";
            this.numSurfaceTolerance.Size = new System.Drawing.Size(120, 21);
            this.numSurfaceTolerance.TabIndex = 0;
            this.numSurfaceTolerance.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioGroupElevationCheckType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numCompareLimit);
            this.groupBox2.Controls.Add(this.numCompareRadius);
            this.groupBox2.Location = new System.Drawing.Point(217, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 78);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "高程检查设置";
            // 
            // radioGroupElevationCheckType
            // 
            this.radioGroupElevationCheckType.EditValue = "UseAttribute";
            this.radioGroupElevationCheckType.Location = new System.Drawing.Point(191, 13);
            this.radioGroupElevationCheckType.Name = "radioGroupElevationCheckType";
            this.radioGroupElevationCheckType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupElevationCheckType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupElevationCheckType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupElevationCheckType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("UseAttribute", "使用属性"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("UseZValue", "使用Z值")});
            this.radioGroupElevationCheckType.Size = new System.Drawing.Size(88, 55);
            this.radioGroupElevationCheckType.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "对比限值";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "对比半径";
            // 
            // numCompareLimit
            // 
            this.numCompareLimit.DecimalPlaces = 2;
            this.numCompareLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCompareLimit.Location = new System.Drawing.Point(65, 47);
            this.numCompareLimit.Name = "numCompareLimit";
            this.numCompareLimit.Size = new System.Drawing.Size(120, 21);
            this.numCompareLimit.TabIndex = 0;
            this.numCompareLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numCompareRadius
            // 
            this.numCompareRadius.DecimalPlaces = 2;
            this.numCompareRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCompareRadius.Location = new System.Drawing.Point(65, 20);
            this.numCompareRadius.Name = "numCompareRadius";
            this.numCompareRadius.Size = new System.Drawing.Size(120, 21);
            this.numCompareRadius.TabIndex = 0;
            this.numCompareRadius.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxNX);
            this.groupBox3.Controls.Add(this.textBoxZX);
            this.groupBox3.Location = new System.Drawing.Point(12, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 78);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "流向检查设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "  逆向值";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "  正向值";
            // 
            // textBoxNX
            // 
            this.textBoxNX.Location = new System.Drawing.Point(65, 47);
            this.textBoxNX.Name = "textBoxNX";
            this.textBoxNX.Size = new System.Drawing.Size(120, 21);
            this.textBoxNX.TabIndex = 0;
            this.textBoxNX.Text = "1";
            // 
            // textBoxZX
            // 
            this.textBoxZX.Location = new System.Drawing.Point(65, 20);
            this.textBoxZX.Name = "textBoxZX";
            this.textBoxZX.Size = new System.Drawing.Size(120, 21);
            this.textBoxZX.TabIndex = 0;
            this.textBoxZX.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxMultiConnect);
            this.groupBox4.Controls.Add(this.textBoxFourConnect);
            this.groupBox4.Controls.Add(this.textBoxThreeConnect);
            this.groupBox4.Controls.Add(this.textBoxStraightPnt);
            this.groupBox4.Location = new System.Drawing.Point(12, 180);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 131);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "特征点类型检查设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "多通点";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "四通点";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "三通点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "直线点";
            // 
            // textBoxMultiConnect
            // 
            this.textBoxMultiConnect.Location = new System.Drawing.Point(65, 101);
            this.textBoxMultiConnect.Name = "textBoxMultiConnect";
            this.textBoxMultiConnect.Size = new System.Drawing.Size(120, 21);
            this.textBoxMultiConnect.TabIndex = 0;
            this.textBoxMultiConnect.Text = "多通";
            // 
            // textBoxFourConnect
            // 
            this.textBoxFourConnect.Location = new System.Drawing.Point(65, 74);
            this.textBoxFourConnect.Name = "textBoxFourConnect";
            this.textBoxFourConnect.Size = new System.Drawing.Size(120, 21);
            this.textBoxFourConnect.TabIndex = 0;
            this.textBoxFourConnect.Text = "四通";
            // 
            // textBoxThreeConnect
            // 
            this.textBoxThreeConnect.Location = new System.Drawing.Point(65, 47);
            this.textBoxThreeConnect.Name = "textBoxThreeConnect";
            this.textBoxThreeConnect.Size = new System.Drawing.Size(120, 21);
            this.textBoxThreeConnect.TabIndex = 0;
            this.textBoxThreeConnect.Text = "三通";
            // 
            // textBoxStraightPnt
            // 
            this.textBoxStraightPnt.Location = new System.Drawing.Point(65, 20);
            this.textBoxStraightPnt.Name = "textBoxStraightPnt";
            this.textBoxStraightPnt.Size = new System.Drawing.Size(120, 21);
            this.textBoxStraightPnt.TabIndex = 0;
            this.textBoxStraightPnt.Text = "直线";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.splitContainer1);
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Location = new System.Drawing.Point(217, 96);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 215);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "属性域设置 - 字段标准化检查";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxFields);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxValues);
            this.splitContainer1.Size = new System.Drawing.Size(274, 160);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBoxFields
            // 
            this.listBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Location = new System.Drawing.Point(0, 0);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(128, 160);
            this.listBoxFields.TabIndex = 0;
            this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
            // 
            // richTextBoxValues
            // 
            this.richTextBoxValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxValues.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxValues.Name = "richTextBoxValues";
            this.richTextBoxValues.Size = new System.Drawing.Size(142, 160);
            this.richTextBoxValues.TabIndex = 0;
            this.richTextBoxValues.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(5, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(60, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(50, 23);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(221, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmDataCheckConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 318);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmDataCheckConfig";
            this.Text = "数据检查设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDataCheckConfig_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSurfaceTolerance)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupElevationCheckType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompareLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompareRadius)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numElevationTolerance;
        private System.Windows.Forms.NumericUpDown numSurfaceTolerance;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCompareLimit;
        private System.Windows.Forms.NumericUpDown numCompareRadius;
        private DevExpress.XtraEditors.RadioGroup radioGroupElevationCheckType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNX;
        private System.Windows.Forms.TextBox textBoxZX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMultiConnect;
        private System.Windows.Forms.TextBox textBoxFourConnect;
        private System.Windows.Forms.TextBox textBoxThreeConnect;
        private System.Windows.Forms.TextBox textBoxStraightPnt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.RichTextBox richTextBoxValues;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
    }
}
namespace Yutai.Pipeline3D.Forms
{
    partial class FrmValueSelect
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
            this.listBoxFsw = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxSelFsw = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectSingle = new System.Windows.Forms.Button();
            this.btnUnSelectSingle = new System.Windows.Forms.Button();
            this.btnUnSelectAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxFsw);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 317);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "附属物";
            // 
            // listBoxFsw
            // 
            this.listBoxFsw.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxFsw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxFsw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFsw.FormattingEnabled = true;
            this.listBoxFsw.ItemHeight = 12;
            this.listBoxFsw.Location = new System.Drawing.Point(3, 17);
            this.listBoxFsw.Name = "listBoxFsw";
            this.listBoxFsw.Size = new System.Drawing.Size(144, 297);
            this.listBoxFsw.TabIndex = 0;
            this.listBoxFsw.DoubleClick += new System.EventHandler(this.listBoxFsw_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxSelFsw);
            this.groupBox2.Location = new System.Drawing.Point(206, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 317);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已选附属物";
            // 
            // listBoxSelFsw
            // 
            this.listBoxSelFsw.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSelFsw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxSelFsw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSelFsw.FormattingEnabled = true;
            this.listBoxSelFsw.ItemHeight = 12;
            this.listBoxSelFsw.Location = new System.Drawing.Point(3, 17);
            this.listBoxSelFsw.Name = "listBoxSelFsw";
            this.listBoxSelFsw.Size = new System.Drawing.Size(144, 297);
            this.listBoxSelFsw.TabIndex = 0;
            this.listBoxSelFsw.DoubleClick += new System.EventHandler(this.listBoxSelFsw_DoubleClick);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(172, 29);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(24, 24);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "》";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectSingle
            // 
            this.btnSelectSingle.Location = new System.Drawing.Point(172, 59);
            this.btnSelectSingle.Name = "btnSelectSingle";
            this.btnSelectSingle.Size = new System.Drawing.Size(24, 24);
            this.btnSelectSingle.TabIndex = 1;
            this.btnSelectSingle.Text = ">";
            this.btnSelectSingle.UseVisualStyleBackColor = true;
            this.btnSelectSingle.Click += new System.EventHandler(this.btnSelectSingle_Click);
            // 
            // btnUnSelectSingle
            // 
            this.btnUnSelectSingle.Location = new System.Drawing.Point(172, 89);
            this.btnUnSelectSingle.Name = "btnUnSelectSingle";
            this.btnUnSelectSingle.Size = new System.Drawing.Size(24, 24);
            this.btnUnSelectSingle.TabIndex = 1;
            this.btnUnSelectSingle.Text = "<";
            this.btnUnSelectSingle.UseVisualStyleBackColor = true;
            this.btnUnSelectSingle.Click += new System.EventHandler(this.btnUnSelectSingle_Click);
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(172, 119);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(24, 24);
            this.btnUnSelectAll.TabIndex = 1;
            this.btnUnSelectAll.Text = "《";
            this.btnUnSelectAll.UseVisualStyleBackColor = true;
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(281, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(200, 360);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FrmValueSelect
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(368, 395);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUnSelectAll);
            this.Controls.Add(this.btnSelectSingle);
            this.Controls.Add(this.btnUnSelectSingle);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmValueSelect";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxFsw;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxSelFsw;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectSingle;
        private System.Windows.Forms.Button btnUnSelectSingle;
        private System.Windows.Forms.Button btnUnSelectAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}
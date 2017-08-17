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
            this.numSurfaceTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numElevationTolerance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSurfaceTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationTolerance)).BeginInit();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "高程容差";
            // 
            // FrmDataCheckConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 96);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmDataCheckConfig";
            this.Text = "数据检查设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSurfaceTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElevationTolerance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numElevationTolerance;
        private System.Windows.Forms.NumericUpDown numSurfaceTolerance;
    }
}
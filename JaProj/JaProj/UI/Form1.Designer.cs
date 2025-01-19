using System;

namespace JaProj
{
    partial class Form1
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
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pictureBoxO = new System.Windows.Forms.PictureBox();
            this.btnProces = new System.Windows.Forms.Button();
            this.pictureBoxHis = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.radioButton_ASM = new System.Windows.Forms.RadioButton();
            this.radioButton_Cpp = new System.Windows.Forms.RadioButton();
            this.pictureBoxHisSharp = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.threadsBar = new System.Windows.Forms.TrackBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHisSharp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(39, 22);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(131, 26);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Load image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // pictureBoxO
            // 
            this.pictureBoxO.Location = new System.Drawing.Point(216, 12);
            this.pictureBoxO.Name = "pictureBoxO";
            this.pictureBoxO.Size = new System.Drawing.Size(681, 450);
            this.pictureBoxO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxO.TabIndex = 1;
            this.pictureBoxO.TabStop = false;
            // 
            // btnProces
            // 
            this.btnProces.Location = new System.Drawing.Point(39, 336);
            this.btnProces.Name = "btnProces";
            this.btnProces.Size = new System.Drawing.Size(131, 28);
            this.btnProces.TabIndex = 2;
            this.btnProces.Text = "Print Histogram";
            this.btnProces.UseVisualStyleBackColor = true;
            this.btnProces.Click += new System.EventHandler(this.btnProces_Click);
            // 
            // pictureBoxHis
            // 
            this.pictureBoxHis.Location = new System.Drawing.Point(399, 524);
            this.pictureBoxHis.Name = "pictureBoxHis";
            this.pictureBoxHis.Size = new System.Drawing.Size(256, 121);
            this.pictureBoxHis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHis.TabIndex = 3;
            this.pictureBoxHis.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(903, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(681, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Sharpen image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(51, 175);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(51, 20);
            this.threadsLabel.TabIndex = 10;
            this.threadsLabel.Text = "label1";
            // 
            // radioButton_ASM
            // 
            this.radioButton_ASM.AutoSize = true;
            this.radioButton_ASM.Location = new System.Drawing.Point(77, 219);
            this.radioButton_ASM.Name = "radioButton_ASM";
            this.radioButton_ASM.Size = new System.Drawing.Size(69, 24);
            this.radioButton_ASM.TabIndex = 11;
            this.radioButton_ASM.Text = "ASM";
            this.radioButton_ASM.UseVisualStyleBackColor = true;
            this.radioButton_ASM.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton_Cpp
            // 
            this.radioButton_Cpp.AutoSize = true;
            this.radioButton_Cpp.Location = new System.Drawing.Point(77, 249);
            this.radioButton_Cpp.Name = "radioButton_Cpp";
            this.radioButton_Cpp.Size = new System.Drawing.Size(63, 24);
            this.radioButton_Cpp.TabIndex = 12;
            this.radioButton_Cpp.Text = "C++";
            this.radioButton_Cpp.UseVisualStyleBackColor = true;
            this.radioButton_Cpp.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // pictureBoxHisSharp
            // 
            this.pictureBoxHisSharp.Location = new System.Drawing.Point(1131, 524);
            this.pictureBoxHisSharp.Name = "pictureBoxHisSharp";
            this.pictureBoxHisSharp.Size = new System.Drawing.Size(256, 121);
            this.pictureBoxHisSharp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHisSharp.TabIndex = 13;
            this.pictureBoxHisSharp.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(823, 468);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(154, 38);
            this.progressBar1.TabIndex = 14;
            // 
            // threadsBar
            // 
            this.threadsBar.AccessibleDescription = "";
            this.threadsBar.AccessibleName = "";
            this.threadsBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.threadsBar.LargeChange = 1;
            this.threadsBar.Location = new System.Drawing.Point(12, 126);
            this.threadsBar.Maximum = 6;
            this.threadsBar.Name = "threadsBar";
            this.threadsBar.Size = new System.Drawing.Size(198, 69);
            this.threadsBar.TabIndex = 9;
            this.threadsBar.ValueChanged += new System.EventHandler(this.threadsBar_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(108, 173);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 26);
            this.numericUpDown1.TabIndex = 15;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1598, 772);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBoxHisSharp);
            this.Controls.Add(this.radioButton_Cpp);
            this.Controls.Add(this.radioButton_ASM);
            this.Controls.Add(this.threadsLabel);
            this.Controls.Add(this.threadsBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxHis);
            this.Controls.Add(this.btnProces);
            this.Controls.Add(this.pictureBoxO);
            this.Controls.Add(this.btnLoadImage);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHisSharp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox pictureBoxO;
        private System.Windows.Forms.Button btnProces;
        private System.Windows.Forms.PictureBox pictureBoxHis;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label threadsLabel;
        private System.Windows.Forms.RadioButton radioButton_ASM;
        private System.Windows.Forms.RadioButton radioButton_Cpp;
        private System.Windows.Forms.PictureBox pictureBoxHisSharp;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TrackBar threadsBar;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
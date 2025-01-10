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
            this.pictureBoxRed = new System.Windows.Forms.PictureBox();
            this.pictureBoxGreen = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlue = new System.Windows.Forms.PictureBox();
            this.pictureBoxLuma = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.threadsBar = new System.Windows.Forms.TrackBar();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.radioButton_ASM = new System.Windows.Forms.RadioButton();
            this.radioButton_Cpp = new System.Windows.Forms.RadioButton();
            this.pictureBoxLuma2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlue2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxGreen2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRed2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLuma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLuma2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed2)).BeginInit();
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
            this.pictureBoxO.Size = new System.Drawing.Size(827, 539);
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
            // pictureBoxRed
            // 
            this.pictureBoxRed.Location = new System.Drawing.Point(515, 580);
            this.pictureBoxRed.Name = "pictureBoxRed";
            this.pictureBoxRed.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRed.TabIndex = 3;
            this.pictureBoxRed.TabStop = false;
            // 
            // pictureBoxGreen
            // 
            this.pictureBoxGreen.Location = new System.Drawing.Point(515, 709);
            this.pictureBoxGreen.Name = "pictureBoxGreen";
            this.pictureBoxGreen.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGreen.TabIndex = 4;
            this.pictureBoxGreen.TabStop = false;
            // 
            // pictureBoxBlue
            // 
            this.pictureBoxBlue.Location = new System.Drawing.Point(671, 580);
            this.pictureBoxBlue.Name = "pictureBoxBlue";
            this.pictureBoxBlue.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxBlue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBlue.TabIndex = 5;
            this.pictureBoxBlue.TabStop = false;
            // 
            // pictureBoxLuma
            // 
            this.pictureBoxLuma.Location = new System.Drawing.Point(671, 709);
            this.pictureBoxLuma.Name = "pictureBoxLuma";
            this.pictureBoxLuma.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxLuma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLuma.TabIndex = 6;
            this.pictureBoxLuma.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1065, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(827, 539);
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
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(80, 175);
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
            // pictureBoxLuma2
            // 
            this.pictureBoxLuma2.Location = new System.Drawing.Point(1553, 709);
            this.pictureBoxLuma2.Name = "pictureBoxLuma2";
            this.pictureBoxLuma2.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxLuma2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLuma2.TabIndex = 16;
            this.pictureBoxLuma2.TabStop = false;
            // 
            // pictureBoxBlue2
            // 
            this.pictureBoxBlue2.Location = new System.Drawing.Point(1553, 580);
            this.pictureBoxBlue2.Name = "pictureBoxBlue2";
            this.pictureBoxBlue2.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxBlue2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBlue2.TabIndex = 15;
            this.pictureBoxBlue2.TabStop = false;
            // 
            // pictureBoxGreen2
            // 
            this.pictureBoxGreen2.Location = new System.Drawing.Point(1397, 709);
            this.pictureBoxGreen2.Name = "pictureBoxGreen2";
            this.pictureBoxGreen2.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxGreen2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGreen2.TabIndex = 14;
            this.pictureBoxGreen2.TabStop = false;
            // 
            // pictureBoxRed2
            // 
            this.pictureBoxRed2.Location = new System.Drawing.Point(1397, 580);
            this.pictureBoxRed2.Name = "pictureBoxRed2";
            this.pictureBoxRed2.Size = new System.Drawing.Size(110, 96);
            this.pictureBoxRed2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRed2.TabIndex = 13;
            this.pictureBoxRed2.TabStop = false;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.pictureBoxLuma2);
            this.Controls.Add(this.pictureBoxBlue2);
            this.Controls.Add(this.pictureBoxGreen2);
            this.Controls.Add(this.pictureBoxRed2);
            this.Controls.Add(this.radioButton_Cpp);
            this.Controls.Add(this.radioButton_ASM);
            this.Controls.Add(this.threadsLabel);
            this.Controls.Add(this.threadsBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxLuma);
            this.Controls.Add(this.pictureBoxBlue);
            this.Controls.Add(this.pictureBoxGreen);
            this.Controls.Add(this.pictureBoxRed);
            this.Controls.Add(this.btnProces);
            this.Controls.Add(this.pictureBoxO);
            this.Controls.Add(this.btnLoadImage);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLuma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLuma2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox pictureBoxO;
        private System.Windows.Forms.Button btnProces;
        private System.Windows.Forms.PictureBox pictureBoxRed;
        private System.Windows.Forms.PictureBox pictureBoxGreen;
        private System.Windows.Forms.PictureBox pictureBoxBlue;
        private System.Windows.Forms.PictureBox pictureBoxLuma;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar threadsBar;
        private System.Windows.Forms.Label threadsLabel;
        private System.Windows.Forms.RadioButton radioButton_ASM;
        private System.Windows.Forms.RadioButton radioButton_Cpp;
        private System.Windows.Forms.PictureBox pictureBoxLuma2;
        private System.Windows.Forms.PictureBox pictureBoxBlue2;
        private System.Windows.Forms.PictureBox pictureBoxGreen2;
        private System.Windows.Forms.PictureBox pictureBoxRed2;
    }
}


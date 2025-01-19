// Version 1.0

// Update:
// Better comments for asm

using JaProj.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JaProj
{
    //Lib to choose
    public enum ProcessingLib
    {
        None,
        ASM,
        CPP
    }
    //The Form1 class represents the main window of the application.
    public partial class Form1 : Form
    {

        // Numer of Threads
        private int threadCount;
        // Threads to choice
        int[] threadOptions = { 1, 2, 4, 8, 16, 32, 64 };
        // Bitmap to store the currently loaded image
        private Bitmap currentBitmap;
        // Bitmap after convert
        private Bitmap convertedBitmap;

        private ProcessingLib activeLib = ProcessingLib.None;
        // Constructor for the form, initializes the components
        public Form1()
        {
            InitializeComponent();
            threadCount = Environment.ProcessorCount;
            threadsLabel.Text = "Threads: ";
            int defaultIndex = Array.IndexOf(threadOptions, threadCount);
            threadsBar.Value = defaultIndex;
            numericUpDown1.Value = threadCount;
            progressBar1.Visible = false;
        }

        //"Load Image" button click event.
        //Opens a file dialog for the user to select an image.
        //If a valid image is selected, it loads and displays it in pictureBoxO.
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Images (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.Title = "Select an Image";
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                string folderPath = Path.GetFullPath(Path.Combine(executablePath, "../../../Images"));
                openFileDialog.InitialDirectory = folderPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load the selected image
                        string filePath = openFileDialog.FileName;
                        currentBitmap = new Bitmap(filePath);
                        pictureBoxO.Image = currentBitmap;

                        pictureBox1.Image = null;
                        convertedBitmap = null;
                        pictureBoxHis.Visible = false;
                        pictureBoxHisSharp.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        // Display an error message
                        MessageBox.Show("Loading image error: " + ex.Message);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Button click event.
        // Checks if an image is loaded, then calculates and displays the histograms for Red, Green, Blue, and Luma channels.
        private void btnProces_Click(object sender, EventArgs e)
        {
            if (currentBitmap != null && convertedBitmap != null)
            {
                pictureBoxHis.Visible = true;
                pictureBoxHisSharp.Visible = true;
                Histogram ogHistogram = new Histogram(currentBitmap);
                ogHistogram.printHis(pictureBoxHis);

                Histogram convertHistogram = new Histogram(convertedBitmap);
                convertHistogram.printHis(pictureBoxHisSharp);
            }
            else
            {
                // Display an error message
                MessageBox.Show("First convert your image");
            }
        }

        // Button click event.
        // Checks if an image is loaded, then sharpen image using c++ lib
        private async void button1_Click(object sender, EventArgs e)
        {
            if (currentBitmap != null)
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = threadCount;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                progressBar1.Visible = true;
                await Task.Run(() =>
                {

                    if (activeLib == ProcessingLib.ASM)
                    {
                        ASMSharpening processor = new ASMSharpening();
                        convertedBitmap = processor.sharpenByASM(currentBitmap, threadCount, pictureBox1, progressBar1);
                    }
                    else if (activeLib == ProcessingLib.CPP)
                    {
                        CppSharpening processor = new CppSharpening();
                        convertedBitmap = processor.sharpenByCpp(currentBitmap, threadCount, pictureBox1, progressBar1);
                    }
                    else
                    {
                        MessageBox.Show("Select ASM or C++");
                    }
                });
                progressBar1.Visible = false;
            }
            else
            {
                MessageBox.Show("Load the image first.");
            }
        }

        // Track ValueChanged Bar event.
        // Sets the number of threads
        private void threadsBar_ValueChanged(object sender, EventArgs e)
        {
            threadCount = threadOptions[threadsBar.Value];
            numericUpDown1.Value = threadCount;
        }
        // Track CheckedChanged radio button event.
        // Sets active lib on ASM
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            activeLib = ProcessingLib.ASM;
        }
        //Track CheckedChanged radio button event.
        // Sets active lib on C++
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            activeLib = ProcessingLib.CPP;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            threadCount = (int)numericUpDown1.Value;
        }
    }
}
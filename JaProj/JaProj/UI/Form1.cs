// Version 0.5

// Update:
// Fixing bugs
// Improved asm library


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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JaProj
{
    //The `Form1` class represents the main window of the application.
    public partial class Form1 : Form
    {

        // Numer of Threads
        private int threadCount;
        // Threads to choice
        int[] threadOptions = { 1, 2, 4, 8, 16, 32, 64 };
        // Bitmap to store the currently loaded image
        private Bitmap currentBitmap;
        //

        private Bitmap convertedBitmap;

        private string activeLib;
        // Constructor for the form, initializes the components
        public Form1()
        {
            InitializeComponent();
            threadCount = Environment.ProcessorCount;
            threadsLabel.Text = $"Threads: {threadCount}";
            int defaultIndex = Array.IndexOf(threadOptions, threadCount);
            threadsBar.Value = defaultIndex;
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
                Histogram ogHistogram = new Histogram(currentBitmap);
                ogHistogram.printHis(pictureBoxRed, "r");
                ogHistogram.printHis(pictureBoxGreen, "g");
                ogHistogram.printHis(pictureBoxBlue, "b");
                ogHistogram.printHis(pictureBoxLuma, "l");

                Histogram convertHistogram = new Histogram(convertedBitmap);
                convertHistogram.printHis(pictureBoxRed2, "r");
                convertHistogram.printHis(pictureBoxGreen2, "g");
                convertHistogram.printHis(pictureBoxBlue2, "b");
                convertHistogram.printHis(pictureBoxLuma2, "l");
            }
            else
            {
                // Display an error message
                MessageBox.Show("First convert your image");
            }
        }

        // Button click event.
        // Checks if an image is loaded, then sharpen image using c++ lib
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentBitmap != null)
            {
                if (activeLib == "CPP")
                {
                    CppSharpening processor = new CppSharpening();
                    processor.sharpenByCpp(currentBitmap, threadCount, pictureBox1);
                }
                else if (activeLib == "ASM")
                {
                    ASMSharpening processor = new ASMSharpening();
                    convertedBitmap = processor.sharpenByASM(currentBitmap, threadCount, pictureBox1);
                }
                else
                {
                    MessageBox.Show("Select ASM or C++");
                }
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
            threadsLabel.Text = $"Threads: {threadCount}";
        }
        // Track CheckedChanged radio button event.
        // Sets active lib on ASM
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.activeLib = "ASM";
        }
        // Track CheckedChanged radio button event.
        // Sets active lib on C++
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.activeLib = "CPP";
        }
    }
}
// Version 0.1

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JaProj
{
    public partial class Form1 : Form
    {
        //import asm lib
        //[dllimport(@"..\x64\debug\jaasm.dll")]
        //static extern int myproc1(int a, int b);

        //import cpp lib
        //[dllimport(@"..\x64\debug\jacpp.dll")]
        //static extern int multiply(int a, int b);


        // Bitmap to store the currently loaded image
        private Bitmap currentBitmap;
        // Arrays to store color histograms for each channel
        private int[] redHistogram = new int[256], greenHistogram = new int[256], blueHistogram = new int[256], lumaHistogram = new int[256];

        // Constructor for the form, initializes the components
        public Form1()
        {
            InitializeComponent();
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
                        MessageBox.Show("Błąd podczas otwierania obrazu: " + ex.Message);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //"Process" button click event.
        // Checks if an image is loaded, then calculates and displays the histograms for Red, Green, Blue, and Luma channels.
        private void btnProces_Click(object sender, EventArgs e)
        {
            if (currentBitmap != null)
            {
                // Calculate and display histograms
                histogram(currentBitmap);
                printHistogram(redHistogram, pictureBoxRed, Color.Red);
                printHistogram(greenHistogram, pictureBoxGreen, Color.Green);
                printHistogram(blueHistogram, pictureBoxBlue, Color.Blue);
                printHistogram(lumaHistogram, pictureBoxLuma, Color.DarkGray);

            }
            else
            {
                // Display an error message
                MessageBox.Show("Proszę najpierw wczytać obraz.");
            }
        }

        //Calculates the histograms for each color channel.
        //The histogram values are stored in the arrays.
        private void histogram(Bitmap bitmap)
        {
            Array.Clear(redHistogram, 0, redHistogram.Length);
            Array.Clear(greenHistogram, 0, greenHistogram.Length);
            Array.Clear(blueHistogram, 0, blueHistogram.Length);
            Array.Clear(lumaHistogram, 0, lumaHistogram.Length);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for(int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    redHistogram[pixelColor.R]++;
                    greenHistogram[pixelColor.G]++;
                    blueHistogram[pixelColor.B]++;
                    double luma = pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114;
                    int lumaIndex = (int)Math.Round(luma);
                    lumaIndex = Math.Min(255, Math.Max(0, lumaIndex));
                    lumaHistogram[lumaIndex]++;
                }
            }
        }

        //Renders the histogram data as a vertical bar chart in the specified PictureBox.
        private void printHistogram(int[] histogram, PictureBox pictureBox, Color color)
        {
            Bitmap histogramBit = new Bitmap(pictureBox.Width, pictureBox.Height);

            using (Graphics g = Graphics.FromImage(histogramBit))
            {
                g.Clear(Color.White);
                int max = histogram.Max();

                for(int i = 0; i < histogram.Length; i++)
                {
                    int barHeight = (int)(((double)histogram[i] / max) * pictureBox.Height);
                    g.DrawLine(new Pen(color), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - barHeight));
                }
            }
            pictureBox.Image = histogramBit;
        }
    }
}
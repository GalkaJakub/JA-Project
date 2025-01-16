using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JaProj.Processing
{
    /*
    The `Histogram` class calculates histograms for image color channels (RGB) and brightness.
    It is used to visualize the distribution of colors in an image.
    */
    internal class Histogram
    {
        // Bitmap to create histogram
        private Bitmap currentImage;
        // Arrays to store color histograms for each channel
        private int[] redHis, greenHis, blueHis, lumaHis;

        public Histogram(Bitmap image)
        {
            this.currentImage = image;
            this.redHis = new int[256];
            this.greenHis = new int[256];
            this.blueHis = new int[256];
            this.lumaHis = new int[256];
            createHistogram();
        }


        //Calculates the histograms for each color channel.
        //The histogram values are stored in the arrays.
        private void createHistogram()
        {
            for (int y = 0; y < currentImage.Height; y++)
            {
                for (int x = 0; x < currentImage.Width; x++)
                {
                    Color pixelColor = currentImage.GetPixel(x, y);
                    redHis[pixelColor.R]++;
                    greenHis[pixelColor.G]++;
                    blueHis[pixelColor.B]++;
                    double luma = pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114;
                    int lumaIndex = (int)Math.Round(luma);
                    lumaIndex = Math.Min(255, Math.Max(0, lumaIndex));
                    lumaHis[lumaIndex]++;
                }
            }
        }
        // Renders all histograms (RGB and luma) on a single image.
        public void printHis(PictureBox pictureBox)
        {
            Bitmap histogramBit = new Bitmap(pictureBox.Width, pictureBox.Height);

            // Determine maximum values across all histograms
            int maxRed = redHis.Max();
            int maxGreen = greenHis.Max();
            int maxBlue = blueHis.Max();
            int maxLuma = lumaHis.Max();
            int globalMax = Math.Max(Math.Max(maxRed, maxGreen), Math.Max(maxBlue, maxLuma));

            using (Graphics g = Graphics.FromImage(histogramBit))
            {
                g.Clear(Color.White);

                for (int i = 0; i < 256; i++)
                {
                    // Calculate bar heights relative to the picture box height
                    int redHeight = (int)(((double)redHis[i] / globalMax) * pictureBox.Height);
                    int greenHeight = (int)(((double)greenHis[i] / globalMax) * pictureBox.Height);
                    int blueHeight = (int)(((double)blueHis[i] / globalMax) * pictureBox.Height);
                    int lumaHeight = (int)(((double)lumaHis[i] / globalMax) * pictureBox.Height);

                    // Draw lines for each channel
                    g.DrawLine(new Pen(Color.FromArgb(128, Color.DarkGray), 1.5f), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - lumaHeight));
                    g.DrawLine(new Pen(Color.FromArgb(128, Color.Red), 1.5f), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - redHeight));
                    g.DrawLine(new Pen(Color.FromArgb(128, Color.Green), 1.5f), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - greenHeight));
                    g.DrawLine(new Pen(Color.FromArgb(128, Color.Blue), 1.5f), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - blueHeight));
                }
            }

            // Display the combined histogram in the PictureBox
            pictureBox.Image = histogramBit;
        }

    }
}

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

        //Renders the histogram data as a vertical bar chart in the specified PictureBox.
        public void printHis(PictureBox pictureBox, string hisColor)
        {
            Bitmap histogramBit = new Bitmap(pictureBox.Width, pictureBox.Height);
            int[] histogram;
            Color color;

            switch (hisColor)
            {
                case "r":
                    histogram = redHis;
                    color = Color.Red;
                    break;
                case "g":
                    histogram = greenHis;
                    color = Color.Green;
                    break;
                case "b":
                    histogram = blueHis;
                    color = Color.Blue;
                    break;
                case "l":
                    histogram = lumaHis;
                    color = Color.DarkGray;
                    break;
                default:
                    throw new ArgumentException("wrong color");
            }

            using (Graphics g = Graphics.FromImage(histogramBit))
            {
                g.Clear(Color.White);
                int max = histogram.Max();

                for (int i = 0; i < histogram.Length; i++)
                {
                    int barHeight = (int)(((double)histogram[i] / max) * pictureBox.Height);
                    g.DrawLine(new Pen(color), new Point(i, pictureBox.Height), new Point(i, pictureBox.Height - barHeight));
                }
            }

            pictureBox.Image = histogramBit;
        }
    }
}

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace JaProj.Processing
{
    public class CppSharpening
    {
        // Import the C++ function from the DLL
        [DllImport(@"C:\Users\jakub\JA-Project\JaProj\x64\Debug\JACpp.dll")]
        static extern void ImageSharpening(byte[] data, int width, int height, int stride);

        // Method to sharpen an image using the C++ DLL function in multiple threads
        public void sharpenByCpp(Bitmap bitmap, PictureBox pictureBox, int threadCount)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int width = bitmap.Width;
            int height = bitmap.Height;
            int stride = bmpData.Stride;

            // Calculate the number of bytes required and copy pixel data
            int bytes = Math.Abs(stride) * height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            // Divide the image into fragments
            int rowsPerThread = height / threadCount;
            int remainder = height % threadCount;

            Thread[] threads = new Thread[threadCount];
            MessageBox.Show(threads.Length.ToString());
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // Each thread work on a separate part of the image
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = t * rowsPerThread;
                int endRow = (t == threadCount - 1) ? (startRow + rowsPerThread + remainder) : (startRow + rowsPerThread);

                threads[t] = new Thread(() =>
                {
                    // Number of rows in this fragment
                    int localHeight = endRow - startRow;

                    // Create a buffer for the local fragment
                    byte[] localData = new byte[localHeight * stride];

                    // Copy the appropriate fragment from rgbValues
                    Buffer.BlockCopy(rgbValues, startRow * stride, localData, 0, localHeight * stride);

                    // Call the filter on the local fragment
                    ImageSharpening(localData, width, localHeight, stride);

                    // Copy the data back to rgbValues
                    Buffer.BlockCopy(localData, 0, rgbValues, startRow * stride, localHeight * stride);
                });

                threads[t].Start();
            }

            // Wait for all threads to finish
            foreach (var thread in threads)
            {
                thread.Join();
            }
            timer.Stop();

            // Copy the modified pixels back to the bitmap
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bitmap.UnlockBits(bmpData);

            // Update the PictureBox with the modified image
            pictureBox.Image = bitmap;
            // Show time
            MessageBox.Show($"Execution time (assembler): {timer.ElapsedMilliseconds} ms");

            // Save the file
            string outputPath = @"C:\Users\jakub\JA-Project\JaProj\test1.jpg";
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }
}

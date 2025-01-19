using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace JaProj.Processing
{
    public class CppSharpening
    {
        // Import the C++ function from the DLL
        [DllImport("libs/JACpp.dll")]
        static extern void ImageSharpening(byte[] data, byte[] outData, int width, int height, int stride);

        // Method to sharpen an image using the C++ DLL function in multiple threads
        public Bitmap sharpenByCpp(Bitmap loadedBitmap, int threadCount, PictureBox pictureBox, ProgressBar progressBar)
        {

            // Create a copy of the loaded image
            Bitmap bitmap = new Bitmap(loadedBitmap);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

            int width = bitmap.Width;
            int height = bitmap.Height;
            int stride = bmpData.Stride;
            int pixelSize = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            // Calculate the number of bytes required and copy pixel data
            int bytes = Math.Abs(stride) * height;
            // Allocate two arrays: data (original) and outData (for temporary processing)
            byte[] data = new byte[bytes];
            byte[] outData = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, data, 0, bytes);

            // Save the edges to preserve boundary pixels
            SaveEdges(data, width, height, stride, pixelSize,out byte[] topRow, out byte[] bottomRow, out byte[] leftColumn, out byte[] rightColumn);

            // Determine how many rows each thread should process
            int rowsPerThread = height / threadCount;
            Thread[] threads = new Thread[threadCount];
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int currentRow = 0;
            // Create and start each thread
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = currentRow;
                // Assign rows to threads; last thread may process extra rows
                int endRow = (t == threadCount - 1) ? (height) : (startRow + rowsPerThread);

                currentRow = endRow;

                threads[t] = new Thread(() =>
                {
                    // Calculate how many rows thread will process
                    int usedHeight = endRow - startRow;

                    // Overlap for neighbors
                    int localStart = Math.Max(0, startRow - 1);
                    int localEnd = Math.Min(height, endRow + 1);
                    int localHeight = localEnd - localStart;

                    // Allocate local arrays for data processing
                    byte[] localData = new byte[localHeight * stride];
                    byte[] localOutData = new byte[localHeight * stride];

                    // Copy part of global data to local input buffer
                    Buffer.BlockCopy(data, localStart * stride, localData, 0, localHeight * stride);
                    // Call the C++ sharpening function on local data
                    ImageSharpening(localData, localOutData, width, localHeight, stride);
                    // Determine where to copy the processed data back into the global array
                    int outCopyOffset = (startRow - localStart) * stride;
                    int outCopySize = usedHeight * stride;
                    // Copy processed data from local output buffer back to main data array
                    Buffer.BlockCopy(localOutData, outCopyOffset, outData, startRow * stride, outCopySize);

                    // Update progress bar
                    if (progressBar.InvokeRequired)
                    {
                        progressBar.Invoke((MethodInvoker)(() => progressBar.PerformStep()));
                    }
                    else
                    {
                        progressBar.PerformStep();
                    }
                });
                // Start the thread
                threads[t].Start();
            }
            // Wait for all threads to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }
            timer.Stop();

            // Copy processed global `outData` back to `data`
            Buffer.BlockCopy(outData, 0, data, 0, data.Length);

            // Restore the saved edges
            RestoreEdges(data, width, height, stride, pixelSize, topRow, bottomRow, leftColumn, rightColumn);

            // Copy the modified pixels back to the bitmap
            Marshal.Copy(data, 0, bmpData.Scan0, bytes);
            bitmap.UnlockBits(bmpData);

            // Set new pictureBox
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke((MethodInvoker)(() => pictureBox.Image = bitmap));
            }
            else
            {
                pictureBox.Image = bitmap;
            }

            // Show info
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke((MethodInvoker)(() =>
                    MessageBox.Show($"Execution time (cpp): {timer.ElapsedMilliseconds} ms\nThreads: {threadCount}\nImage saved in 'Results' folder")));
            }
            else
            {
                MessageBox.Show($"Execution time (cpp): {timer.ElapsedMilliseconds} ms\nThreads: {threadCount}\nImage saved in 'Results' folder");
            }

            // Save the file current date
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM HH-mm-s");

            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.GetFullPath(Path.Combine(executablePath, $"../../../Results/cpp {date}.bmp"));
            bitmap.Save(folderPath, ImageFormat.Bmp);

            return bitmap;
        }

        // Method to save the edges to keep boundary pixels unchanged.
        private void SaveEdges(byte[] data, int width, int height, int stride, int pixelSize, out byte[] topRow,
            out byte[] bottomRow,out byte[] leftColumn, out byte[] rightColumn)
        {
            topRow = new byte[stride];
            bottomRow = new byte[stride];
            leftColumn = new byte[height * pixelSize];
            rightColumn = new byte[height * pixelSize];

            // Copy the first and last row
            Buffer.BlockCopy(data, 0, topRow, 0, stride);
            Buffer.BlockCopy(data, (height - 1) * stride, bottomRow, 0, stride);

            // Copy the first and last column for each row
            for (int y = 0; y < height; y++)
            {
                Buffer.BlockCopy(data, y * stride, leftColumn, y * pixelSize, pixelSize);
                Buffer.BlockCopy(data, y * stride + (width - 1) * pixelSize, rightColumn, y * pixelSize, pixelSize);
            }
        }

        // Method to restor the saved edges after filtering.
        private void RestoreEdges(byte[] data, int width, int height, int stride, int pixelSize, byte[] topRow,
            byte[] bottomRow, byte[] leftColumn, byte[] rightColumn)
        {
            // Restore the first and last row
            Buffer.BlockCopy(topRow, 0, data, 0, stride);
            Buffer.BlockCopy(bottomRow, 0, data, (height - 1) * stride, stride);

            // Restore the first and last column for each row
            for (int y = 0; y < height; y++)
            {
                Buffer.BlockCopy(leftColumn, y * pixelSize, data, y * stride, pixelSize);
                Buffer.BlockCopy(rightColumn, y * pixelSize, data, y * stride + (width - 1) * pixelSize, pixelSize);
            }
        }
    }
}
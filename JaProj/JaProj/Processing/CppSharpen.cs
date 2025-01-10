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
        static extern void ImageSharpening(byte[] data, byte[] outData, int width, int height, int stride);

        // Method to sharpen an image using the C++ DLL function in multiple threads
        public void sharpenByCpp(Bitmap loadedBitmap, int threadCount, PictureBox pictureBox)
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
            int remainder = height % threadCount;
            Thread[] threads = new Thread[threadCount];
            // Show the number of threads
            MessageBox.Show($"C++ \nThreads: {threads.Length}");
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
                    Buffer.BlockCopy(localOutData, outCopyOffset,data, startRow * stride, outCopySize);
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

            // Restore the saved edges
            RestoreEdges(data, width, height, stride, pixelSize, topRow, bottomRow, leftColumn, rightColumn);

            // Copy the modified pixels back to the bitmap
            Marshal.Copy(data, 0, bmpData.Scan0, bytes);
            bitmap.UnlockBits(bmpData);

            // Update the PictureBox with the modified image
            pictureBox.Image = bitmap;
            // Show time
            MessageBox.Show($"Execution time (assembler): {timer.ElapsedMilliseconds} ms");

            // Save the file
            string outputPath = @"C:\Users\jakub\JA-Project\JaProj\test_cpp.jpg";
            bitmap.Save(outputPath, ImageFormat.Jpeg);
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


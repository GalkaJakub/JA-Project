using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JaProj.Processing
{
    public class CppSharpening
    {
        // Import the C++ function from the DLL
        [DllImport(@"C:\Users\jakub\JA-Project\JaProj\x64\Debug\JACpp.dll")]
        static extern void ImageSharpening(byte[] data, int width, int height, int stride);

        // Method to sharpen an image using the C++ DLL function
        public void sharpenByCpp(Bitmap bitmap, PictureBox pictureBox, int threadCount)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            // Calculate the number of bytes required and copy pixel data
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);
            // Call the C++ function to perform image sharpening
            ImageSharpening(rgbValues, bitmap.Width, bitmap.Height, bmpData.Stride);
            // Copy modified pixel data back to the bitmap
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bitmap.UnlockBits(bmpData);
            // Update the PictureBox with the modified image
            pictureBox.Image = bitmap;
        }
    }
}

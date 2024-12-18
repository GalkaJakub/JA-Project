using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JaProj.Processing
{
    public class ASMSharpening
    {
        [DllImport(@"C:\Users\jakub\JA-Project\JaProj\x64\Debug\JAAsm.dll")]
        static extern void ASMSharpen(byte[] output, byte[] input, int width, int height, int stride);
        // Method to sharpen an image using the ASM DLL function
        public void sharpenByASM(Bitmap bitmap, PictureBox pictureBox, int threadCount)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            // Calculate the number of bytes required and copy pixel data
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] inputValues = new byte[bytes];
            byte[] outputValues = new byte[bytes];

            Marshal.Copy(bmpData.Scan0, inputValues, 0, bytes);
            // Call the ASM function to perform image sharpening
            ASMSharpen(outputValues, inputValues, bitmap.Width, bitmap.Height, bmpData.Stride);
            // Copy modified pixel data back to the bitmap
            Marshal.Copy(outputValues, 0, bmpData.Scan0, bytes);
            bitmap.UnlockBits(bmpData);

            // Update the PictureBox with the modified image
            pictureBox.Image = bitmap;

            string outputPath = @"C:\Users\jakub\JA-Project\JaProj\test1.jpg";
            bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}

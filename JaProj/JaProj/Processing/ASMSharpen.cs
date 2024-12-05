using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JaProj.Processing
{
    internal class ASMSharpen
    {
        [DllImport(@"C:\Users\jakub\JA-Project\JaProj\x64\Debug\JACpp.dll")]
        static extern void ImageSharpening(byte[] data, int width, int height, int stride);
    }
}

// Version 0.2

// Update:
//Better code organization
//Added selection of number of threads
//Added detects and defaults the number of threads
//Added selection of active lib (C++ or ASM)
//Added prototype of c++ function for image sharpening (requires improvements and multi-threading)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JaProj
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        ///</summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

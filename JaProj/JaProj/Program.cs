// Version 0.3

// Update:
//Added prototype of asm lib
//Improve c++ lib
//Added convert bitmap to Format24bppRgb (if requires)




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

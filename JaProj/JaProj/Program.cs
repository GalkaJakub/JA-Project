// Version 0.7

// Update:
// Fixing bugs
// Added Progress bar
// Improved historgrams

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WiiBalanceWalker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain mainForm = new FormMain();

            Application.Run(mainForm);
        }
    }
}

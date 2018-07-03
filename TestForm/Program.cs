using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Dropbox.Api;

namespace MainForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string strCmdText;
            //strCmdText = @"/C subst Z: D:\GoogleDrive";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            //strCmdText = @"/C subst Y: D:\Dropbox";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        
    }
}

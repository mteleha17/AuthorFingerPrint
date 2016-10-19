using FingerPrint.Controllers;
using FingerPrint.Controllers.Implementations;
using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var analysisController = new AnalysisController();
            var textController = new TextController();
            var groupController = new GroupController();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(analysisController, textController, groupController));
        }
    }
}

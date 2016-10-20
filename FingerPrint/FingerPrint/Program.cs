using FingerPrint.Controllers;
using FingerPrint.Controllers.Implementations;
using FingerPrint.Models;
using FingerPrint.Stores;
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
            ITextStore textStore = new TextStore();
            IGroupStore groupStore = new GroupStore();
            var analysisController = new AnalysisController();
            var textController = new TextController(textStore, groupStore);
            var groupController = new GroupController(textStore, groupStore);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(analysisController, textController, groupController));
        }
    }
}

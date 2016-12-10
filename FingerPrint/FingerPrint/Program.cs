using FingerPrint.Controllers;
using FingerPrint.Controllers.Implementations;
using FingerPrint.Models;
using FingerPrint.Models.Implementations;
using FingerPrint.Models.Interfaces.TypeInterfaces;
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
            FingerprintV8Entities db = new FingerprintV8Entities();
            var modelFactory = new ModelFactory();
            var textStore = new TextStore(db, modelFactory);
            var groupStore = new GroupStore(db, modelFactory, textStore);
            var analysisController = new AnalysisController();

            var textTempDb = new List<ITextViewModel>();
            var groupTempDb = new List<IGroupViewModel>();

            var textController = new TextController(textTempDb, groupTempDb, textStore, groupStore, modelFactory);
            var groupController = new GroupController(analysisController, textTempDb, groupTempDb, textStore, groupStore, modelFactory);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(analysisController, textController, groupController));
        }
    }
}

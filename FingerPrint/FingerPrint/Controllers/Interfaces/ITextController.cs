using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Controllers
{
    /// <summary>
    /// Interface to be implemented by a TextController, i.e., a controller that will handle
    /// the creation, deletion, updating, and fetching of texts.
    /// </summary>
    public interface ITextController
    {
        ITextViewModel GetTextByName(string name);

        List<ITextViewModel> GetAllTexts();

        void CreateText(string name, TextReader input, int length, string author = null);

        void DeleteText(string name);

        void UpdateText(string originalName, string newName = null, string author = null, bool? quotesOn = null);
    }
}

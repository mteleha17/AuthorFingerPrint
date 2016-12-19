using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    /// <summary>
    /// Interface to be implemented by a class that handles database transactions involving texts.
    /// </summary>
    public interface ITextStore : IItemStore<Text, ITextModel>
    {
        void ModifyName(ITextModel model, string newName);

        void ModifyAuthor(ITextModel model, string newAuthor);

        void ModifyIncludeQuotes(ITextModel model, bool includeQuotes);
    }
}

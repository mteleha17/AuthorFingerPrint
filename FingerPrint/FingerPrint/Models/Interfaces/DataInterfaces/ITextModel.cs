using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface to be implemented by a class respresenting a text.
    /// </summary>
    public interface ITextModel : ITextOrGroupModel, ITextViewModel
    {
        void SetAuthor(string author);
        void SetIncludeQuotes(bool value);
        ISingleWordCountModel GetCountsWithQuotes();
        ISingleWordCountModel GetCountsWithoutQuotes();
    }
}

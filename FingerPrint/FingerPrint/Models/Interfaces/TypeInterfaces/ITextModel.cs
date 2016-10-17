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
    /// <typeparam name="SingleCountType"></typeparam>
    public interface ITextModel<SingleCountType> : ITextOrGroupModel<SingleCountType>, ITextViewModel<SingleCountType>
    {
        void SetAuthor(string author);
        void SetIncludeQuotes(bool value);
    }
}

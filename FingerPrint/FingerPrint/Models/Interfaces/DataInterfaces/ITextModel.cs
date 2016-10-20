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
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface ITextModel<SingleCountType> : ITextOrGroupModel<SingleCountType>, ITextViewModel<SingleCountType>
    {
        void SetAuthor(string author);
        void SetIncludeQuotes(bool value);
        SingleCountType GetCountsWithQuotes();
        SingleCountType GetCountsWithoutQuotes();
    }
}

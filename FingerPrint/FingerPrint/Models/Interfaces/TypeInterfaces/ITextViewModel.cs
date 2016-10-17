using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface ITextViewModel<SingleCountType> : ITextOrGroupViewModel<SingleCountType>
    {
        string GetAuthor();
        bool GetIncludeQuotes();
    }
}

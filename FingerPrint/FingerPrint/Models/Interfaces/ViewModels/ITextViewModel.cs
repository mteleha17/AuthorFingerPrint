using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface representing the methods of a text not concerned with mutation (getters not setters). 
    /// </summary>
    public interface ITextViewModel : ITextOrGroupViewModel
    {
        string GetAuthor();
        bool GetIncludeQuotes();
    }
}

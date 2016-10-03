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
    public interface ITextModel<SingleCountType> : ITextOrGroupModel<SingleCountType>
    {
        string Author { get; set; }
        bool IncludeQuotes { get; set; }
    }
}

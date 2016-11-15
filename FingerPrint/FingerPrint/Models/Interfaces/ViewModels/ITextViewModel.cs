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
        /// <summary>
        /// Gets the text's author field.
        /// </summary>
        /// <returns>The text's author.</returns>
        string GetAuthor();

        /// <summary>
        /// Gets the text's include quotes boolean.
        /// </summary>
        /// <returns>True if the text is set to include words inside quotations when 
        /// giving its word counts and false otherwise.</returns>
        bool GetIncludeQuotes();
    }
}

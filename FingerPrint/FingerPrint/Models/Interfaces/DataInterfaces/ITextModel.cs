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
        /// <summary>
        /// Sets the author field of the text.
        /// </summary>
        /// <param name="author">The new value for the text's author field.</param>
        void SetAuthor(string author);

        /// <summary>
        /// Sets the text's include quotes boolean field.
        /// </summary>
        /// <param name="value">True if words in quotations should be counted, false otherwise.</param>
        void SetIncludeQuotes(bool value);

        /// <summary>
        /// Gets the counts including words in quotations.
        /// </summary>
        /// <returns>A collection of counts.</returns>
        ISingleWordCountModel GetCountsWithQuotes();

        /// <summary>
        /// Gets the counts excluding words in quotations.
        /// </summary>
        /// <returns>A collection of counts.</returns>
        ISingleWordCountModel GetCountsWithoutQuotes();
    }
}

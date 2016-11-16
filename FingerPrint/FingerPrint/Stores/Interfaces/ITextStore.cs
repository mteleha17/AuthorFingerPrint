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
        /// <summary>
        /// Finds the text entity corresponding to the model provided and updates its Name field.
        /// </summary>
        /// <param name="model">The model representing the text to modify.</param>
        /// <param name="newName">The new name for the text.</param>
        void ModifyName(ITextModel model, string newName);

        /// <summary>
        /// Finds the text entity corresponding to the model provided and updates its Author field.
        /// </summary>
        /// <param name="model">The model representing the text to modify.</param>
        /// <param name="newAuthor">The new author for the text.</param>
        void ModifyAuthor(ITextModel model, string newAuthor);

        /// <summary>
        /// Finds the text entity corresponding to the model provided and updates its IncludeQuotes field.
        /// </summary>
        /// <param name="model">The model representing the text to modify.</param>
        /// <param name="includeQuotes">True if the text should include words in quotations when returning
        /// word counts, false otherwise.</param>
        void ModifyIncludeQuotes(ITextModel model, bool includeQuotes);
    }
}

using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Controllers
{
    /// <summary>
    /// Interface to be implemented by a TextController, i.e., a controller that will handle
    /// the creation, deletion, updating, and fetching of texts.
    /// </summary>
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface ITextController<SingleCountType>
    {
        /// <summary>
        /// Gets the text with the specified name.
        /// </summary>
        /// <param name="name">The name of the text.</param>
        /// <returns>The text with the specified name or null if no such text exists.</returns>
        ITextViewModel<SingleCountType> GetTextByName(string name);

        /// <summary>
        /// Gets all texts by the specified author.
        /// </summary>
        /// <param name="author">The author of the text(s).</param>
        /// <returns>A list of all texts by the specified author, or an empty list if there are no such texts.</returns>
        List<ITextViewModel<SingleCountType>> GetTextByAuthor(string author);

        /// <summary>
        /// Adds a new text to the database.
        /// </summary>
        /// <param name="name">Text name.</param>
        /// <param name="input">TextReader object storing the contents of the text.</param>
        /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        /// <param name="author">Author name.</param>
        void CreateText(string name, TextReader input, int length, string author = null);

        /// <summary>
        /// Deletes the text with the specified name.
        /// </summary>
        /// <param name="model">The model corresponding to the text to delete.</param>
        void DeleteText(ITextViewModel<SingleCountType> model);

        /// <summary>
        /// Updates the text corresponding to the specified model by updating the specified fields.
        /// Fields that are not to be changed should be left null. 
        /// </summary>
        /// <param name="model">The model corresponding to the text to update.</param>
        /// <param name="name">The new text name.</param>
        /// <param name="author">The new author name.</param>
        /// <param name="quotesOn">The new setting for inclusion of text in quotation marks.</param>
        void UpdateText(ITextViewModel<SingleCountType> model, string name = null, string author = null, bool? quotesOn = null);
    }
}

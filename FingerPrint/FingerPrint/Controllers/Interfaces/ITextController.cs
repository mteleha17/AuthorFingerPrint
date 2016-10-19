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
    /// <typeparam name="TextEntityType">Temporary placeholder to be deleted when database and EF are set up.</typeparam>
    public interface ITextController<SingleCountType, TextEntityType>
    {
        /// <summary>
        /// Gets a list of texts matching some criteria.
        /// </summary>
        /// <param name="criteria">A function taking a Text and returning a bool.</param>
        /// <returns>A list of text view models.</returns>
        List<ITextViewModel<SingleCountType>> GetTextModels(Func<TextEntityType, bool> criteria);

        /// <summary>
        /// Determines whether or not at least one text exists in the database matching the criteria.
        /// </summary>
        /// <param name="criteria">A function taking a text and returning a bool.</param>
        /// <returns>True if such a text exists, false otherwise.</returns>
        bool TextExists(Func<TextEntityType, bool> criteria);

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
        /// Deletes the first text matching the criteria.
        /// </summary>
        /// <param name="criteria">A function taking a text and returning a bool</param>
        void DeleteText(Func<TextEntityType, bool> criteria);

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

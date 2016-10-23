using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface to be implemented by a model factory.
    /// </summary>
    public interface IModelFactory
    {
        /// <summary>
        /// Get a new text model by passing in the content of the text.
        /// </summary>
        /// <param name="name">Name for the new text model.</param>
        /// <param name="input">TextReader containing the content of the text itself.</param>
        /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        /// <returns>A new text model.</returns>
        ITextModel GetTextModel(string name, TextReader input, int length);

        /// <summary>
        /// Get a new text model by passing in an existing set of counts.
        /// </summary>
        /// <param name="name">Name for the new text model.</param>
        /// <param name="counts">The counts for the new text model.</param>
        /// <returns>A new text model.</returns>
        ITextModel GetTextModel(string name, IFlexibleWordCountModel counts);

        /// <summary>
        /// Get a new group model of the specified length.
        /// </summary>
        /// <param name="name">Name of the new group model.</param>
        /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        /// <returns>A new group model.</returns>
        IGroupModel GetGroupModel(string name, int length);

        /// <summary>
        /// Get a new single count model of the specified length. The counts will all be 0 initially.
        /// </summary>
        /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        /// <returns>A new single count model with all counts initialized to 0.</returns>
        ISingleWordCountModel GetSingleCountModel(int length);

        /// <summary>
        /// Get a new single count model by passing in the intial counts as an array of integers.
        /// </summary>
        /// <param name="counts">The intial counts for the new single count model.</param>
        /// <returns>A new single count model with the provided counts.</returns>
        ISingleWordCountModel GetSingleCountModel(int[] counts);

        /// <summary>
        /// Get a new flexible count model by length. A flexible count model consists of two collections of counts: one
        /// counting words in quotations and one excluding such words. Both collections will have the same
        /// number of counts.
        /// </summary>
        /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        /// <returns>A flexible count model of the specified length.</returns>
        IFlexibleWordCountModel GetFlexibleCountModel(int length);

        /// <summary>
        /// Get a new flexible count model by passing in the inital counts. A flexible count model consists
        /// of two collections of counts: one counting words in quotations and one excluding such words. Both
        /// collections must have the same number of counts.
        /// </summary>
        /// <param name="withQuotes">Single count model for counts including words in quotations.</param>
        /// <param name="withoutQuotes">Single count model for counts excluding words in quotations.</param>
        /// <returns>A flexible count model with the provided inital counts.</returns>
        IFlexibleWordCountModel GetFlexibleCountModel(ISingleWordCountModel withQuotes, ISingleWordCountModel withoutQuotes);
    }
}

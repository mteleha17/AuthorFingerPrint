using FingerPrint.Models.Interfaces;
using System;

namespace FingerPrint.Models
{
    /// <summary>
    /// Interface to be implemented by a class that represents flexible collections of word-length counts,
    /// i.e. a class that has a collection of counts including words in quotes and a collection of counts
    /// not including words in quotes.
    /// </summary>
    public interface IFlexibleWordCountModel : IMeasurableItem, ICopyable<IFlexibleWordCountModel>
    {
        /// <summary>
        /// Gets the counts including words inside quotations.
        /// </summary>
        /// <returns>A collection of word length counts.</returns>
        ISingleWordCountModel CountsWithQuotes();

        /// <summary>
        /// Gets the counts excluding words inside quotations.
        /// </summary>
        /// <returns>A collection of word length counts.</returns>
        ISingleWordCountModel CountsWithoutQuotes();

        /// <summary>
        /// Gets the individual count for a given word length. Indexes are off by one from word lengths, 
        /// so index 0 correponds to words of length 1.
        /// </summary>
        /// <param name="includeQuotes">Determines whether the count to be fetched includes words in quotations.</param>
        /// <param name="index">The word length for which to get a count.</param>
        /// <returns></returns>
        int GetAt(bool includeQuotes, int index);

        /// <summary>
        /// Sets the individual count for a given word length. Indexes are off by one from word lengths, 
        /// so index 0 correponds to words of length 1.
        /// </summary>
        /// <param name="includeQuotes">Determines whether the count to be set includes words in quotations.
        /// The other count for words of this length is unaffected.</param>
        /// <param name="index">The word length at which to set a count.</param>
        /// <param name="value">The new count; must be a non-negative integer.</param>
        void SetAt(bool includeQuotes, int index, int value);
    }
}
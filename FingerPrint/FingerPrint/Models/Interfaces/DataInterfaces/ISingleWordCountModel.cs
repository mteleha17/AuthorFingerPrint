using FingerPrint.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace FingerPrint.Models
{
    /// <summary>
    /// Interface to be implemented be a class that represents a single collection of word-length counts.
    /// </summary>
    public interface ISingleWordCountModel : IMeasurableItem, ICopyable<ISingleWordCountModel>
    {
        /// <summary>
        /// Get the count at the specific index. Indexes are off by one from word lengths,
        /// so e.g. index 0 correponds to words of length 1.
        /// </summary>
        /// <param name="index">The index at which to get the count.</param>
        /// <returns></returns>
        int GetAt(int index);

        /// <summary>
        /// Set the count at the specific index. Indexes are off by one from word lengths,
        /// so e.g. index 0 correponds to words of length 1.
        /// </summary>
        /// <param name="index">The index at which to get the count.</param>
        /// <param name="value">A non-negative integer representing a word count.</param>
        void SetAt(int index, int value);
    }
}
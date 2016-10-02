using FingerPrint.Models.Interfaces;
using System;

namespace FingerPrint.Models
{
    /// <summary>
    /// Interface to be implemented by a class that represents flexible collections of word-length counts,
    /// i.e. a class that has a collection of counts including quotes and a collection of counts not including quotes.
    /// </summary>
    public interface IFlexibleWordCountModel<SingleCountType> : IMeasurableItem, ICopyable<IFlexibleWordCountModel<SingleCountType>>
    {
        SingleCountType CountsWithQuotes();
        SingleCountType CountsWithoutQuotes();
        int GetAt(bool includeQuotes, int index);
        void SetAt(bool includeQuotes, int index, int value);
    }
}
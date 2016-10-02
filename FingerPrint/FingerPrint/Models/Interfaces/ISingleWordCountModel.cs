using FingerPrint.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace FingerPrint.Models
{
    /// <summary>
    /// Interface to be implemented be a class that represents a single collection of word-length counts.
    /// </summary>
    public interface ISingleWordCountModel : IMeasurableItem, ICountContainer, ICopyable<ISingleWordCountModel>
    {
        int GetAt(int index);
        void SetAt(int index, int value);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    /// <summary>
    /// Wrapper for two collections of word-length counts: one including quotes and one excluding quotes.
    /// </summary>
    public class FlexibleWordCountModel : IFlexibleWordCountModel
    {
        public readonly int _length;
        private ISingleWordCountModel _countsWithQuotes;
        private ISingleWordCountModel _countsWithoutQuotes;

        public FlexibleWordCountModel(ISingleWordCountModel countsWithQuotes, ISingleWordCountModel countsWithoutQuotes)
        {
            if (countsWithQuotes == null || countsWithoutQuotes == null)
            {
                throw new ArgumentException("Array of counts must not be null.");
            }
            int withQuotesLength = countsWithQuotes.Length();
            int withoutQuotesLength = countsWithoutQuotes.Length();

            if (withQuotesLength < 1 || withoutQuotesLength < 1)
            {
                throw new ArgumentException("Number of counts must not be less than 1.");
            }
            if (withQuotesLength != withoutQuotesLength)
            {
                throw new ArgumentException("The arrays must have the same length.");
            }
            for (int i = 0; i < withQuotesLength; i++)
            {
                if (countsWithQuotes.GetAt(i) < 0 || countsWithoutQuotes.GetAt(i) < 0)
                {
                    throw new ArgumentException($"Counts must not be negative. Item {i} in one of the arrays was negative.");
                }
            }
            _length = withQuotesLength;
            _countsWithQuotes = countsWithQuotes.Copy();
            _countsWithoutQuotes = countsWithoutQuotes.Copy();
        }

        public int Length()
        {
            return _length;
        }

        public int[] CountsWithQuotes()
        {
            return _countsWithQuotes.Counts();
        }

        public int[] CountsWithoutQuotes()
        {
            return _countsWithoutQuotes.Counts();
        }

        public IFlexibleWordCountModel Copy()
        {
            return new FlexibleWordCountModel(_countsWithQuotes.Copy(), _countsWithoutQuotes.Copy());
        }

        public int GetAt(bool includeQuotes, int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            if (includeQuotes)
            {
                return _countsWithQuotes.GetAt(index);
            }
            else
            {
                return _countsWithoutQuotes.GetAt(index);
            }
        }

        public void SetAt(bool includeQuotes, int index, int value)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            if (value < 0)
            {
                throw new ArgumentException("Counts must not be negative.");
            }
            if (includeQuotes)
            {
                _countsWithQuotes.SetAt(index, value);
            }
            else
            {
                _countsWithoutQuotes.SetAt(index, value);
            }
        }
    }
}

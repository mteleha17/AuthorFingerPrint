using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
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
                if (countsWithQuotes[i] < 0 || countsWithoutQuotes[i] < 0)
                {
                    throw new ArgumentException($"Counts must not be negative. Item {i} in one of the arrays was negative.");
                }
            }
            _length = withQuotesLength;
            _countsWithQuotes = countsWithQuotes.Copy();
            _countsWithoutQuotes = countsWithoutQuotes.Copy();
        }

        public ISingleWordCountModel CountsWithQuotes()
        {
            return _countsWithQuotes.Copy();
        }

        public ISingleWordCountModel CountsWithoutQuotes()
        {
            return _countsWithoutQuotes.Copy();
        }

        public int Length()
        {
            return _length;
        }

        private int GetAt(int index, bool withQuotes)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            if (withQuotes)
            {
               return _countsWithQuotes[index];
            }
            else
            {
                return _countsWithoutQuotes[index];
            }
        }

        private void SetAt(int index, bool withQuotes, int value)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            if (value < 0)
            {
                throw new ArgumentException("Counts must not be negative.");
            }
            if (withQuotes)
            {
                _countsWithQuotes[index] = value;
            }
            else
            {
                _countsWithoutQuotes[index] = value;
            }
        }

        public FlexibleWordCountModel Copy()
        {
            return new FlexibleWordCountModel(CountsWithQuotes(), CountsWithoutQuotes());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    /// <summary>
    /// Wrapper for a collection of word-length counts.
    /// </summary>
    public class SingleWordCountModel : ISingleWordCountModel
    {
        public readonly int _length;
        private int[] _counts;

        public SingleWordCountModel(int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("The number of counts must be positive.");
            }
            _length = length;
            _counts = new int[_length];
        }

        public SingleWordCountModel(int[] counts)
        {
            if (counts == null)
            {
                throw new ArgumentException("Array of counts must not be null.");
            }
            if (counts.Length < 1)
            {
                throw new ArgumentException("The number of counts must be positive.");
            }
            foreach (int i in counts)
            {
                if (i < 0)
                {
                    throw new ArgumentException("Counts must not be negative.");
                }
            }
            _length = counts.Length;
            _counts = counts.ToArray();
        }

        public int Length()
        {
            return _length;
        }

        public int[] Counts()
        {
            return _counts.ToArray();
        }

        public ISingleWordCountModel Copy()
        {
            return new SingleWordCountModel(Counts());
        }

        public int GetAt(int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            return _counts[index];
        }

        public void SetAt(int index, int value)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            if (value < 0)
            {
                throw new ArgumentException("Counts must not be negative.");
            }
            _counts[index] = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class SingleWordCountModel : ISingleWordCountModel
    {
        public readonly int _length;
        private int[] _counts;

        public int[] Counts()
        {
            return _counts.ToArray();
        }

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
                throw new ArgumentException("Number of counts must not be less than 1.");
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

        /// <summary>
        /// Enable use of square brackets to get and set array elements, e.g.
        /// var model = new SingleWordCountModel(n);
        /// model[i] = value
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>int at specified index</returns>
        public int this[int i]
        {
            get { return GetAt(i); }
            set { SetAt(i, value); }
        }

        private int GetAt(int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            return _counts[index];
        }

        private void SetAt(int index, int value)
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

        public ISingleWordCountModel Copy()
        {
            return new SingleWordCountModel(Counts());
        }

    }
}

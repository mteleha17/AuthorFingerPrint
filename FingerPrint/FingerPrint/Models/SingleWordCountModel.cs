using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class SingleWordCountModel
    {
        public readonly int _length;
        public int Length
        {
            get
            {
                return _length;
            }
        }

        private int[] _counts;
        public int[] Counts
        {
            get
            {
                return _counts.ToArray();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Array of counts must not be null.");
                }
                if (value.Length != _length)
                {
                    throw new ArgumentException("Wrong number of counts provided.");
                }
                foreach (int i in value)
                {
                    if (i < 0)
                    {
                        throw new ArgumentException("Counts must be positive.");
                    }
                }
                _counts = value;
            }
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
                    throw new ArgumentException("Counts must be positive.");
                }
            }
            _length = counts.Length;
            _counts = counts;
        }

        public void SetAt(int index, int value)
        {
            if (index < 0 || index >= _counts.Length)
            {
                throw new IndexOutOfRangeException();
            }
            if (value < 0)
            {
                throw new ArgumentException("Counts must not be negative.");
            }
            _counts[index] = value;
        }

        public SingleWordCountModel Copy()
        {
            return new SingleWordCountModel(Counts);
        }

    }
}

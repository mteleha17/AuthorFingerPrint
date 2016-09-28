using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class SingleWordCountModel
    {
        private int[] _counts;

        public int[] Counts
        {
            get
            {
                return _counts.ToArray();
            }
            set
            {
                foreach (int i in value)
                {
                    if (i < 0)
                    {
                        throw new ArgumentException("Counts must be positive.");
                    }
                }
                
            }
        }

        public void SetAtIndex(int index, int value)
        {
            if (index < 0 || index >= _counts.Length)
            {
                throw new IndexOutOfRangeException();
            }
            if (value < 0)
            {
                throw new ArgumentException("Counts must not be negative.");
            }
            Counts[index] = value;
        }

    }
}

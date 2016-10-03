using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintUnitTests.FakeModels
{
    public class FakeSingleWordCountModel : ISingleWordCountModel
    {
        public int[] CountCollection { get; set; }

        public FakeSingleWordCountModel(int[] countCollection)
        {
            CountCollection = countCollection;
        }

        public int GetAt(int index)
        {
            return CountCollection[index];
        }

        public void SetAt(int index, int value)
        {
            CountCollection[index] = value;
        }

        public int Length()
        {
            return CountCollection.Length;
        }

        public ISingleWordCountModel Copy()
        {
            return new FakeSingleWordCountModel(CountCollection.ToArray());
        }
    }
}

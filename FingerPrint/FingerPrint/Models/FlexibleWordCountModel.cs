using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class FlexibleWordCountModel
    {
        public SingleWordCountModel CountsWithQuotes { get; set; }
        public SingleWordCountModel CountsWithoutQuotes{ get; set; }

        public FlexibleWordCountModel(int length)
        {
            CountsWithQuotes = new SingleWordCountModel(length);
            CountsWithoutQuotes = new SingleWordCountModel(length);
        }
    }
}

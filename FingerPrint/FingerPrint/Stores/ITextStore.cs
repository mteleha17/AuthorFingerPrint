using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface ITextStore : IItemStore<File>
    {
        NumbersWithQuote GetCountsWithQuotes();
        NumbersWithoutQuote GetCountsWithoutQuotes();
    }
}

using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface ITextStore : IItemStore<Text, ITextModel>
    {
        ISingleWordCountModel GetCountsWithQuotes();
        ISingleWordCountModel GetCountsWithoutQuotes();
    }
}

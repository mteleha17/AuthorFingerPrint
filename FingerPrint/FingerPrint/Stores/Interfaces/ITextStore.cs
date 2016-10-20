using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface ITextStore<SingleCountType> : IItemStore<File, ITextModel<SingleCountType>>
    {
        SingleCountType GetCountsWithQuotes();
        SingleCountType GetCountsWithoutQuotes();
    }
}

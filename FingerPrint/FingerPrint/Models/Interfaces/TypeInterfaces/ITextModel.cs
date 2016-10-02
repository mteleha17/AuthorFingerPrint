using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface ITextModel<SingleCountType> : ITextOrGroupModel<SingleCountType>
    {
        string Author { get; set; }
        bool IncludeQuotes { get; set; }
    }
}

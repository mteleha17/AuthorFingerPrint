using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface IModelFactory<SingleCountType, FlexibleCountType>
    {
        ITextModel<SingleCountType> GetTextModel(string name, TextReader input, int length);
        ITextModel<SingleCountType> GetTextModel(string name, FlexibleCountType counts);
        IGroupModel<SingleCountType> GetGroupModel(string name, int length);
        SingleCountType GetSingleCountModel(int length);
        SingleCountType GetSingleCountModel(int[] counts);
        FlexibleCountType GetFlexibleCountModel(int length);
        FlexibleCountType GetFlexibleCountModel(SingleCountType withQuotes, SingleCountType withoutQuotes);
    }
}

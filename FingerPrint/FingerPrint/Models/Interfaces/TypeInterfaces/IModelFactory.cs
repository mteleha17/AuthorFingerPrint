using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface IModelFactory<SingleCountType>
    {
        ITextModel<SingleCountType> GetTextModel(string name, TextReader input, int numberWordLengths);
        IGroupModel<SingleCountType> GetGroupModel(string name, int numberWordLengths);
    }
}

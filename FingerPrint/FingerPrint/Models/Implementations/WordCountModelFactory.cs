using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FingerPrint.Models.Implementations
{
    public class WordCountModelFactory : IWordCountModelFactory<IFlexibleWordCountModel<ISingleWordCountModel>>
    {
        public IFlexibleWordCountModel<ISingleWordCountModel> GenerateCounts(TextReader text, IFlexibleWordCountModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }
    }
}

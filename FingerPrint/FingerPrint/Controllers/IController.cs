using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Controllers
{
    public interface IController<SingleCountType>
    {
        List<ITextViewModel<SingleCountType>> GetTextModels(Func<ITextViewModel<SingleCountType>, bool> criteria);
        List<IGroupViewModel<SingleCountType>> GetGroupModels(Func<IGroupViewModel<SingleCountType>, bool> criteria);
        void SaveText(ITextViewModel<SingleCountType> model);
        void SaveGroup(IGroupViewModel<SingleCountType> model);


    }
}

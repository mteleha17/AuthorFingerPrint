using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface IGroupViewModel<SingleCountType> : ITextOrGroupViewModel<SingleCountType>
    {
        List<ITextOrGroupViewModel<SingleCountType>> GetMembers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implemented by a class that contains ITextOrGroup items.
    /// </summary>
    public interface IGroupModel<SingleCountType> : ITextOrGroupModel<SingleCountType>
    {
        void Add(ITextOrGroupModel<SingleCountType> item);
        void Delete(ITextOrGroupModel<SingleCountType> item);
    }
}

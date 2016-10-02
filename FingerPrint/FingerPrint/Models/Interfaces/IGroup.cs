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
    public interface IGroup : ITextOrGroup
    {
        void Add(ITextOrGroup item);
        void Delete(ITextOrGroup item);
    }
}

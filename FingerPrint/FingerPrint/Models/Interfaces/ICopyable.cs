using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implemented by a class that can create a copy of itself.
    /// </summary>
    /// <typeparam name="Type">The type of the class that is implementing the interface.</typeparam>
    public interface ICopyable<Type>
    {
        Type Copy();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    /// <summary>
    /// Interface to be implemented by a class that can return its own name.
    /// </summary>
    public interface INamedItem
    {
        string GetName();
    }
}

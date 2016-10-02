using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implented by a class that has and can return a length dimension.
    /// </summary>
    public interface IMeasurableItem
    {
        int Length();
    }
}

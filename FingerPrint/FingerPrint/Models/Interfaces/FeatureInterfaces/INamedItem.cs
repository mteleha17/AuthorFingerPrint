using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.FeatureInterfaces
{
    /// <summary>
    /// Interface to be implemented by a class that has and can return a name.
    /// </summary>
    public interface INamedItem
    {
        string GetName();
    }
}

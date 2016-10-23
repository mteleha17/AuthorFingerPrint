using FingerPrint.Models.Interfaces.FeatureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface representing the methods of a text or group not concerned with mutation (getters not setters). 
    /// </summary>
    public interface ITextOrGroupViewModel : INamedItem, IMeasurableItem, ICountContainer
    {}
}

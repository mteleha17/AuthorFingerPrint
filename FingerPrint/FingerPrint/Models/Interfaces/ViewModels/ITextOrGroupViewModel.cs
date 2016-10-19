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
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface ITextOrGroupViewModel<SingleCountType> : INamedItem, IMeasurableItem, ICountContainer<SingleCountType>
    {}
}

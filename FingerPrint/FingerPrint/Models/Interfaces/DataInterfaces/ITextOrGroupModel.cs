using FingerPrint.Models.Interfaces.FeatureInterfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implemented by a text or group.
    /// </summary>
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface ITextOrGroupModel<SingleCountType> : ITextOrGroupViewModel<SingleCountType>
    {
        void SetName(string name);
    }
}

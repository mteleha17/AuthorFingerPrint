using FingerPrint.Models.Interfaces.FeatureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    public interface ITextOrGroupViewModel<SingleCountType> : INamedItem, IMeasurableItem, ICountContainer<SingleCountType>
    {}
}

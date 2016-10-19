using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface representing the methods of a group not concerned with mutation (getters not setters). 
    /// </summary>
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface IGroupViewModel<SingleCountType> : ITextOrGroupViewModel<SingleCountType>
    {
        /// <summary>
        /// Gets the texts and groups that are members of this group.
        /// </summary>
        /// <returns>List of texts and groups.</returns>
        List<ITextOrGroupViewModel<SingleCountType>> GetMembers();
    }
}

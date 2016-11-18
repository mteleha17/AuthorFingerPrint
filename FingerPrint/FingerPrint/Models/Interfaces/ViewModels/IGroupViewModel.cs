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
    public interface IGroupViewModel : ITextOrGroupViewModel
    {
        /// <summary>
        /// Gets the texts and groups that are members of this group.
        /// </summary>
        /// <returns>List of texts and groups.</returns>
        List<ITextOrGroupViewModel> GetMembers();
        bool ContainsRecursive(ITextOrGroupViewModel item);
    }
}

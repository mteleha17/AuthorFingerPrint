using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FingerPrint.Controllers
{
    /// <summary>
    /// Interface to be implemented by a GroupController, i.e., by a controller that will handle the
    /// creation, deletion, updating, and fetching of groups.
    /// </summary>
    public interface IGroupController
    {
        IGroupViewModel GetGroupByName(string name);

        List<IGroupViewModel> GetAllGroups();

        IGroupViewModel CreateGroup(string name, int length);

        void Delete(string name);

        IGroupViewModel AddItemToGroup(string groupName, string itemName);

        IGroupViewModel RemoveItemFromGroup(string groupName, string itemName);

        IGroupViewModel UpdateGroup(string oldName, string newName);
    }
}

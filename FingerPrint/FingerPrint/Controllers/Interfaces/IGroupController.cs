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
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    /// <typeparam name="GroupEntityType">Temporary placeholder to be deleted when database and EF are set up.</typeparam>
    public interface IGroupController<SingleCountType, GroupEntityType>
    {
        /// <summary>
        /// Gets a list of groups that match the criteria.
        /// </summary>
        /// <param name="criteria">A function taking a group and returning a bool.</param>
        /// <returns>A list of group view models.</returns>
        List<IGroupViewModel<SingleCountType>> GetGroupModels(Func<GroupEntityType, bool> criteria);

        /// <summary>
        /// Determines whether or not at least one group exists in the database
        /// meeting the specified criteria.
        /// </summary>
        /// <param name="criteria">A function taking a group and returning a bool.</param>
        /// <returns>True if such a group exists, false otherwise.</returns>
        bool GroupExists(Func<GroupEntityType, bool> criteria);

        /// <summary>
        /// Adds a new group to the database.
        /// </summary>
        /// <param name="name">The group's name.</param>
        /// /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        void CreateGroup(string name, int length);

        /// <summary>
        /// Deletes the first group matching the criteria from the database.
        /// </summary>
        /// <param name="criteria">A function taking a group and returning a bool.</param>
        void DeleteGroup(Func<GroupEntityType, bool> criteria);

        /// <summary>
        /// Adds a text or group to the specified group.
        /// </summary>
        /// <param name="group">The group to which to add the text or group.</param>
        /// <param name="item">The text or group to be added.</param>
        void AddToGroup(IGroupViewModel<SingleCountType> group, ITextOrGroupViewModel<SingleCountType> item);

        /// <summary>
        /// Removes a text or group from the specified group.
        /// </summary>
        /// <param name="group">The group from which to remove the text or group.</param>
        /// <param name="item">The text or group to be removed.</param>
        void RemoveFromGroup(IGroupViewModel<SingleCountType> group, ITextOrGroupViewModel<SingleCountType> item);
        
        /// <summary>
        /// Updates the specified group in the database.
        /// </summary>
        /// <param name="model">The group to update.</param>
        /// <param name="name">The new name.</param>
        void UpdateGroup(IGroupViewModel<SingleCountType> model, string name);
    }
}

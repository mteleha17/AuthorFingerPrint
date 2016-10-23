﻿using FingerPrint.Models.Interfaces.TypeInterfaces;
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
        /// <summary>
        /// Gets the group with the specified name.
        /// </summary>
        /// <param name="name">The name of the group to get.</param>
        /// <returns>The group with the specified name or null if there is no such group.</returns>
        IGroupViewModel GetGroupByName(string name);

        /// <summary>
        /// Adds a new group to the database.
        /// </summary>
        /// <param name="name">The group's name.</param>
        /// /// <param name="length">The word length above which further lengths will not be considered.
        /// For example, length = 10 would mean that all words of length 10+ are considered a single category.</param>
        void CreateGroup(string name, int length);

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="model">The group to delete.</param>
        void Delete(IGroupViewModel model);

        /// <summary>
        /// Adds a text or group to the specified group.
        /// </summary>
        /// <param name="group">The group to which to add the text or group.</param>
        /// <param name="item">The text or group to be added.</param>
        void AddToGroup(IGroupViewModel group, ITextOrGroupViewModel item);

        /// <summary>
        /// Removes a text or group from the specified group.
        /// </summary>
        /// <param name="group">The group from which to remove the text or group.</param>
        /// <param name="item">The text or group to be removed.</param>
        void RemoveFromGroup(IGroupViewModel group, ITextOrGroupViewModel item);
        
        /// <summary>
        /// Updates the specified group in the database.
        /// </summary>
        /// <param name="model">The group to update.</param>
        /// <param name="name">The new name.</param>
        void UpdateGroup(IGroupViewModel model, string name);
    }
}
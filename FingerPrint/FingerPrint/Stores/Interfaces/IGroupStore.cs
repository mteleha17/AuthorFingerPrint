using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    /// <summary>
    /// Interface to be implemented by a class that handles database transactions involving texts.
    /// </summary>
    public interface IGroupStore : IItemStore<Grouping, IGroupModel>
    {
        /// <summary>
        /// Finds the group corresponding to the provided model and updates its Name field.
        /// </summary>
        /// <param name="model">The model representing the group to modify.</param>
        /// <param name="newName">The new name for the group.</param>
        void ModifyName(IGroupModel model, string newName);

        bool Contains(IGroupModel model, ITextOrGroupModel item);

        /// <summary>
        /// Adds a text or group to the specified group.
        /// </summary>
        /// <param name="model">The model representing the group to modify.</param>
        /// <param name="item">A model representing the text or group to add.</param>
        void AddItem(IGroupModel model, ITextOrGroupModel item);

        /// <summary>
        /// Adds multiple texts and/or groups to the specified group.
        /// </summary>
        /// <param name="model">The model representing the group to modify.</param>
        /// <param name="items">The texts and/or groups to add.</param>
        void AddItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items);

        /// <summary>
        /// Removes a text or group from the specified group.
        /// </summary>
        /// <param name="model">The model representing the group to modify.</param>
        /// <param name="item">The text or group to remove.</param>
        void RemoveItem(IGroupModel model, ITextOrGroupModel item);

        /// <summary>
        /// Removes multiple texts and/or groups from the specified group.
        /// </summary>
        /// <param name="model">The model representing the group to modify.</param>
        /// <param name="items">The texts and/or groups to remove.</param>
        void RemoveItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items);

        bool IsParent(IGroupModel model);
    }
}
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implemented by a class that contains ITextOrGroup items.
    /// </summary>
    public interface IGroupModel : ITextOrGroupModel, IGroupViewModel
    {
        /// <summary>
        /// Event to be used to notify parent groups when this group is modified so that they can
        /// recalculate their counts accordingly.
        /// </summary>
        //event EventHandler Modified;

        /// <summary>
        /// Adds a text or group to this group.
        /// </summary>
        /// <param name="item">The text or group to add.</param>
        void Add(ITextOrGroupModel item);

        /// <summary>
        /// Removes a text or group from this group.
        /// </summary>
        /// <param name="item">The text or group to be removed.</param>
        void Remove(ITextOrGroupModel item);

        /// <summary>
        /// Determines whether the specified text or group is a member
        /// of this group.
        /// </summary>
        /// <param name="item">The text or group to be looked for.</param>
        /// <returns>True if the text or group specified is a member of this group, false otherwise.</returns>
        bool Contains(ITextOrGroupModel item);
    }
}

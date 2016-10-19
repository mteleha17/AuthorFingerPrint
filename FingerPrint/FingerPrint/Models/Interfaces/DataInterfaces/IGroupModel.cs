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
    /// <typeparam name="SingleCountType">The type of object being used to store a single collection of counts.</typeparam>
    public interface IGroupModel<SingleCountType> : ITextOrGroupModel<SingleCountType>, IGroupViewModel<SingleCountType>
    {
        /// <summary>
        /// event to be used to notify parent groups when this group is modified so that they can
        /// recalculate their counts accordingly.
        /// </summary>
        event EventHandler Modified;

        /// <summary>
        /// Adds a text or group to this group.
        /// </summary>
        /// <param name="item">The text or group to add.</param>
        void Add(ITextOrGroupModel<SingleCountType> item);

        /// <summary>
        /// Removes a text or group from this group.
        /// </summary>
        /// <param name="item">The text or group to be removed.</param>
        void Remove(ITextOrGroupModel<SingleCountType> item);

        /// <summary>
        /// Determines whether the specified text or group is a member
        /// of this group.
        /// </summary>
        /// <param name="item">The text or group to be looked for.</param>
        /// <returns>True if the text or group specified is a member of this group, false otherwise.</returns>
        bool Contains(ITextOrGroupModel<SingleCountType> item);
    }
}

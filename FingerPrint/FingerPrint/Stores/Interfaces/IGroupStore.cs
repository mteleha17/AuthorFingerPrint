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
        void ModifyName(IGroupModel model, string newName);

        bool Contains(IGroupModel model, ITextOrGroupModel item);

        void AddItem(IGroupModel model, ITextOrGroupModel item);

        void RemoveItem(IGroupModel model, ITextOrGroupModel item);
    }
}
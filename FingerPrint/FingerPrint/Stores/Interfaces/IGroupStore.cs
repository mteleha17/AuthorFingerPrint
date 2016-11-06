using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface IGroupStore : IItemStore<Group, IGroupModel>
    {
        void ModifyName(IGroupModel model, string newName);
        void AddItem(IGroupModel model, ITextOrGroupModel item);
        void AddItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items);
        void RemoveItem(IGroupModel model, ITextOrGroupModel item);
        void RemoveItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items);
    }
}

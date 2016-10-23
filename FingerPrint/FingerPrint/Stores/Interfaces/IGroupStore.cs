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
        IEnumerable<ITextModel> GetChildTexts(IGroupModel parent, Func<File, bool> criteria);
        void AddChildText(IGroupModel parent, ITextModel child);
        void RemoveChildText(IGroupModel parent, ITextModel child);
        IEnumerable<IGroupModel> GetChildGroups(IGroupModel parent, Func<Group, bool> criteria);
        void AddChildGroup(IGroupModel parent, IGroupModel child);
        void RemoveChildGroup(IGroupModel parent, IGroupModel child);
    }
}

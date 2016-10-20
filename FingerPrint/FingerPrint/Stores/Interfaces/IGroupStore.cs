using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface IGroupStore<SingleCountType> : IItemStore<Group, IGroupModel<SingleCountType>>
    {
        IEnumerable<ITextModel<SingleCountType>> GetChildTexts(IGroupModel<SingleCountType> parent, Func<File, bool> criteria);
        void AddChildText(IGroupModel<SingleCountType> parent, ITextModel<SingleCountType> child);
        void RemoveChildText(IGroupModel<SingleCountType> parent, ITextModel<SingleCountType> child);
        IEnumerable<IGroupModel<SingleCountType>> GetChildGroups(IGroupModel<SingleCountType> parent, Func<Group, bool> criteria);
        void AddChildGroup(IGroupModel<SingleCountType> parent, IGroupModel<SingleCountType> child);
        void RemoveChildGroup(IGroupModel<SingleCountType> parent, IGroupModel<SingleCountType> child);
    }
}

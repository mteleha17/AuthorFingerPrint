using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface IGroupStore : IItemStore<Group>
    {
        IEnumerable<File> GetChildTexts(Group parent, Func<File, bool> criteria);
        void AddChildText(Group parent, File child);
        IEnumerable<Group> GetChildGroups(Group parent, Func<Group, bool> criteria);
        void AddChildGroup(Group parent, Group child);
    }
}

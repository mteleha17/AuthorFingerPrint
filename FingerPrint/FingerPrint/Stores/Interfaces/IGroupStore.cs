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
        IGroupModel ModifyName(IGroupModel model, string newName);
    }
}

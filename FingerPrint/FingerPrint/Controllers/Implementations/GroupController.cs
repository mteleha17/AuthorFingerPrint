using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;

namespace FingerPrint.Controllers.Implementations
{
    public class GroupController : IGroupController<ISingleWordCountModel, Group>
    {
        private ITextStore _textStore;
        private IGroupStore _groupStore;

        public GroupController(ITextStore textStore, IGroupStore groupStore)
        {
            _textStore = textStore;
            _groupStore = groupStore;
        }

        public void AddToGroup(IGroupViewModel<ISingleWordCountModel> group, ITextOrGroupViewModel<ISingleWordCountModel> item)
        {
            throw new NotImplementedException();
        }

        public void CreateGroup(string name, int length)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public List<IGroupViewModel<ISingleWordCountModel>> GetGroupModels(Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public bool GroupExists(Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromGroup(IGroupViewModel<ISingleWordCountModel> group, ITextOrGroupViewModel<ISingleWordCountModel> item)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(IGroupViewModel<ISingleWordCountModel> model, string name)
        {
            throw new NotImplementedException();
        }
    }
}

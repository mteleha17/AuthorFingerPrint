using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrint.Controllers.Implementations
{
    public class GroupController : IGroupController
    {
        private IItemStore<Text, ITextModel> _textStore;
        private IItemStore<Group, IGroupModel> _groupStore;
        private IModelFactory _modelFactory;

        public GroupController(IItemStore<Text, ITextModel> textStore,
            IItemStore<Group, IGroupModel> groupStore,
            IModelFactory modelFactory)
        {
            _textStore = textStore;
            _groupStore = groupStore;
            _modelFactory = modelFactory;
        }

        public IGroupViewModel GetGroupByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name must not be null.");
            }
            return _groupStore.GetOne(x => x.Name == name);
        }

        public void CreateGroup(string name, int length)
        {
            _groupStore.Add(_modelFactory.GetGroupModel(name, length));
        }

        public void Delete(IGroupViewModel model)
        {
            _groupStore.Delete((IGroupModel)model);
        }

        public void AddToGroup(IGroupViewModel group, ITextOrGroupViewModel item)
        {
            throw new NotImplementedException();
            //if (item is ITextViewModel)
            //{
            //    _groupStore.AddChildText((IGroupModel)group, (ITextModel)item);
            //}
            //else
            //{
            //    _groupStore.AddChildGroup((IGroupModel)group, (IGroupModel)item);
            //}
        }

        public void RemoveFromGroup(IGroupViewModel group, ITextOrGroupViewModel item)
        {
            throw new NotImplementedException();

            //if (item is ITextViewModel)
            //{
            //    _groupStore.RemoveChildText((IGroupModel)group, (ITextModel)item);
            //}
            //else
            //{
            //    _groupStore.RemoveChildGroup((IGroupModel)group, (IGroupModel)item);
            //}
        }

        public void UpdateGroup(IGroupViewModel model, string name)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    throw new ArgumentException("Name cannot be null.");
            //}
            //IGroupModel updatedModel = (IGroupModel)model;
            //updatedModel.SetName(name);
            //_groupStore.Modify(updatedModel);
        }
    }
}

using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;
using FingerPrint.Models.Interfaces;

namespace FingerPrint.Controllers.Implementations
{
    public class GroupController : IGroupController<ISingleWordCountModel>
    {
        private ITextStore<ISingleWordCountModel> _textStore;
        private IGroupStore<ISingleWordCountModel> _groupStore;
        private IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> _modelFactory;

        public GroupController(ITextStore<ISingleWordCountModel> textStore,
            IGroupStore<ISingleWordCountModel> groupStore,
            IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> modelFatory)
        {
            _textStore = textStore;
            _groupStore = groupStore;
            _modelFactory = modelFatory;
        }

        public IGroupViewModel<ISingleWordCountModel> GetGroupByName(string name)
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

        public void Delete(IGroupViewModel<ISingleWordCountModel> model)
        {
            _groupStore.Delete((IGroupModel<ISingleWordCountModel>)model);
        }

        public void AddToGroup(IGroupViewModel<ISingleWordCountModel> group, ITextOrGroupViewModel<ISingleWordCountModel> item)
        {
            if (item is ITextViewModel<ISingleWordCountModel>)
            {
                _groupStore.AddChildText((IGroupModel<ISingleWordCountModel>)group, (ITextModel<ISingleWordCountModel>)item);
            }
            else
            {
                _groupStore.AddChildGroup((IGroupModel<ISingleWordCountModel>)group, (IGroupModel<ISingleWordCountModel>)item);
            }
        }

        public void RemoveFromGroup(IGroupViewModel<ISingleWordCountModel> group, ITextOrGroupViewModel<ISingleWordCountModel> item)
        {
            if (item is ITextViewModel<ISingleWordCountModel>)
            {
                _groupStore.RemoveChildText((IGroupModel<ISingleWordCountModel>)group, (ITextModel<ISingleWordCountModel>)item);
            }
            else
            {
                _groupStore.RemoveChildGroup((IGroupModel<ISingleWordCountModel>)group, (IGroupModel<ISingleWordCountModel>)item);
            }
        }

        public void UpdateGroup(IGroupViewModel<ISingleWordCountModel> model, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null.");
            }
            IGroupModel<ISingleWordCountModel> updatedModel = (IGroupModel<ISingleWordCountModel>)model;
            updatedModel.SetName(name);
            _groupStore.Modify(updatedModel);
        }
    }
}

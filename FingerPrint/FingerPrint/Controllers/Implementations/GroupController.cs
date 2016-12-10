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
        private List<IGroupViewModel> _tempDbGroup;
        private List<ITextViewModel> _tempDbText;
        private IAnalysisController _analysisController;
        private ITextStore _textStore;//group2
        private IGroupStore _groupStore;//group2
        private IModelFactory _modelFactory;

        public GroupController(IAnalysisController analysisController,
            ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            _tempDbGroup = new List<IGroupViewModel>();
            _tempDbText = new List<ITextViewModel>();
            _analysisController = analysisController;
            _textStore = textStore;//group2
            _groupStore = groupStore;//group2
            _modelFactory = modelFactory;
        }

        public IGroupViewModel GetGroupByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name must not be null.");
            }
            return _groupStore.GetOne(x => x.Name == name);//group2
            //return _tempDbGroup.FirstOrDefault(x => x.GetName() == name);//group1
        }

        public bool AnyByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name must not be null.");
            }
            return _groupStore.Exists(x => x.Name == name);//group2
            //return _tempDbGroup.Any(x => x.GetName() == name);//group1
        }

        public IGroupViewModel CreateGroup(string name, int length)
        {
            if (AnyByName(name))
            {
                throw new ArgumentException($"Cannot create group because another group in the database already has the name {name}.");
            }
            IGroupModel model = _modelFactory.GetGroupModel(name, length);
            _groupStore.Add(model);//group2
            //_tempDbGroup.Add(model);//group1
            return model;

        }

        public void Delete(IGroupViewModel model)
        {
            if (_analysisController.GroupIsActive(model))
            {
                throw new ArgumentException($"Cannot delete group {model.GetName()} because it is active.");
            }
            _groupStore.Delete((IGroupModel)model);//group2
            //_tempDbGroup.Remove(model);//group1
        }

        public void AddItemToGroup(IGroupViewModel model, ITextOrGroupViewModel item)
        {
            AddItemsToGroup(model, new List<ITextOrGroupViewModel>() { item});
        }

        public void AddItemsToGroup(IGroupViewModel model, IEnumerable<ITextOrGroupViewModel> items)
        {
            IGroupModel groupModel = (IGroupModel)model;
            IEnumerable<ITextOrGroupModel> itemModels = items.Select(x => (ITextOrGroupModel)x);
            foreach (var m in itemModels)
            {
                groupModel.Add(m);
            }
            _groupStore.AddItems(groupModel, itemModels);//group2
        }

        public void RemoveItemFromGroup(IGroupViewModel model, ITextOrGroupViewModel item)
        {
            RemoveItemsFromGroup(model, new List<ITextOrGroupViewModel>() { item});
        }

        public void RemoveItemsFromGroup(IGroupViewModel model, IEnumerable<ITextOrGroupViewModel> items)
        {
            IGroupModel groupModel = (IGroupModel)model;
            IEnumerable<ITextOrGroupModel> itemModels = items.Select(x => (ITextOrGroupModel)x);
            foreach (var m in itemModels)
            {
                groupModel.Remove(m);
            }
            _groupStore.RemoveItems(groupModel, itemModels);//group2
        }

        public void UpdateGroup(IGroupViewModel model, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null.");
            }
            IGroupModel updatedModel = (IGroupModel)model;
            updatedModel.SetName(name);
            _groupStore.ModifyName(updatedModel, name);//group2
        }

        public List<IGroupViewModel> GetAllGroups()
        {
            return _groupStore.GetMany(x => true).Select(x => ((IGroupViewModel)x)).ToList();//group2
            //return _tempDbGroup.Where(x => true).Select(x => ((IGroupViewModel)x)).ToList();//group1
        }
    }
}

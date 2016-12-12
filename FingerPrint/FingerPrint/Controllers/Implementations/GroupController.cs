﻿using FingerPrint.Models;
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
        //private List<IGroupViewModel> _tempDbGroup;
        //private List<ITextViewModel> _tempDbText;
        private IAnalysisController _analysisController;
        private ITextStore _textStore;
        private IGroupStore _groupStore;
        private IModelFactory _modelFactory;

        public GroupController(IAnalysisController analysisController,
            ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            //_tempDbText = textTempDb;
            //_tempDbGroup = groupTempDb;
            _analysisController = analysisController;
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
            //return _tempDbGroup.FirstOrDefault(x => x.GetName() == name);
        }

        public bool AnyByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name must not be null.");
            }
            return _groupStore.Exists(x => x.Name == name);
            //return _tempDbGroup.Any(x => x.GetName() == name);
        }

        public bool Contains(string groupName, string itemName)
        {
            return _groupStore.Contains(groupName, itemName);
        }

        public bool GroupIsEmpty(IGroupViewModel model)
        {
            return model.GetMembers().Count == 0;
        }

        public IGroupViewModel CreateGroup(string name, int length)
        {
            if (AnyByName(name) || _textStore.Exists(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot create group because another item in the database already has the name {name}.");
            }
            IGroupModel model = _modelFactory.GetGroupModel(name, length);
            _groupStore.Add(model);
            //_tempDbGroup.Add(model);
            return model;

        }

        public void Delete(IGroupViewModel model)
        {
            if (_analysisController.ItemIsActive(model.GetName()))
            {
                _analysisController.RemoveFromActiveItems(model.GetName());
            }
            _groupStore.Delete((IGroupModel)model);
            //_tempDbGroup.Remove(model);
        }

        public void AddItemToGroup(IGroupViewModel model, ITextOrGroupViewModel item)
        {
            AddItemsToGroup(model, new List<ITextOrGroupViewModel>() { item});
        }

        public void AddItemsToGroup(IGroupViewModel model, IEnumerable<ITextOrGroupViewModel> items)
        {
            if (items.Contains(null))
            {
                throw new ArgumentException("Cannot add a null item to a group.");
            }
            if (items.Any(x => x.GetName() == model.GetName()))
            {
                throw new ArgumentException("Cannot add a group to itself.");
            }
            foreach (var i in items)
            {
                if (i is IGroupViewModel && ((IGroupViewModel)i).GetMembers().Any(x => x.GetName() == model.GetName()))
                {
                    throw new ArgumentException("Cannot add a group to its own child.");
                }
            }
            IGroupModel groupModel = (IGroupModel)model;
            IEnumerable<ITextOrGroupModel> itemModels = items.Select(x => (ITextOrGroupModel)x);
            foreach (var m in itemModels)
            {
                groupModel.Add(m);
            }
            _groupStore.AddItems(groupModel, itemModels);
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
            _groupStore.RemoveItems(groupModel, itemModels);
        }

        public void UpdateGroup(IGroupViewModel model, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null.");
            }
            IGroupModel updatedModel = (IGroupModel)model;
            updatedModel.SetName(name);
            _groupStore.ModifyName(updatedModel, name);
        }

        public List<IGroupViewModel> GetAllGroups()
        {
            return _groupStore.GetMany(x => true).Select(x => ((IGroupViewModel)x)).ToList();
            //return _tempDbGroup.Where(x => true).Select(x => ((IGroupViewModel)x)).ToList();
        }
    }
}

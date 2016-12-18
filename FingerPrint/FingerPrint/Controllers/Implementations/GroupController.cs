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
        //private List<IGroupViewModel> _tempDbGroup;
        //private List<ITextViewModel> _tempDbText;
        //private IAnalysisController _analysisController;
        private ITextStore _textStore;
        private IGroupStore _groupStore;
        private IModelFactory _modelFactory;

        public GroupController(ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            //_tempDbText = textTempDb;
            //_tempDbGroup = groupTempDb;
            //_analysisController = analysisController;
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

        public IGroupViewModel CreateGroup(string name, int length)
        {
            if (_groupStore.Exists(x => x.Name == name) || _textStore.Exists(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot create group because another item in the database already has the name {name}.");
            }
            IGroupModel model = _modelFactory.GetGroupModel(name, length);
            _groupStore.Add(model);
            return model;

        }

        public void Delete(string name)
        {
            IGroupModel model = _groupStore.GetOne(x => x.Name == name);
            if (model == null)
            {
                throw new ArgumentException($"Can't delete group with name {name} because it doesn't exist.");
            }
            _groupStore.Disassociate(model);
            _groupStore.Delete(model);
        }

        public IGroupViewModel AddItemToGroup(string groupName, string itemName)
        {
            if (groupName == itemName)
            {
                throw new ArgumentException("Cannot add a group to itself.");
            }
            IGroupModel parent = _groupStore.GetOne(x => x.Name == groupName);
            if (parent == null)
            {
                throw new ArgumentException($"Can't add item to group named {groupName} since it doesn't exist.");
            }
            ITextModel childText = _textStore.GetOne(x => x.Name == itemName);
            IGroupModel childGroup = _groupStore.GetOne(x => x.Name == itemName);
            if (childText == null)
            {
                if (childGroup == null)
                {
                    throw new ArgumentException($"Can't add item named {itemName} to group since it doesn't exist.");
                }
                if (_groupStore.Contains(childGroup, parent))
                {
                    throw new ArgumentException("Can't add a group as a child to its own parent or a parent of its parent or ...");
                }
                if (_groupStore.Contains(parent, childGroup))
                {
                    throw new ArgumentException("Cannot add item to group since it is already a member of that group.");
                }
                parent.Add(childGroup);
                _groupStore.AddItem(parent, childGroup);
            }
            else
            {
                if (childGroup != null)
                {
                    throw new ArgumentException("The database has a text and group with the same name. That shouldn't happen.");
                }
                if (_groupStore.Contains(parent, childText))
                {
                    throw new ArgumentException("Cannot add item to group since it is already a member of that group.");
                }
                parent.Add(childText);
                _groupStore.AddItem(parent, childText);
            }
            return parent;
        }

        public IGroupViewModel RemoveItemFromGroup(string groupName, string itemName)
        {
            IGroupModel parent = _groupStore.GetOne(x => x.Name == groupName);
            if (parent == null)
            {
                throw new ArgumentException($"Can't remove item from group named {groupName} since it doesn't exist.");
            }
            ITextModel childText = _textStore.GetOne(x => x.Name == itemName);
            IGroupModel childGroup = _groupStore.GetOne(x => x.Name == itemName);
            if (childText == null)
            {
                if (childGroup == null)
                {
                    throw new ArgumentException($"Can't remove item named {itemName} from group since it doesn't exist.");
                }
                if (!_groupStore.Contains(parent, childGroup))
                {
                    throw new ArgumentException("Cannot remove item from group since it is not a member of that group.");
                }
                parent.Remove(childGroup);
                _groupStore.RemoveItem(parent, childGroup);
            }
            else
            {
                if (childGroup != null)
                {
                    throw new ArgumentException("The database has a text and group with the same name. That shouldn't happen.");
                }
                if (!_groupStore.Contains(parent, childText))
                {
                    throw new ArgumentException("Cannot remove item from group since it is not a member of that group.");
                }
                parent.Remove(childText);
                _groupStore.RemoveItem(parent, childText);
            }
            return parent;
        }

        public IGroupViewModel UpdateGroup(string oldName, string newName)
        {
            IGroupModel model = _groupStore.GetOne(x => x.Name == oldName);
            if (model == null)
            {
                throw new ArgumentException("Cannot update group since it doesn't exist.");
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name cannot be null.");
            }
            if (_groupStore.Exists(x => x.Name == newName) || _textStore.Exists(x => x.Name == newName))
            {
                throw new ArgumentException($"Cannot create group because another item in the database already has the name {newName}.");
            }
            _groupStore.ModifyName(model, newName);
            model.SetName(newName);
            return model;
        }

        public List<IGroupViewModel> GetAllGroups()
        {
            return _groupStore.GetMany(x => true).Select(x => ((IGroupViewModel)x)).ToList();
        }
    }
}

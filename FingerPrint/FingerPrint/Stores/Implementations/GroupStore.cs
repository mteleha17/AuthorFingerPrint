using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.AuxiliaryClasses;

namespace FingerPrint.Stores
{
    public class GroupStore : IGroupStore
    {
        private FingerprintV4Entities _db;
        private IModelFactory _modelFactory;
        private ITextStore _textStore;

        public GroupStore(FingerprintV4Entities db, IModelFactory modelFactory, ITextStore textStore)
        {
            _db = db;
            _modelFactory = modelFactory;
            _textStore = textStore;   
        }

        public void Add(IGroupModel model)
        {
            string name = model.GetName();
            if (_db.Groups.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot add model since a model already exists in the database with name {model.GetName()}.");
            }
            Group group = new Group()
            {
                Name = model.GetName(),
            };
            _db.Groups.Add(group);
            _db.SaveChanges();
            AddItems(model, model.GetMembers().Select(x => (ITextOrGroupModel)x));
        }

        public void Delete(IGroupModel model)
        {
            string name = model.GetName();
            Group group = _db.Groups.FirstOrDefault(x => x.Name == name);
            if (group == null)
            {
                throw new ArgumentException("Cannot delete group because it does not exist in the database.");
            }
            RemoveItems(model, model.GetMembers().Select(x => (ITextOrGroupModel)x));
            _db.Groups.Remove(group);
            _db.SaveChanges();
        }

        public bool Exists(Expression<Func<Group, bool>> criteria)
        {
            return _db.Groups.Any(criteria);
        }

        public IEnumerable<IGroupModel> GetMany(Expression<Func<Group, bool>> criteria)
        {
            foreach (Group group in _db.Groups.Where(criteria))
            {
                yield return GetOne(x => x.GroupID == group.GroupID);
            }
        }

        public IGroupModel GetOne(Expression<Func<Group, bool>> criteria)
        {
            Group group = _db.Groups.FirstOrDefault(criteria);
            IGroupModel output = _modelFactory.GetGroupModel(group.Name, UniversalConstants.CountSize);
            foreach (Text text in group.Texts)
            {
                ITextModel textModel = _textStore.GetOne(x => x.TextID == text.TextID);
                if (textModel == null)
                {
                    throw new ArgumentException("Cannot get group since it contains a reference to a nonexistant text.");
                }
                output.Add(textModel);
            }
            foreach (Group_Group groupGroup in _db.Group_Group.Where(x => x.ParentID == group.GroupID))
            {
                IGroupModel groupModel = GetOne(x => x.GroupID == groupGroup.ChildID);
                if (groupModel == null)
                {
                    throw new ArgumentException("Cannot get group since it contains a reference to a nonexistant child group.");
                }
                output.Add(groupModel);
            }
            return output;
        }

        public void ModifyName(IGroupModel model, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Cannot update group name to null or white space.");
            }
            string name = model.GetName();
            Group group = _db.Groups.FirstOrDefault(x => x.Name == name);
            if (group == null)
            {
                throw new ArgumentException("Cannot update group name because group does not exist in the database.");
            }
            group.Name = newName;
            _db.SaveChanges();
        }

        public void AddItem(IGroupModel model, ITextOrGroupModel item)
        {
            AddItems(model, new List<ITextOrGroupModel>() { item});
        }

        public void AddItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items)
        {
            string name = model.GetName();
            Group parentGroup = _db.Groups.FirstOrDefault(x => x.Name == name);
            if (parentGroup == null)
            {
                throw new ArgumentException("Cannot add items to group because the group is not in the database.");
            }
            List<Text> texts = new List<Text>();
            List<int> textIds = new List<int>();
            List<int> groupIds = new List<int>();
            foreach (var item in items)
            {
                if (item == null)
                {
                    throw new ArgumentException("Cannot add null item to group.");
                }
                if (item is ITextModel)
                {
                    string textName = item.GetName();
                    Text text = _db.Texts.FirstOrDefault(x => x.Name == textName);
                    if (text == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: text {text.Name}.");
                    }
                    texts.Add(text);
                    textIds.Add(text.TextID);
                }
                else
                {
                    string groupName = item.GetName();
                    Group group = _db.Groups.FirstOrDefault(x => x.Name == groupName);
                    if (group == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: group {group.Name}.");
                    }
                    groupIds.Add(group.GroupID);
                }
            }
            foreach (Text t in texts)
            {
                parentGroup.Texts.Add(t);
                _db.SaveChanges();
            }
            //foreach (int id in textIds)
            //{
            //    Text_Group textGroup = new Text_Group()
            //    {
            //        GroupID = parentGroup.GroupID,
            //        TextID = id
            //    };
            //    _db.Text_Group.Add(textGroup);
            //    _db.SaveChanges();
            //}
            foreach (int id in groupIds)
            {
                Group_Group groupGroup = new Group_Group()
                {
                    ParentID = parentGroup.GroupID,
                    ChildID = id
                };
                _db.Group_Group.Add(groupGroup);
                _db.SaveChanges();
            }
        }

        public void RemoveItem(IGroupModel model, ITextOrGroupModel item)
        {
            RemoveItems(model, new List<ITextOrGroupModel>() { item});
        }

        public void RemoveItems(IGroupModel model, IEnumerable<ITextOrGroupModel> items)
        {
            string name = model.GetName();
            Group parentGroup = _db.Groups.FirstOrDefault(x => x.Name == name);
            if (parentGroup == null)
            {
                throw new ArgumentException("Cannot add items to group because the group is not in the database.");
            }
            List<Text> texts = new List<Text>();
            List<int> textGroupIds = new List<int>();
            List<int> groupGroupIds = new List<int>();
            foreach (var item in items)
            {
                if (item == null)
                {
                    throw new ArgumentException("Cannot add null item to group.");
                }
                if (item is ITextModel)
                {
                    string textName = item.GetName();
                    Text text = _db.Texts.FirstOrDefault(x => x.Name == textName);
                    if (text == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: text {text.Name}.");
                    }
                    if (!parentGroup.Texts.Contains(text))
                    {
                        throw new ArgumentException("Cannot remove item from group because it is not a member of the group.");
                    }
                    texts.Add(text);
                    //Text_Group textGroup = _db.Text_Group.FirstOrDefault(x => x.GroupID == parentGroup.GroupID && x.TextID == text.TextID);
                    //if (textGroup == null)
                    //{
                    //    throw new ArgumentException("Cannot remove item from group because it is not a member of the group.");
                    //}
                   // textGroupIds.Add(textGroup.TextTextID);
                }
                else
                {
                    string groupName = item.GetName();
                    Group child = _db.Groups.FirstOrDefault(x => x.Name == groupName);
                    if (child == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: group {child.Name}.");
                    }
                    Group_Group groupGroup = _db.Group_Group.FirstOrDefault(x => x.ParentID == parentGroup.GroupID && x.ChildID == child.GroupID);
                    if (groupGroup == null)
                    {
                        throw new ArgumentException("Cannot remove item from group because it is not a member of the group.");
                    }
                 //   groupGroupIds.Add(groupGroup.GroupGroupID);
                }
            }
            foreach (Text t in texts)
            {
                parentGroup.Texts.Remove(t);
                _db.SaveChanges();
            }
            //foreach (int id in textGroupIds)
            //{
            //    //   Text_Group textGroup = _db.Text_Group.FirstOrDefault(x => x.TextTextID == id);
            //    //   _db.Text_Group.Remfove(textGroup);
            //    _db.SaveChanges();
            //}
            foreach (int id in groupGroupIds)
            {
                Group_Group groupGroup = _db.Group_Group.FirstOrDefault(x => x.GG_ID == id);
                _db.Group_Group.Remove(groupGroup);
                _db.SaveChanges();
            }
        }



    }
}

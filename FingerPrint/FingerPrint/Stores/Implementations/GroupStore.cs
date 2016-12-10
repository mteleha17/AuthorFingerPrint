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
        private FingerprintLite13Entities _db;
        private IModelFactory _modelFactory;
        private ITextStore _textStore;

        public GroupStore(FingerprintLite13Entities db, IModelFactory modelFactory, ITextStore textStore)
        {
            _db = db;
            _modelFactory = modelFactory;
            _textStore = textStore;   
        }

        public void Add(IGroupModel model)
        {
            string name = model.GetName();
            if (_db.Groupings.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot add model since a model already exists in the database with name {model.GetName()}.");
            }
            Grouping group = new Grouping()
            {
                Name = model.GetName(),
            };
            _db.Groupings.Add(group);
            _db.SaveChanges();
            AddItems(model, model.GetMembers().Select(x => (ITextOrGroupModel)x));
        }

        public void Delete(IGroupModel model)
        {
            string name = model.GetName();
            Grouping group = _db.Groupings.FirstOrDefault(x => x.Name == name);
            if (group == null)
            {
                throw new ArgumentException("Cannot delete group because it does not exist in the database.");
            }
            RemoveItems(model, model.GetMembers().Select(x => (ITextOrGroupModel)x));
            _db.Groupings.Remove(group);
            _db.SaveChanges();
        }

        public bool Exists(Expression<Func<Grouping, bool>> criteria)
        {
            return _db.Groupings.Any(criteria);
        }

        public IEnumerable<IGroupModel> GetMany(Expression<Func<Grouping, bool>> criteria)
        {
            foreach (Grouping group in _db.Groupings.Where(criteria))
            {
                yield return GetOne(x => x.Id == group.Id);
            }
        }

        public IGroupModel GetOne(Expression<Func<Grouping, bool>> criteria)
        {
            Grouping group = _db.Groupings.FirstOrDefault(criteria);
            if (group == null)
            {
                return null;
            }
            IGroupModel output = _modelFactory.GetGroupModel(group.Name, UniversalConstants.CountSize);
            IQueryable<int> TextIds = _db.Texts.Join(
                _db.Text_Grouping,
                text => text.Id,
                text_grouping => text_grouping.TextId,
                (text, text_grouping) => new { TextId = text_grouping.TextId }
                ).Join(
                    _db.Groupings,
                    tg => tg.TextId,
                    grouping => grouping.Id,
                    (tg, grouping) => new { TextId = tg.TextId, GroupingId = grouping.Id }
                ).Where(x => x.GroupingId == group.Id)
                 .Select(x => (int)x.TextId);

            foreach (Text text in _db.Texts.Where(x => TextIds.Contains((int)x.Id)))
            {
                ITextModel textModel = _textStore.GetOne(x => x.Id == text.Id);
                if (textModel == null)
                {
                    throw new ArgumentException("Cannot get group since it contains a reference to a nonexistant text.");
                }
                output.Add(textModel);
            }
            foreach (Grouping_Grouping groupGroup in _db.Grouping_Grouping.Where(x => x.ParentId == group.Id))
            {
                IGroupModel groupModel = GetOne(x => x.Id == groupGroup.ChildId);
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
            Grouping group = _db.Groupings.FirstOrDefault(x => x.Name == name);
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
            Grouping parentGroup = _db.Groupings.FirstOrDefault(x => x.Name == name);
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
                    textIds.Add((int)text.Id);
                }
                else
                {
                    string groupName = item.GetName();
                    Grouping group = _db.Groupings.FirstOrDefault(x => x.Name == groupName);
                    if (group == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: group {group.Name}.");
                    }
                    groupIds.Add((int)group.Id);
                }
            }
            foreach (Text t in texts)
            {
                //parentGroup.Texts.Add(t);
                _db.Text_Grouping.Add(new Text_Grouping() { TextId = t.Id, GroupingId = parentGroup.Id });
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
                Grouping_Grouping groupGroup = new Grouping_Grouping()
                {
                    ParentId = parentGroup.Id,
                    ChildId = id
                };
                _db.Grouping_Grouping.Add(groupGroup);
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
            Grouping parentGroup = _db.Groupings.FirstOrDefault(x => x.Name == name);
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
                    //if (!parentGroup.Texts.Contains(text))
                    //{
                    //    throw new ArgumentException("Cannot remove item from group because it is not a member of the group.");
                    //}
                    if (!_db.Text_Grouping.Any(x => x.TextId == text.Id && x.GroupingId == parentGroup.Id))
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
                    Grouping child = _db.Groupings.FirstOrDefault(x => x.Name == groupName);
                    if (child == null)
                    {
                        throw new ArgumentException($"Cannot add item to group because it does not exist in the database: group {child.Name}.");
                    }
                    Grouping_Grouping groupGroup = _db.Grouping_Grouping.FirstOrDefault(x => x.ParentId == parentGroup.Id && x.ChildId == child.Id);
                    if (groupGroup == null)
                    {
                        throw new ArgumentException("Cannot remove item from group because it is not a member of the group.");
                    }
                 //   groupGroupIds.Add(groupGroup.GroupGroupID);
                }
            }
            foreach (Text t in texts)
            {
                Text_Grouping textGrouping = _db.Text_Grouping.FirstOrDefault(x => x.TextId == t.Id && x.GroupingId == parentGroup.Id);
                if (textGrouping == null)
                {
                    throw new ArgumentException("Cannot remove one or more items since the group does not contain them.");
                }
                _db.Text_Grouping.Remove(textGrouping);
                //parentGroup.Texts.Remove(t);
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
                Grouping_Grouping groupGroup = _db.Grouping_Grouping.FirstOrDefault(x => x.Id == id);
                _db.Grouping_Grouping.Remove(groupGroup);
                _db.SaveChanges();
            }
        }



    }
}

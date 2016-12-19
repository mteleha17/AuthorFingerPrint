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

        public void DangerousDeleteAllTextsAndGroups()
        {
            var textGroups = _db.Text_Grouping;
            _db.Text_Grouping.RemoveRange(textGroups);
            _db.SaveChanges();
            var groupGroups = _db.Grouping_Grouping;
            _db.Grouping_Grouping.RemoveRange(groupGroups);
            _db.SaveChanges();
            var texts = _db.Texts;
            _db.Texts.RemoveRange(texts);
            _db.SaveChanges();
            var groups = _db.Groupings;
            _db.Groupings.RemoveRange(groups);
            _db.SaveChanges();
            var counts = _db.WordCounts;
            _db.WordCounts.RemoveRange(counts);
            _db.SaveChanges();

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
            //AddItems(model, model.GetMembers().Select(x => (ITextOrGroupModel)x));
        }

        public bool Contains(IGroupModel group, ITextOrGroupModel item)
        {
            string groupName = group.GetName();
            string itemName = item.GetName();
            if (string.Equals(groupName, itemName))
            {
                return false;
            }
            Grouping parentGroup = _db.Groupings.FirstOrDefault(x => x.Name == groupName);
            if (parentGroup == null)
            {
                throw new ArgumentException("Parent group does not exist.");
            }
            //IQueryable<Grouping_Grouping> childGroupIds = _db.Grouping_Grouping.Where(x => x.ParentId == parentGroup.Id);
            List<Grouping> allGroups = new List<Grouping>() { parentGroup };
            List<Grouping> nextGroups = new List<Grouping>();
            int oldCount = allGroups.Count;
            int newCount = allGroups.Count;
            do
            {
                oldCount = allGroups.Count;
                foreach (Grouping grouping in allGroups)
                {
                    nextGroups.AddRange(_db.Grouping_Grouping.Where(x => x.ParentId == grouping.Id).Join(
                        _db.Groupings,
                        gg => gg.ChildId,
                        g => g.Id,
                        (gg, g) => g
                        ));
                }
                nextGroups = nextGroups.Distinct().ToList();
                foreach (var nextGroup in nextGroups)
                {
                    allGroups.Add(nextGroup);
                }
                allGroups = allGroups.Distinct().ToList();
                newCount = allGroups.Count;
                nextGroups = new List<Grouping>();
            } while (oldCount < newCount);
            List<Text> allTexts = new List<Text>();
            foreach (Grouping g in allGroups)
            {
                IQueryable<Text> childTexts = _db.Text_Grouping.Where(x => x.GroupingId == g.Id).Join(
                    _db.Texts,
                    tg => tg.TextId,
                    t => t.Id,
                    (tg, t) => t
                    );
                allTexts.AddRange(childTexts);
            }
            allTexts = allTexts.Distinct().ToList();
            return allGroups.Any(x => x.Name == itemName) || allTexts.Any(x => x.Name == itemName);


            //Grouping group = _db.Groupings.FirstOrDefault(x => x.Name == groupName);
            //Grouping groupItem = _db.Groupings.FirstOrDefault(x => x.Name == itemName);
            //Text textItem = _db.Texts.FirstOrDefault(x => x.Name == itemName);
            //if (textItem == null && groupItem == null)
            //{
            //    throw new ArgumentException("No item with the specified name exists.");
            //}
            //if (textItem == null)
            //{
            //    return _db.Grouping_Grouping.Any(x => x.ParentId == group.Id && x.ChildId == groupItem.Id);
            //}
            //return _db.Text_Grouping.Any(x => x.TextId == textItem.Id && x.GroupingId == group.Id);
        }

        public void Delete(IGroupModel model)
        {
            string name = model.GetName();
            Grouping group = _db.Groupings.FirstOrDefault(x => x.Name == name);
            if (group == null)
            {
                throw new ArgumentException("Cannot delete group because it does not exist in the database.");
            }
            Disassociate(group);
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
                (text, text_grouping) => new { TextId = text_grouping.TextId, GroupingId = text_grouping.GroupingId }
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
            if (_db.Groupings.Any(x => x.Name == newName))
            {
                throw new ArgumentException("Cannot change groups name, since that name is already in use.");
            }
            group.Name = newName;
            _db.SaveChanges();
        }

        public void AddItem(IGroupModel model, ITextOrGroupModel item)
        {
            string parentName = model.GetName();
            Grouping parent = _db.Groupings.FirstOrDefault(x => x.Name == parentName);
            if (parent == null)
            {
                throw new ArgumentException("Cannot add to group since the group does not exist.");
            }
            string childName = item.GetName();
            Text childText = _db.Texts.FirstOrDefault(x => x.Name == childName);
            Grouping childGroup = _db.Groupings.FirstOrDefault(x => x.Name == childName);
            if (childText == null)
            {
                if (childGroup == null)
                {
                    throw new ArgumentException("Cannot add item to group since the item does not exist.");
                }
                if (_db.Grouping_Grouping.Any(x => x.ParentId == parent.Id && x.ChildId == childGroup.Id))
                {
                    throw new ArgumentException("Cannot add item to group since it is already a member.");
                }
                Grouping_Grouping association = new Grouping_Grouping() { ParentId = parent.Id, ChildId = childGroup.Id };
                _db.Grouping_Grouping.Add(association);
                _db.SaveChanges();
            }
            else
            {
                if (childGroup != null)
                {
                    throw new InvalidOperationException("The database contains both a text and a group with the same name. That's not good.");
                }
                if (_db.Text_Grouping.Any(x => x.TextId == childText.Id && x.GroupingId == parent.Id))
                {
                    throw new ArgumentException("Cannot add item to group since it is already a member.");
                }
                Text_Grouping assocation = new Text_Grouping() { TextId = childText.Id, GroupingId = parent.Id};
                _db.Text_Grouping.Add(assocation);
                _db.SaveChanges();
            }
        }

        public void RemoveItem(IGroupModel model, ITextOrGroupModel item)
        {
            string parentName = model.GetName();
            Grouping parent = _db.Groupings.FirstOrDefault(x => x.Name == parentName);
            if (parent == null)
            {
                throw new ArgumentException("Cannot add to group since the group does not exist.");
            }
            string childName = item.GetName();
            Text childText = _db.Texts.FirstOrDefault(x => x.Name == childName);
            Grouping childGroup = _db.Groupings.FirstOrDefault(x => x.Name == childName);
            if (childText == null)
            {
                if (childGroup == null)
                {
                    throw new ArgumentException("Cannot remove item from group since the item does not exist.");
                }
                Grouping_Grouping association = _db.Grouping_Grouping.FirstOrDefault(x => x.ParentId == parent.Id && x.ChildId == childGroup.Id);
                if (association == null)
                {
                    throw new ArgumentException("Cannot remove item from group since it is not a member of that group.");
                }
                _db.Grouping_Grouping.Remove(association);
                _db.SaveChanges();
            }
            else
            {
                if (childGroup != null)
                {
                    throw new InvalidOperationException("The database contains both a text and a group with the same name. That's not good.");
                }
                Text_Grouping assocation = _db.Text_Grouping.FirstOrDefault(x => x.TextId == childText.Id && x.GroupingId == parent.Id);
                if (assocation == null)
                {
                    throw new ArgumentException("Cannot remove item from group since it is not a member of that group.");
                }
                _db.Text_Grouping.Remove(assocation);
                _db.SaveChanges();
            }
        }

        private void Disassociate(Grouping group)
        {
            if (group == null)
            {
                throw new ArgumentException("Cannot disassociate group since it does not exist.");
            }
            IQueryable<Text_Grouping> textAssociations = _db.Text_Grouping.Where(x => x.GroupingId == group.Id);
            IQueryable<Grouping_Grouping> groupAssociations = _db.Grouping_Grouping.Where(x => x.ParentId == group.Id || x.ChildId == group.Id);
            _db.Text_Grouping.RemoveRange(textAssociations);
            _db.SaveChanges();
            _db.Grouping_Grouping.RemoveRange(groupAssociations);
            _db.SaveChanges();
        }
    }
}

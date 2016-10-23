using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;

namespace FingerPrint.Stores
{
    public class GroupStore : IGroupStore
    {
        private FingerprintEntities1 db;

        public GroupStore()
        {
            db = new FingerprintEntities1();
        }

        public void Add(IGroupModel model)
        {
            throw new NotImplementedException();
        }

        public void AddChildGroup(IGroupModel parent, IGroupModel child)
        {
            throw new NotImplementedException();
        }

        public void AddChildText(IGroupModel parent, ITextModel child)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGroupModel model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroupModel> GetChildGroups(IGroupModel parent, Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITextModel> GetChildTexts(IGroupModel parent, Func<File, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroupModel> GetMany(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IGroupModel GetOne(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public void Modify(IGroupModel model)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildGroup(IGroupModel parent, IGroupModel child)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildText(IGroupModel parent, ITextModel child)
        {
            throw new NotImplementedException();
        }
    }
}

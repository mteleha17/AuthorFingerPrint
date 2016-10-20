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
    public class GroupStore : IGroupStore<ISingleWordCountModel>
    {
        private FingerprintEntities1 db;

        public GroupStore()
        {
            db = new FingerprintEntities1();
        }

        public void Add(IGroupModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }

        public void AddChildGroup(IGroupModel<ISingleWordCountModel> parent, IGroupModel<ISingleWordCountModel> child)
        {
            throw new NotImplementedException();
        }

        public void AddChildText(IGroupModel<ISingleWordCountModel> parent, ITextModel<ISingleWordCountModel> child)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGroupModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroupModel<ISingleWordCountModel>> GetChildGroups(IGroupModel<ISingleWordCountModel> parent, Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITextModel<ISingleWordCountModel>> GetChildTexts(IGroupModel<ISingleWordCountModel> parent, Func<File, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroupModel<ISingleWordCountModel>> GetMany(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IGroupModel<ISingleWordCountModel> GetOne(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public void Modify(IGroupModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildGroup(IGroupModel<ISingleWordCountModel> parent, IGroupModel<ISingleWordCountModel> child)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildText(IGroupModel<ISingleWordCountModel> parent, ITextModel<ISingleWordCountModel> child)
        {
            throw new NotImplementedException();
        }
    }
}

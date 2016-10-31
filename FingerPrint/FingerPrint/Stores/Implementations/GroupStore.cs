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
    public class GroupStore : IItemStore<Group, IGroupModel>
    {
        private FingerprintV2Entities db;

        public GroupStore()
        {
            db = new FingerprintV2Entities();
        }

        public void Add(IGroupModel model)
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
    }
}

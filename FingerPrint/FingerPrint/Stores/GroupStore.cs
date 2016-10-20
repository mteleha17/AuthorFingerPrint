using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public class GroupStore : IGroupStore
    {
        public void Add(Group entity)
        {
            throw new NotImplementedException();
        }

        public void AddChildGroup(Group parent, Group child)
        {
            throw new NotImplementedException();
        }

        public void AddChildText(Group parent, File child)
        {
            throw new NotImplementedException();
        }

        public void Delete(Group entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Get(Expression<Func<Group, bool>> test)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetChildGroups(Group parent, Func<Group, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<File> GetChildTexts(Group parent, Func<File, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public void Modify(Group entity)
        {
            throw new NotImplementedException();
        }
    }
}

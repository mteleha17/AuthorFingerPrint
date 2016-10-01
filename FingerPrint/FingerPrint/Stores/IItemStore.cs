using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public interface IItemStore<EntityType>
    {
        void Add(EntityType entity);
        void Modify(EntityType entity);
        void Delete(EntityType entity);
        EntityType Get(Expression<Func<EntityType, bool>> test);
    }
}
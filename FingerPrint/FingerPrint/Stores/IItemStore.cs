using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    /// <summary>
    /// Interface to be implemented by a class that serves as an intermediary between its clients and a database
    /// or other datastore.
    /// </summary>
    /// <typeparam name="EntityType">The type of the entity that this store handles.</typeparam>
    public interface IItemStore<EntityType>
    {
        void Add(EntityType entity);
        void Modify(EntityType entity);
        void Delete(EntityType entity);
        EntityType Get(Expression<Func<EntityType, bool>> test);
    }
}
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
    /// <typeparam name="ModelType">The type of the model corresponding to the entity type.</typeparam>
    public interface IItemStore<EntityType, ModelType>
    {
        void Add(ModelType model);

        void Delete(ModelType model);

        bool Exists(Expression<Func<EntityType, bool>> criteria);

        ModelType GetOne(Expression<Func<EntityType, bool>> criteria);

        IEnumerable<ModelType> GetMany(Expression<Func<EntityType, bool>> criteria);
    }
}
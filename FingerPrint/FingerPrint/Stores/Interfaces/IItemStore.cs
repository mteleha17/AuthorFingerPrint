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
        /// <summary>
        /// Adds an entity to the database using the information provided in the model.
        /// </summary>
        /// <param name="model">Model representing the entity to add to the database.</param>
        void Add(ModelType model);

        /// <summary>
        /// Deletes the entity corresponding to the provided model from the database.
        /// </summary>
        /// <param name="model">Model representing the entity to delete from the database.</param>
        void Delete(ModelType model);

        /// <summary>
        /// Determines whether an entity meeting the provided criteria exists in the database.
        /// </summary>
        /// <param name="criteria">The criteria that the entity must meet.</param>
        /// <returns>True if such an entity exists, false otherwise.</returns>
        bool Exists(Expression<Func<EntityType, bool>> criteria);

        /// <summary>
        /// Returns a model representing the first entity in the database meeting the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria to be used to select the entity.</param>
        /// <returns>A model representing the entity, or null if no entity matches the specified criteria.</returns>
        ModelType GetOne(Expression<Func<EntityType, bool>> criteria);

        /// <summary>
        /// Returns a list of models representing all entities in the database meeting the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria to be used to select the entities.</param>
        /// <returns>A (possibly empty) list of entities meeting the criteria.</returns>
        IEnumerable<ModelType> GetMany(Expression<Func<EntityType, bool>> criteria);
    }
}
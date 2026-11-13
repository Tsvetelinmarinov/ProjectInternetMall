using EMall.Data.Repositories.Interfaces;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        ///  Retrieves all the records of the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        public IEnumerable<TEntity> GetAll<TEntity>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Adds an entity/record to the database.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The type of the entity.
        /// </typeparam>
        /// <param name="entity">
        ///  The entity to be added.
        /// </param>
        public void Add<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Retrieves entity of type <typeparamref name="TEntity"/> from the database
        ///  by its ID.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The type of the entity.
        /// </typeparam>
        /// <param name="id">
        ///  The ID of the record/entity.
        /// </param>
        /// <returns>
        ///  The record/entity with the specified ID.
        /// </returns>
        public TEntity FindById<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Updates existing record/entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The data type of the entity.
        /// </typeparam>
        /// <param name="id">
        ///  The ID of the entity to be updated.
        /// </param>
        public void Update<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Deletes entity/record of type <typeparamref name="TEntity"/> form the
        ///  database by its ID.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The type of the entity/record.
        /// </typeparam>
        /// <param name="id">
        ///  The ID of the entity/record to be deleted.
        /// </param>
        /// <returns>
        ///  True if deleted - otherwise False.
        /// </returns>
        public bool Delete<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

    }
}
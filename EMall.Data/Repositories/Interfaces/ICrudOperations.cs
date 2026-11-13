namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides base CRUD operations for all repositories.
    /// </summary>
    public interface ICrudOperations
    {
        /// <summary>
        ///  Adds an entity/record to the database.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The type of the entity.
        /// </typeparam>
        /// <param name="entity">
        ///  The entity to be added.
        /// </param>
        void Add<TEntity>(TEntity entity);

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
        TEntity FindById<TEntity>(int id);

        /// <summary>
        ///  Updates existing record/entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The data type of the entity.
        /// </typeparam>
        /// <param name="id">
        ///  The ID of the entity to be updated.
        /// </param>
        void Update<TEntity>(int id);

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
        bool Delete<TEntity>(int id);
    }
}
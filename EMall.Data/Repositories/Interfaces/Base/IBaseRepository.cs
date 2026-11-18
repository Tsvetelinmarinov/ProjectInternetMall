namespace EMall.Data.Repositories.Interfaces.Base
{
    /// <summary>
    ///  Provides base CRUD operations for all repositories.
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Retrieves all the entities from the database
        ///  whithout tracking it for changes.
        /// </summary>
        /// <returns>
        ///  <see cref="IQueryable{TEntity}"/> with all untracked entites.
        /// </returns>
        Task<IQueryable<TEntity>> AllAsNoTracking();

        /// <summary>
        ///  Adds an entity/record to the database.
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The type of the entity.
        /// </typeparam>
        /// <param name="entity">
        ///  The entity to be added.
        /// </param>
        Task AddAsync(TEntity entity);

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
        Task<TEntity> FindByIdAsync(int id);

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
        Task DeleteAsync(int id);
    }
}
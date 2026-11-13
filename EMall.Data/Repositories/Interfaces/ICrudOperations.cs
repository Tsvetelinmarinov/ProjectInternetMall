namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides base CRUD operations for all repositories.
    /// </summary>
    public interface ICrudOperations
    {
        /// <summary>
        ///  Retrieves all the records of the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        IEnumerable<TEntity> GetAll<TEntity>();
    }
}
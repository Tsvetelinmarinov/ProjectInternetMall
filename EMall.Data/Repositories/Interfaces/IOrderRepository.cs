namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public interface IOrderRepository : ICrudOperations
    {
        /// <summary>
        ///  Retrieves all the records of the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        IEnumerable<TEntity> GetAll<TEntity>();
    }
}
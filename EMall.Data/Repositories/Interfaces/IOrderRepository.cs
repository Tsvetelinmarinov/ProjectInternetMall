using EMall.Data.EntityModels;

namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public interface IOrderRepository : ICrudOperations<Order>
    {
        /// <summary>
        ///  Retrieves all the orders/>.
        /// </summary>
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
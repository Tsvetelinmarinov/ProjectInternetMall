using EMall.Data.EntityModels;
using EMall.Data.Repositories.Interfaces.Base;

namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public interface IOrderRepository : IBaseRepository<Order>
    {
        /// <summary>
        ///  Retrieves all the orders/>.
        /// </summary>
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
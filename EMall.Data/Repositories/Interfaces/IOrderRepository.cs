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
        ///  Retrieves order from the database by its ID.
        /// </summary>
        /// <param name="id">
        ///  The ID of the order.
        /// </param>
        /// <returns>
        ///  The <see cref="Order"/> entity with the corresponding ID.
        /// </returns>
        Task<Order> FindOrderWithProductsByIdAsync(int id);

        /// <summary>
        ///  Updates existing order in database.
        /// </summary>
        /// <param name="newOrderData">
        ///  The <see cref="Order"/> entity with the new data.
        /// </param>
        /// <returns>
        ///  A <see cref="Task"/> that returns <see cref="bool"/> indicating the
        ///  result of UPDATE operation.
        /// </returns>
        Task<bool> UpdateOrderAsync(Order newOrderData);
    }
}
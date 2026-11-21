using EMall.Core.ServiceModels;

namespace EMall.Core.Services.Interfaces
{
    /// <summary>
    ///  Service for orders.
    ///  Communicates with the orders repository and serves the AminHomeController or OrdersController.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        ///  Retrieves all the orders models from the orders repository.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{OrderOutputServiceModel}"/> with the orders.
        /// </returns>
        Task<IEnumerable<OrderServiceModel>> GetAllOrders(string? sortBy, string? sortDir);

        /// <summary>
        ///  Retrieve order from the orders repository
        ///  by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order service model with the coresponding ID.</returns>
        Task<OrderServiceModel> FindOrderById(int? id);

        /// <summary>
        ///  Updates existing Order entity.
        /// </summary>
        /// <param name="inputServiceModel">
        ///  The <see cref="OrderServiceModel"/> with the new data.
        /// </param>
        /// <returns>
        /// <see cref="Task"/> witch result returns <see cref="bool"/> that indicates
        /// the result of the UPDATE operation.
        /// </returns>
        Task<bool> UpdateOrder(OrderServiceModel inputServiceModel);
    }
}
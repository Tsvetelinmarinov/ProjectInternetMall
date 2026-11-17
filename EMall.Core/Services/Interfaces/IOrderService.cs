using EMall.Core.ServiceModels.Output;

namespace EMall.Core.Services.Interfaces
{
    /// <summary>
    ///  Service for orders.
    ///  Communicates with the orders repository and serves the AminHomeController.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        ///  Retrieves all the orders models from the orders repository.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{OrderOutputServiceModel}"/> with the orders.
        /// </returns>
        Task<IEnumerable<OrderOutputServiceModel>> GetAllOrders(string? sortBy, string? sortDir);

        /// <summary>
        ///  Retrieve order from the orders repository
        ///  by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order service model with the coresponding ID.</returns>
        Task<OrderOutputServiceModel> FindOrderById(int? id);
    }
}
using AutoMapper;
using EMall.Core.Exceptions;
using EMall.Core.ServiceModels.Output;
using EMall.Core.Services.Interfaces;
using EMall.Data.EntityModels;
using EMall.Data.Repositories.Interfaces;
using static EMall.Core.Common.ServiceConstants.OrderRelated;

namespace EMall.Core.Services
{
    /// <summary>
    ///  Service for orders.
    ///  Communicates with the orders repository and serves the AminHomeController.
    /// </summary>
    public class OrderService(IOrderRepository repository, IMapper mapper) : IOrderService
    {
        /// <summary>
        ///  Retrieves all the orders models from the orders repository.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{OrderOutputServiceModel}"/> with the orders.
        /// </returns>
        public async Task<IEnumerable<OrderOutputServiceModel>> GetAllOrders()
        {
            // Извличане на поръчките от репозиторията
            IEnumerable<Order> orderEntities = await repository.GetAll();

            // Кастване към сервизни модели.
            var orderServiceModels
                = mapper.Map<IEnumerable<OrderOutputServiceModel>>(orderEntities)
                 ?? throw new ServiceException(UnableToMapOrdersToOrderOutputServiceModels);

            return orderServiceModels;
        }
    }
}
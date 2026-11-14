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
        public IEnumerable<OrderOutputServiceModel> GetAllOrders()
        {
            // Извличане на поръчките от репозиторията им асинхронно, след което
            // синхронизиране на нишата на репото с тази + кастване на колекцията
            // към синхронния и еквивалент.
            IEnumerable<Order> orderEntities = repository
                .GetAll()
                .Result
                 as IEnumerable<Order>
                 ?? throw new ServiceException(UnableToCastIAsyncEnumerableToIEnumerable);

            // Кастване към сервизни модели.
            var orderServiceModels
                = mapper.Map<IEnumerable<OrderOutputServiceModel>>(orderEntities)
                 ?? throw new ServiceException(UnableToMapOrdersToOrderOutputServiceModels);

            return orderServiceModels;
        }
    }
}
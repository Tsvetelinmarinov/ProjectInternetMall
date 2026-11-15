using AutoMapper;
using EMall.Core.Exceptions;
using EMall.Core.ServiceModels.Output;
using EMall.Core.Services.Interfaces;
using EMall.Data.EntityModels;
using EMall.Data.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;
using static EMall.Core.Common.ServiceConstants.OrderRelated;

namespace EMall.Core.Services
{
    /// <summary>
    ///  Service for orders.
    ///  Communicates with the orders repository and serves the AminHomeController.
    /// </summary>
    public class OrderService(IOrderRepository repository, IMapper mapper) : IOrderService
    {
        #region Private Fields

        // Ключове за сортиране.
        private readonly List<string> _sortingKeys = new List<string>(){
            "number",
            "customer",
            "date",
            "total",
            "status"
        };

        // Посоки на сортиране:
        // asc - от по-малко към по-голямо, ако е текст - от А го Я.
        // desc - от по-голямо към по-малко, ако е текст - от Я до А.
        private readonly string _sortDirAsc = "asc";
        private readonly string _sortDirDesc = "desc";

        #endregion


        /// <summary>
        ///  Retrieves all the orders models from the orders repository.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{OrderOutputServiceModel}"/> with the orders.
        /// </returns>
        public async Task<IEnumerable<OrderOutputServiceModel>> GetAllOrders(string? sortBy, string? sortDir)
        {
            // Извличане на поръчките от репозиторията.
            IEnumerable<Order> orderEntities = await repository.GetAll();

            // Кастване към сервизни модели.
            var orderServiceModels
                = mapper.Map<IEnumerable<OrderOutputServiceModel>>(orderEntities)
                 ?? throw new ServiceException(UnableToMapOrdersToOrderOutputServiceModels);

            // Сортиране по указаните параметри.
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                throw new ServiceException(SortingKeyCannotBeNull);
            }

            if (string.IsNullOrWhiteSpace(sortDir))
            {
                throw new ServiceException(SortingDirectionKeyCannotBeNull);
            }

            if (this._sortingKeys.Contains(sortBy) is false)
            {
                throw new ServiceException(InvalidSortingKey);
            }

            if (sortDir != this._sortDirAsc && sortDir != this._sortDirDesc)
            {
                throw new ServiceException(InvalidSortingDirectoryKey);
            }

            /*
             * number customer date total status
             */
            string currentSortingKey = sortBy switch
            {
                "number" => this._sortingKeys[0],
                "customer" => this._sortingKeys[1],
                "date" => this._sortingKeys[2],
                "totoal" => this._sortingKeys[3],
                "status" => this._sortingKeys[4],
                _ => this._sortingKeys[2] // По подразбиране соритаме по дата, възходящо.
            };

            // За всеки ключ - малко по завъртяна проверка за посоката на сортиране.
            orderServiceModels = currentSortingKey switch
            {
                "number" 
                    => sortDir == this._sortDirAsc ?
                       orderServiceModels = orderServiceModels
                          .OrderBy((orderServiceModel) => orderServiceModel.OrderId)
                       :
                       orderServiceModels = orderServiceModels
                          .OrderByDescending((orderServiceModel) => orderServiceModel.OrderId),
                "customer" 
                    => sortDir == this._sortDirAsc ?
                       orderServiceModels = orderServiceModels
                          .OrderBy((orderServiceModel) => orderServiceModel.CustomerName)
                       :
                       orderServiceModels = orderServiceModels
                          .OrderByDescending((orderServiceModel) => orderServiceModel.CustomerName),
                "date"
                    => sortDir == this._sortDirAsc ?
                       orderServiceModels = orderServiceModels
                          .OrderBy((orderServiceModel) => orderServiceModel.CreatedOn)
                       :
                       orderServiceModels = orderServiceModels
                          .OrderByDescending((orderServiceModel) => orderServiceModel.CreatedOn),
                "total"
                    => sortDir == this._sortDirAsc ?
                       orderServiceModels = orderServiceModels
                          .OrderBy((orderServiceModel) => orderServiceModel.TotalPrice)
                       :
                       orderServiceModels = orderServiceModels
                          .OrderByDescending((orderServiceModel) => orderServiceModel.TotalPrice),
                "status"
                    => sortDir == this._sortDirAsc ?
                       orderServiceModels = orderServiceModels
                          .OrderBy((orderServiceModel) => orderServiceModel.Status)
                       :
                       orderServiceModels = orderServiceModels
                          .OrderByDescending((orderServiceModel) => orderServiceModel.Status),
                _ => orderServiceModels.OrderBy((orderServiceModel) => orderServiceModel.CreatedOn)
            };

            return orderServiceModels;
        }
    }
}
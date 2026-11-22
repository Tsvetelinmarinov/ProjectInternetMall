using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Exceptions;
using EMall.Data.Repositories.Base;
using EMall.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EMall.Data.EntityModels.Common.DbConstants.BaseRepositoryRelated;
using static EMall.Data.EntityModels.Common.DbConstants.OrderProductRelated;
using static EMall.Data.EntityModels.Common.DbConstants.OrderRelated;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository(ApplicationDbContext dbContext)
        : RepositoryBase<Order>(dbContext),
          IOrderRepository
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
        public async Task<Order> FindOrderWithProductsByIdAsync(int id)
        {
            var order = await this
                .DbContext
                .Orders
                .AsNoTracking()
                .Include(order => order.OrderProducts!)
                .ThenInclude(orderProduct => orderProduct.Product!)
                .ThenInclude(product => product.Category)
                .SingleOrDefaultAsync(order => order.OrderId == id)
                ?? throw new DbException(NoSuchEntity);

            var orderProducts = this
                .DbContext
                .OrderProducts
                .Where((orderProduct) => orderProduct.OrderId == order.OrderId)
                .Include((orderProduct) => orderProduct.Product)
                .ThenInclude((product) => product!.Category)
                .ToList() 
                ?? throw new DbException(NoSuchOrderOrProducts);

            order.OrderProducts = orderProducts;
            return order;
        }

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
        public async Task<bool> UpdateOrderAsync(Order newOrderData)
        {
            if (newOrderData is null)
            {
                throw new DbException(UpdatingOrderDTOIsNull);
            }

            var existingOrder = await this.CurrentSet.FindAsync(newOrderData.OrderId) 
                ?? throw new DbException(NoOrderFromDb);

            existingOrder.CustomerName = newOrderData.CustomerName;
            existingOrder.CustomerEmail = newOrderData.CustomerEmail;
            existingOrder.CreatedOn = newOrderData.CreatedOn;
            existingOrder.AddressForShipping = newOrderData.AddressForShipping;
            existingOrder.Status = newOrderData.Status;

            _ = await this.DbContext.SaveChangesAsync(
                new CancellationTokenSource(
                    TimeSpan.FromMinutes(5) //=> If after 5 minutes still cannot save.
                ).Token
            );

            return true;
        }
    }
}
using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Exceptions;
using EMall.Data.Repositories.Base;
using EMall.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EMall.Data.EntityModels.Common.DbConstants.BaseRepositoryRelated;
using static EMall.Data.EntityModels.Common.DbConstants.OrderProductRelated;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository(ApplicationDbContext dbContext)
        : RepositoryBase<Order>(dbContext),
          IOrderRepository
    {
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
    }
}
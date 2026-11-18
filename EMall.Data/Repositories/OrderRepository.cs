using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Repositories.Base;
using EMall.Data.Repositories.Interfaces;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository(ApplicationDbContext dbContext)
        : RepositoryBase<Order>(dbContext),
          IOrderRepository
    {
    }
}
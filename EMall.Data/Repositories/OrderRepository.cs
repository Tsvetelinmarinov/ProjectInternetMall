using EMall.Data.Repositories.Interfaces;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        public void Add<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll<TEntity>()
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Repositories.Base;
using EMall.Data.Repositories.Interfaces;

namespace EMall.Data.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) :
        RepositoryBase<Product>(dbContext),
        IProductRepository
    {
    }
}
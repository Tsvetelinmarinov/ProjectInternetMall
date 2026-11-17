using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Exceptions;
using EMall.Data.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using static EMall.Data.EntityModels.Common.DbConstants.CategoryRelated;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for entity Category.
    /// </summary>
    public class CategoryRepository(ApplicationDbContext database) : ICategoryRepository
    {
        /// <summary>
        ///  Retrieves all categories from the database.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{Category}"/> with all categories.
        /// </returns>
        public async Task<IAsyncEnumerable<Category>> GetAllCategoriesAsync() 
        {
            return database
                .Categories
                .Include((category) => category.Products)
                .AsNoTracking()
                .AsAsyncEnumerable()
                 ?? throw new DbException(NoCategoriesFromDb);
        }

        public Task Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
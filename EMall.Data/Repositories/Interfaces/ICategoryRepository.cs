using EMall.Data.EntityModels;

namespace EMall.Data.Repositories.Interfaces
{
    /// <summary>
    ///  Provides CRUD operations for entity Category.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        ///  Retrieves all categories from the database.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{Category}"/> with all categories.
        /// </returns>
        Task<IAsyncEnumerable<Category>> GetAllCategoriesAsync();
    }
}
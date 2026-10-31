using EMall.Core.ServiceModels.Output;

namespace EMall.Core.Services.Interfaces
{
    /// <summary>
    ///  Communicates with CategoryRepository(Data layer) and 
    ///  serves CategoryController(WebMVC layer).
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        ///  Retrieves all the categories from the database.
        /// </summary>
        /// <returns>
        ///  <see cref="IAsyncEnumerable{CategoryServiceOutputModel}"/> with the categories
        ///  mapped to service output models.
        /// </returns>
        Task<IAsyncEnumerable<ProductOutputServiceModel>> GetAllCategoriesAsync();
    }
}
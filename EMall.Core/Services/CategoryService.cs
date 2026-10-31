using AutoMapper;
using EMall.Core.Exceptions;
using EMall.Core.ServiceModels.Output;
using EMall.Core.Services.Interfaces;
using EMall.Data.EntityModels;
using EMall.Data.Repositories.Interfaces;
using static EMall.Core.Common.ServiceConstants.CategoryRelated;

namespace EMall.Core.Services
{
    /// <summary>
    ///  Communicates with CategoryRepository(Data layer) and 
    ///  serves CategoryController(WebMVC layer).
    /// </summary>
    public class CategoryService(ICategoryRepository repository, IMapper autoMapper) : ICategoryService
    {
        /// <summary>
        ///  Retrieves all the categories from the database.
        /// </summary>
        /// <returns>
        ///  <see cref="IEnumerable{CategoryServiceOutputModel}"/> with the categories
        ///  mapped to service output models.
        /// </returns>
        public async Task<IEnumerable<CategoryOutputServiceModel>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categoryEntities = (IEnumerable<Category>)await repository.GetAllCategoriesAsync();

            IEnumerable<CategoryOutputServiceModel> categoryServiceModels
                = autoMapper.Map<IEnumerable<CategoryOutputServiceModel>>(categoryEntities)
                  ?? throw new ServiceException(UnableToMapCategoryToCategoryServiceOutputModel);

            return categoryServiceModels;
        }
    }
}
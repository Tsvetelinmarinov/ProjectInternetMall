using AutoMapper;
using EMall.Core.Services.Interfaces;
using EMall.Exceptions;
using EMall.ViewModels.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EMall.Constants.WebConstants.CatalogController;

namespace EMall.Controllers
{
    /// <summary>
    ///  Provides endpoints for Catalog page.
    /// </summary>
    [AllowAnonymous]
    public class CatalogController(ICategoryService categoryService, IMapper mapper) : CoreController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoryServiceModels = await categoryService.GetAllCategoriesAsync();

            var categoryViewModels
                = mapper.Map<IAsyncEnumerable<CategoryOutputViewModel>>(categoryServiceModels)
                ?? throw new WebException(UnableToMapCategoryOutputServiceModelsToCategoryOutputViewModels);

            return View(categoryViewModels);
        }
    } 
}
using AutoMapper;
using EMall.Areas.Administration.ViewModels;
using EMall.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the administrator page.
    /// </summary>
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class HomeController(ICategoryService categoryService, IMapper mapper) : Controller
    {
        /// <summary>
        ///  Endpoint of the Administrator dashboard page.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoryServiceModels 
                = await categoryService.GetAllCategoriesAsync();

            var categoryStatViewModels 
                = mapper.Map<IEnumerable<CategoryStatViewModel>>(categoryServiceModels);

            if (categoryStatViewModels is null)
            {
                throw new ArgumentNullException(nameof(categoryStatViewModels));
            }

            var boardModel = new AdminDashboardViewModel
            {
                PendingOrders = 0,
                ProductsCount = categoryStatViewModels.Sum((categoryStat) => categoryStat.ProductsCount),
                OrdersCount = 0,
                UsersCount = 1,
                NewUsersThisWeek = 0,
                RevenueThisMonth = 0,
                Categories = [.. categoryStatViewModels],
                RecentOrders = [],
                LowStock = []
            };

            return View(boardModel);
        }
    }
}
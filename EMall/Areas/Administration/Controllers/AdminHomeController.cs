using EMall.Areas.Administration.ViewModels;
using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the administrator page.
    /// </summary>
    public class AdminHomeController(ICategoryService categoryService, IOrderService ordersService) 
        : AdminCoreController
    {
        /// <summary>
        ///  Endpoint of the Administrator dashboard page.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoryServiceModels 
                = await categoryService.GetAllCategoriesAsync();

            var categoryStatViewModels = categoryServiceModels
                .OrderBy((category) => category.CategoryName)
                .Select((category) => new CategoryStatViewModel
                {
                    CategoryName = category.CategoryName,
                    Color = category.Color,
                    ProductsCount = (category.Products ??[]).Count()
                });

            var orderServiceModels = await ordersService.GetAllOrders("date", "asc"); 

            // Понеже тази колекция е за нови поръчки, се взимат само 5 колкото за dashboard-а.
            // Все пак този медот връща всички, защото ще се използва и в OrderController-а.
            if (orderServiceModels.Count() > 5)
            {
                orderServiceModels = orderServiceModels.Take(5);
            }

            var orderViewModels = orderServiceModels
                .Select((orderServiceModel) => new OrderOutputViewModel
                {
                    OrderId = orderServiceModel.OrderId,
                    CustomerName = orderServiceModel.CustomerName,
                    CreatedOn = orderServiceModel.CreatedOn,
                    Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                    TotalPrice = orderServiceModel.TotalPrice,
                })
                .ToList();

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
                RecentOrders = orderViewModels,
                LowStock = []
            };

            return View(boardModel);
        }
    }
}
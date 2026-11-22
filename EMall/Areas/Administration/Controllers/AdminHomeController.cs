using EMall.Areas.Administration.ViewModels;
using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Core.Services.Interfaces;
using EMall.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the administrator page.
    /// </summary>
    public class AdminHomeController(
        ICategoryService categoryService,
        IOrderService ordersService,
        UserManager<ApplicationUser> userManager
    ) : AdminCoreController
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

            var allOrders = (await ordersService.GetAllOrders("date", "asc")).ToList();

            var recentOrders = allOrders
                .Take(5)
                .Select((orderServiceModel) => new OrderViewModel
                {
                    OrderId = orderServiceModel.OrderId,
                    CustomerName = orderServiceModel.CustomerName,
                    CustomerEmail = orderServiceModel.CustomerEmail,
                    AddressForShipping = orderServiceModel.AddressForShipping,
                    CreatedOn = orderServiceModel.CreatedOn,
                    Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                    TotalPrice = orderServiceModel.TotalPrice,
                })
                .ToList();

            var pendingOrdersCount = allOrders.Count(order =>
                order.Status == OrderStatus.Pending.ToString()
            );

            if (categoryStatViewModels is null)
            {
                throw new ArgumentNullException(nameof(categoryStatViewModels));
            }

            var boardModel = new AdminDashboardViewModel
            {
                PendingOrders = pendingOrdersCount,
                ProductsCount = categoryStatViewModels.Sum((categoryStat) => categoryStat.ProductsCount),
                OrdersCount = allOrders.Count,
                UsersCount = await userManager.Users.CountAsync(),
                NewUsersThisWeek = 0,
                RevenueThisMonth = recentOrders.Sum((orderModel) => orderModel.TotalPrice),
                Categories = [.. categoryStatViewModels],
                RecentOrders = recentOrders,
                LowStock = []
            };

            return View(boardModel);
        }
    }
}
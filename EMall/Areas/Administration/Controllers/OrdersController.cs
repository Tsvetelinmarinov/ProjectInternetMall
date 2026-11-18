using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the orders menu in the admin panel.
    /// </summary>
    public class OrdersController(IOrderService orderService) : AdminCoreController
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? sortBy, [FromQuery] string? sortDir)
        {
            sortBy ??= "date";
            sortDir ??= "asc";

            ViewBag.SortBy = sortBy;
            ViewBag.SortDir = sortDir;

            var orderServiceModels 
                = await orderService.GetAllOrders(sortBy, sortDir);

            var orderViewModels = orderServiceModels
                .Select((orderServiceModel) => new OrderOutputViewModel
                {
                    OrderId = orderServiceModel.OrderId,
                    CustomerName = orderServiceModel.CustomerName,
                    TotalPrice = orderServiceModel.TotalPrice,
                    CreatedOn = orderServiceModel.CreatedOn,
                    Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                }).ToList();

            return View(orderViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails([FromRoute] int? id)
        {
            var orderServiceModel = await orderService.FindOrderById(id);

            var orderViewModel = new OrderOutputViewModel
            {
                OrderId = orderServiceModel.OrderId,
                CustomerName = orderServiceModel.CustomerName,
                Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                CreatedOn = orderServiceModel.CreatedOn,
                TotalPrice = orderServiceModel.TotalPrice
            };

            return View(orderViewModel);
        }
    }
}
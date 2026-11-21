using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Areas.Administration.ViewModels.OrderProduct;
using EMall.Core.Services.Interfaces;
using EMall.ViewModels.Output;
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
                    CustomerEmail = orderServiceModel.CustomerEmail,
                    AddressForShipping = orderServiceModel.AddressForShipping,
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
                CustomerEmail = orderServiceModel.CustomerEmail,
                AddressForShipping = orderServiceModel.AddressForShipping,
                Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                CreatedOn = orderServiceModel.CreatedOn,
                TotalPrice = orderServiceModel.TotalPrice,
                Products = orderServiceModel.Products?.Select(orderProduct => new OrderProductOutputViewModel
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    Product = orderProduct.Product is null ? 
                        null 
                        : 
                        new ProductOutputViewModel
                        {
                            ProductName = orderProduct.Product.ProductName,
                            Description = orderProduct.Product.Description,
                            Claims = orderProduct.Product.Claims.Select(claim => new ProductClaimOutputViewModel
                            {
                                ClaimName = claim.ClaimName,
                                ClaimValue = claim.ClaimValue
                            }),
                            Price = orderProduct.Product.Price,
                            Category = orderProduct.Product.Category is null ? 
                                null 
                                : 
                                new CategoryOutputViewModel
                                {
                                    CategoryName = orderProduct.Product.Category.CategoryName,
                                    CategoryDescription = orderProduct.Product.Category.CategoryDescription,
                                    Icon = orderProduct.Product.Category.Icon,
                                    Color = orderProduct.Product.Category.Color
                                }
                        }
                }).ToList()
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder([FromForm] OrderInputViewModel orderModel)
        {
            return View();
        }
    }
}
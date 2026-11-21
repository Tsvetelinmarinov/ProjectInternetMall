using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Areas.Administration.ViewModels.OrderProduct;
using EMall.Core.ServiceModels;
using EMall.Core.Services.Interfaces;
using EMall.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the orders menu in the admin panel.
    /// </summary>
    public class OrdersController(IOrderService orderService) : AdminCoreController
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] string? sortBy, 
            [FromQuery] string? sortDir
        ){
            sortBy ??= "date";
            sortDir ??= "asc";

            ViewBag.SortBy = sortBy;
            ViewBag.SortDir = sortDir;

            var orderServiceModels 
                = await orderService.GetAllOrders(sortBy, sortDir);

            var orderViewModels = orderServiceModels
                .Select((orderServiceModel) => new OrderViewModel
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

            var orderViewModel = new OrderViewModel
            {
                OrderId = orderServiceModel.OrderId,
                CustomerName = orderServiceModel.CustomerName,
                CustomerEmail = orderServiceModel.CustomerEmail,
                AddressForShipping = orderServiceModel.AddressForShipping,
                Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                CreatedOn = orderServiceModel.CreatedOn,
                TotalPrice = orderServiceModel.TotalPrice,
                Products = MapProducts(orderServiceModel.Products)
            };

            return View(orderViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder([FromRoute] int? id)
        {
            var orderServiceModel = await orderService.FindOrderById(id);

            var orderViewModel = new OrderViewModel
            {
                OrderId = orderServiceModel.OrderId,
                CustomerName = orderServiceModel.CustomerName,
                CustomerEmail = orderServiceModel.CustomerEmail,
                AddressForShipping = orderServiceModel.AddressForShipping,
                Status = Enum.Parse<OrderStatus>(orderServiceModel.Status),
                CreatedOn = orderServiceModel.CreatedOn,
                TotalPrice = orderServiceModel.TotalPrice,
                Products = MapProducts(orderServiceModel.Products)
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(
            [FromRoute] int id,
            [FromForm] OrderViewModel orderModel
        ){
            var persistedOrder = await orderService.FindOrderById(id);
            orderModel.Products = MapProducts(persistedOrder.Products);

            OrderServiceModel orderServiceModel = new()
            {
                OrderId = orderModel.OrderId,
                AddressForShipping = orderModel.AddressForShipping,
                CreatedOn = orderModel.CreatedOn,
                CustomerEmail = orderModel.CustomerEmail,
                CustomerName = orderModel.CustomerName,
                Products = [.. orderModel
                    .Products!
                    .Select((productViewModel) => new OrderProductServiceModel()
                    {
                        ProductId = productViewModel.ProductId,
                        OrderId = productViewModel.OrderId,
                        Product = new()
                        {
                            ProductName = productViewModel.Product!.ProductName,
                            Price = productViewModel.Product!.Price,
                            Category = new CategoryServiceModel()
                            {
                                CategoryName = productViewModel.Product.Category!.CategoryName,
                                CategoryDescription = productViewModel.Product.Category!.CategoryDescription,
                                Color = productViewModel.Product.Category!.Color,
                                Icon = productViewModel.Product.Category!.Icon
                            },
                            Description = productViewModel.Product.Description,
                            Claims = productViewModel
                                .Product
                                .Claims
                                .Select((claims) => new ProductClaimServiceModel
                                {
                                    ClaimName = claims.ClaimName,
                                    ClaimValue = claims.ClaimValue
                                })
                        }
                    })
                ],
                TotalPrice = orderModel.TotalPrice,
                Status = orderModel.Status.ToString()
            };

            bool successUpdating = await orderService.UpdateOrder(orderServiceModel);
            if (successUpdating is false)
            {
                this.TempData["UpdateOprFailed"] = $"Unsuccessfully updating of order with ID {orderServiceModel.OrderId}!";
            }

            this.TempData["UpdateOprOk"] = $"Successfully updated order with ID {orderServiceModel.OrderId}!";
            return this.RedirectToAction("Index", "Orders");
        }

        /// <summary>
        ///  Helpers functions that map collection of 
        ///  OrderProductServiceModels to OrderProductViewModels.
        /// </summary>
        /// <param name="products">
        ///  The OrderProduct service models.
        /// </param>
        /// <returns>
        ///  Collection of <see cref="OrderProductViewModel"/> mapped from the service models.
        /// </returns>
        private static List<OrderProductViewModel>? MapProducts(IEnumerable<OrderProductServiceModel>? products)
            => products?.Select(orderProduct => new OrderProductViewModel
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Product = orderProduct.Product is null
                    ? null
                    : new ProductViewModel
                    {
                        ProductName = orderProduct.Product.ProductName,
                        Description = orderProduct.Product.Description,
                        Claims = orderProduct.Product.Claims.Select(claim => new ProductClaimViewModel
                        {
                            ClaimName = claim.ClaimName,
                            ClaimValue = claim.ClaimValue
                        }),
                        Price = orderProduct.Product.Price,
                        Category = orderProduct.Product.Category is null
                            ? null
                            : new CategoryViewModel
                            {
                                CategoryName = orderProduct.Product.Category.CategoryName,
                                CategoryDescription = orderProduct.Product.Category.CategoryDescription,
                                Icon = orderProduct.Product.Category.Icon,
                                Color = orderProduct.Product.Category.Color
                            }
                    }
            }).ToList();
    }
}
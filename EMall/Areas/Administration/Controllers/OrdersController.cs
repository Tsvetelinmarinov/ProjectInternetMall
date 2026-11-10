using EMall.Areas.Administration.ViewModels.Order;
using EMall.Areas.Administration.ViewModels.Order.Common;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the orders menu in the admin panel.
    /// </summary>
    
    public class OrdersController : AdminCoreController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(
                new List<OrderOutputViewModel>
                {
                    new()
                    {
                        CustomerName = "Angel Snopf",
                        OrderId = 1,
                        CreatedOn = DateTime.Now,
                        Status = OrderStatus.Cancelled,
                        TotalPrice = 25.55M
                    },
                    new()
                    {
                        CustomerName = "Tsvetelin Marinov",
                        OrderId = 2,
                        CreatedOn = DateTime.Now,
                        Status = OrderStatus.Processing,
                        TotalPrice = 666M
                    },
                    new()
                    {
                        CustomerName = "Martina Pavlova",
                        OrderId = 3,
                        CreatedOn = DateTime.Now,
                        Status = OrderStatus.Shipped,
                        TotalPrice = 666M
                    }
                }
            );
        }
    }
}
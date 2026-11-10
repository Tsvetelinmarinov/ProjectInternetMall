using EMall.Areas.Administration.ViewModels.Order.Common;

namespace EMall.Areas.Administration.ViewModels.Order
{
    /// <summary>
    ///  Specifies an order.
    /// </summary>
    public class OrderOutputViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
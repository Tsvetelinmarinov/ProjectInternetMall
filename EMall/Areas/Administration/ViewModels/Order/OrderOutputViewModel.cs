using EMall.Areas.Administration.ViewModels.Order.Common;

namespace EMall.Areas.Administration.ViewModels.Order
{
    public class OrderOutputViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
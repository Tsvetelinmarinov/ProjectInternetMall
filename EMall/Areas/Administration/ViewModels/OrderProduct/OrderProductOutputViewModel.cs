using EMall.ViewModels;

namespace EMall.Areas.Administration.ViewModels.OrderProduct
{
    public class OrderProductOutputViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel? Product { get; set; }
    }
}
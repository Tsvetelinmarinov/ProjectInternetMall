using EMall.ViewModels.Output;

namespace EMall.Areas.Administration.ViewModels.OrderProduct
{
    public class OrderProductOutputViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductOutputViewModel? Product { get; set; }
    }
}
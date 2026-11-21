namespace EMall.Core.ServiceModels
{
    public class OrderProductServiceModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductServiceModel? Product { get; set; }
    }
}
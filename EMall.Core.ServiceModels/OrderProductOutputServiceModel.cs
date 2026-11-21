namespace EMall.Core.ServiceModels
{
    public class OrderProductOutputServiceModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductServiceModel? Product { get; set; }
    }
}
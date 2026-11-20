namespace EMall.Core.ServiceModels.Output
{
    public class OrderProductOutputServiceModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductOutputServiceModel? Product { get; set; }
    }
}
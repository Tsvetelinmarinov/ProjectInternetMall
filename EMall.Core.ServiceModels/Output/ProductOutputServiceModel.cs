using EMall.Data.EntityModels;

namespace EMall.Core.ServiceModels.Output
{
    /// <summary>
    ///  Defines Product output service model.
    /// </summary>
    public class ProductOutputServiceModel
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ProductClaimOutputServiceModel> Claims { get; set; } = [];
        public double Price { get; set; }
        public CategoryOutputServiceModel? Category { get; set; }
    }
}
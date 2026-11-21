using EMall.Data.EntityModels;

namespace EMall.Core.ServiceModels
{
    /// <summary>
    ///  Defines Product output service model.
    /// </summary>
    public class ProductServiceModel
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ProductClaimServiceModel> Claims { get; set; } = [];
        public double Price { get; set; }
        public CategoryServiceModel? Category { get; set; }
    }
}
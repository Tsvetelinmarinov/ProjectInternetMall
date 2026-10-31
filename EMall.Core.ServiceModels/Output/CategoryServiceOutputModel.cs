using EMall.Data.EntityModels;

namespace EMall.Core.ServiceModels.Output
{
    /// <summary>
    ///  Output service model Category.
    /// </summary>
    public class CategoryServiceOutputModel
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public required string Icon { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
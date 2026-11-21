namespace EMall.Core.ServiceModels
{
    /// <summary>
    ///  Output service model Category.
    /// </summary>
    public class CategoryServiceModel
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public required string Icon { get; set; }
        public required string Color { get; set; }
        public IEnumerable<ProductServiceModel>? Products { get; set; }
    }
}
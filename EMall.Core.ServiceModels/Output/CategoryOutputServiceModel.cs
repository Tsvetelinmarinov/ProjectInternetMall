namespace EMall.Core.ServiceModels.Output
{
    /// <summary>
    ///  Output service model Category.
    /// </summary>
    public class CategoryOutputServiceModel
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public required string Icon { get; set; }
        public required string Color { get; set; }
        public IEnumerable<ProductOutputServiceModel>? Products { get; set; }
    }
}
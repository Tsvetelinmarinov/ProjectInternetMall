namespace EMall.ViewModels.Output
{
    /// <summary>
    ///  Defines Category view model.
    /// </summary>
    public class CategoryOutputViewModel
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public required string Icon { get; set; }
        public IEnumerable<ProductOutputViewModel>? Products { get; set; }
    }
}
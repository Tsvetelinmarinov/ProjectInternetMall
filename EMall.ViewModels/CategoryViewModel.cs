namespace EMall.ViewModels
{
    /// <summary>
    ///  Defines Category view model.
    /// </summary>
    public class CategoryViewModel
    {
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public required string Icon { get; set; }
        public required string Color { get; set; }
        public IEnumerable<ProductViewModel>? Products { get; set; }
    }
}
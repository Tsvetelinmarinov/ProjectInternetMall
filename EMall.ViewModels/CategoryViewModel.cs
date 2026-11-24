namespace EMall.ViewModels
{
    /// <summary>
    ///  Defines Category view model.
    /// </summary>
    public class CategoryViewModel
    {
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public IEnumerable<ProductViewModel>? Products { get; set; }
    }
}
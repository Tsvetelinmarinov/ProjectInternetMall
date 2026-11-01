namespace EMall.Areas.Administration.ViewModels
{
    public class CategoryStatViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Color { get; set; } = "#2250F5";
        public int ProductsCount { get; set; }
    }
}

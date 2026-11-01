namespace EMall.Areas.Administration.ViewModels
{
    public class LowStockViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
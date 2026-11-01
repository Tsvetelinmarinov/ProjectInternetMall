namespace EMall.Areas.Administration.ViewModels
{
    public class RecentOrderViewModel
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Нова";
    }
}
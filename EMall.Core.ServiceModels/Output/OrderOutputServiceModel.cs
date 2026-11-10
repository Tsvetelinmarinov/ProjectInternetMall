namespace EMall.Core.ServiceModels.Output
{
    public class OrderOutputServiceModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        // The status here will be string from that moment.
        public string Status { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
namespace EMall.Areas.Administration.ViewModels.Order.Common
{
    /// <summary>
    ///  Specifies the status of an order.
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
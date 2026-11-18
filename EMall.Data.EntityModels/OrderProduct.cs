namespace EMall.Data.EntityModels
{
    /// <summary>
    ///  Third mapping table of Orders and Products.
    /// </summary>
    public class OrderProduct
    {
        // Configuration with Fluent in configuration file.
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
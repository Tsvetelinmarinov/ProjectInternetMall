using System.ComponentModel.DataAnnotations.Schema;

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

        [InverseProperty("OrderProducts")]
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
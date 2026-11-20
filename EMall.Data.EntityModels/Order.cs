using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EMall.Data.EntityModels.Common.DbConstants.Validation;

namespace EMall.Data.EntityModels
{
    /// <summary>
    ///  Entity Order - specifies an order in the store.
    /// </summary>
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(
           OrderCustomerNameMinLen , 
           MinimumLength = OrderCustomerNameMaxLen,
           ErrorMessage = InvalidOrderCustomerNameLength
        )]
        public string CustomerName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = null!;

        public string? Status { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Range(OrderPriceMin, OrderPriceMax)]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Required]
        public string AddressForShipping { get; set; } = null!;

        [InverseProperty("Order")]
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
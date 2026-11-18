using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(100, MinimumLength = 2)]
        public string CustomerName { get; set; } = string.Empty;

        public string? Status { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
using EMall.Areas.Administration.ViewModels.Order.Common;
using EMall.Areas.Administration.ViewModels.OrderProduct;
using System.ComponentModel.DataAnnotations;
using static EMall.Constants.WebConstants.Validation.OrderInputViewModel;

namespace EMall.Areas.Administration.ViewModels.Order
{
    public class OrderOutputViewModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = CustomerNameIsRequired)]
        [StringLength(
            CustomerNameMaxLen,
            MinimumLength = CustomerNameMinLen,
            ErrorMessage = InvalidCustomerNameLength
        )]
        public required string CustomerName { get; set; }

        [Required(ErrorMessage = CustomerEmailIsRequired)]
        [EmailAddress(ErrorMessage = InvalidCustomerEmail)]
        public required string CustomerEmail { get; set; }

        [Required(ErrorMessage = ShippingAddressIsRequired)]
        [StringLength(
            ShippingAddressMaxLen,
            MinimumLength = ShippingAddressMinLen,
            ErrorMessage = InvalidShippingAddressLength
        )]
        public string AddressForShipping { get; set; } = string.Empty;

        public OrderStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = PriceIsRequired)]
        [Range(
            PriceMin,
            PriceMax,
            ErrorMessage = InvalidPriceRange
        )]
        public decimal TotalPrice { get; set; }

        public List<OrderProductOutputViewModel>? Products { get; set; }
    }
}
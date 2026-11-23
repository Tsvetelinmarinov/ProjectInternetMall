using System.ComponentModel.DataAnnotations;
using static EMall.ViewModels.ValidationConstants.WebValidationConstants.ProductViewModelRelated;

namespace EMall.ViewModels
{
    /// <summary>
    ///  Defines Product view model.
    /// </summary>
    public class ProductViewModel
    {
        [Required(ErrorMessage = NameIsRequired)]
        [StringLength(
            NameMaxLen,
            MinimumLength = NameMinLen,
            ErrorMessage = InvalidNameLength
        )]
        public required string ProductName { get; set; }

        [Required(ErrorMessage = DescriptionIsRequired)]
        [StringLength(
            DescriptionMaxLen,
            MinimumLength = DescriptionMinLen,
            ErrorMessage = InvalidDescriptionLength
        )]
        public required string Description { get; set; }

        public IEnumerable<ProductClaimViewModel> Claims { get; set; } = [];

        [Required(ErrorMessage = PriceIsRequired)]
        [Range(
            PriceMin,
            PriceMax,
            ErrorMessage = InvalidPriceRange
        )]
        public double Price { get; set; }

        [Required(ErrorMessage = CategoryIsRequired)]
        public required CategoryViewModel Category { get; set; }
    }
}
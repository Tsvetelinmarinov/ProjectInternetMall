using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EMall.Data.EntityModels.Common.DbConstants.Validation;

namespace EMall.Data.EntityModels
{
    /// <summary>
    ///  Category entity - specifies product category.
    /// </summary>
    public class Category
    {
        /// <summary>
        ///  The ID of the category.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        ///  The name of the category.
        /// </summary>
        [Required(ErrorMessage = CategoryNameIsRequired)]
        [StringLength(
            CategoryNameMaxLen,
            MinimumLength = CategoryNameMinLen,
            ErrorMessage = InvalidCategoryNameLength
        )]
        public required string CategoryName { get; set; }

        /// <summary>
        ///  The description of the category.
        /// </summary>
        [Required(ErrorMessage = CategoryDescriptionIsRequired)]
        [StringLength(
            CategoryDescriptionMaxLen,
            MinimumLength = CategoryDescriptionMinLen,
            ErrorMessage = InvalidCategoryDescription
        )]
        public required string CategoryDescription { get; set; }

        /// <summary>
        ///  Navigation property to Product entity.
        /// </summary>
        [InverseProperty("Category")]
        public ICollection<Product>? Products { get; set; }
    }
}
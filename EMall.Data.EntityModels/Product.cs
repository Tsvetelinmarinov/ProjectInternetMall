using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EMall.Data.EntityModels.Common.DbConstants.Validation;
using Microsoft.Data.SqlTypes;

namespace EMall.Data.EntityModels
{
    /// <summary>
    ///  Product entity model.
    /// </summary>
    public class Product
    {
        /// <summary>
        ///  The ID of the product.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        ///  The name of the product.
        /// </summary>
        [Required(ErrorMessage = ProductNameIsRequired)]
        [StringLength(
            ProductNameMaxLen,
            MinimumLength = ProductNameMinLen,
            ErrorMessage = InvalidProductNameLength
        )]
        public required string ProductName { get; set; }

        /// <summary>
        ///  The description of the product.
        /// </summary>
        [StringLength(
            ProductDescriptionMaxLen,
            MinimumLength = ProductDescriptionMinLen,
            ErrorMessage = InvalidProductDescriptionLength
        )]
        public string? Description { get; set; }

        /// <summary>
        ///  The characteristics of the product.
        /// </summary>
        public IList<ProductClaim> Claims
        {
            get;
            set;
        } = [];

        /// <summary>
        ///  The price of the product.
        /// </summary>
        [Range(ProductPriceMin, ProductPriceMax)]
        public double Price { get; set; }

        /// <summary>
        ///  Foreign key to table Categories -> CategoryId.
        /// </summary>
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        ///  Navigation property to Category.
        /// </summary>
        [InverseProperty("Products")]
        public Category? Category { get; set; }

        /// <summary>
        ///  Property/column for soft delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///  Vector for semantic search.
        /// </summary>
        [Column(TypeName = "VECTOR(768)")] 
        public SqlVector<float> Vector { get; set; }
    }
}
namespace EMall.ViewModels.ValidationConstants
{
    /// <summary>
    ///  Provides constants for validation of WebMVC layer view models.
    /// </summary>
    internal static class WebValidationConstants
    {
        /// <summary>
        ///  Provides constants for validation of ProductViewModel.
        /// </summary>
        internal static class ProductViewModelRelated
        {
            internal const int NameMinLen = 2;
            internal const int NameMaxLen = 100;
            internal const int DescriptionMinLen = 20;
            internal const int DescriptionMaxLen = 6000;

            internal const double PriceMin = 0.00;
            internal const double PriceMax = 100_000;

            internal const string InvalidNameLength
                = "The length of the product name should be between {2} and {1} characters";

            internal const string InvalidDescriptionLength
                = "The length of the product description text should be between {2} and {1} characters";

            internal const string InvalidPriceRange
                = "The price of the product should be between {1} and {2} euro";

            internal const string NameIsRequired = "Product name is required";
            internal const string DescriptionIsRequired = "Product description is required";
            internal const string PriceIsRequired = "Product price is required";
            internal const string CategoryIsRequired = "Product category is required";
        }

        /// <summary>
        ///  Provides common constants for ProductViewModelModelBinder custom binder.
        /// </summary>
        internal static class ProductViewModelModelBinderRelated
        {
            internal const string NullBindingContext = "The context of binding is null!";
            internal const string InvalidClaim = "The claim cannot be null, empty or consider only from white spaces.";
            internal const string InvalidProductName 
                = "The product name cannot be null, empty or consider only from white spaces.";
            internal const string InvalidProductDescription
                = "The product description cannot be null, empty or consider only from white spaces.";
            internal const string InvalidProductPrice 
                = "The product price cannot be null, empty or consider only from white spaces.";
            internal const string InvalidProductCategory 
                = "The product category name cannot be null, empty or consider only from white spaces.";

            internal const string UnsuccessfullParsingOfPrice = "Unsuccessfull parsing of product price from the HTTP request!";
        }

        /// <summary>
        ///  Provides common constants for ProductViewModelModelBinderProvider model binder provider.
        /// </summary>
        internal static class ProductViewModelModelBinderProviderRelated
        {
            internal const string NullProviderContext = "The ModelBinderProviderContext is null!";
        }
    }
}
namespace EMall.Data.EntityModels.Common
{
    /// <summary>
    ///  Provides constants for exceptions, validation etc. for Database layer types.
    /// </summary>
    public static class DbConstants
    {
        /// <summary>
        ///  Provides constants for entity validation.
        /// </summary>
        public static class Validation
        {
            #region Entity Product

            public const int ProductNameMinLen = 1;
            public const int ProductNameMaxLen = 100;
            public const int ProductDescriptionMaxLen = 5000;
            public const int ProductDescriptionMinLen = 20;

            public const double ProductPriceMin = 0.00;
            public const double ProductPriceMax = double.MaxValue;

            public const string ProductNameIsRequired = "Product name is required!";
            public const string InvalidProductNameLength = "Product name should be between {2} and {1} characters.";
            public const string InvalidProductDescriptionLength = "Product description should be between {1} and {2} characters.";

            #endregion
            #region Entity Category

            public const int CategoryNameMinLen = 2;
            public const int CategoryNameMaxLen = 40;

            public const string InvalidCategoryNameLength = "Category name should be between {2} and {1} characters.";
            public const string CategoryNameIsRequired = "Category name is required!";

            #endregion
        }
    }
}
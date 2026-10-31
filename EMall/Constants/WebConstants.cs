namespace EMall.Constants
{
    /// <summary>
    ///  Provides constants for members of WebMVC layer.
    /// </summary>
    public static class WebConstants
    {
        /// <summary>
        ///  Constants that is used in HomeController.
        /// </summary>
        public static class CatalogController
        {
            // Prefix for all errors related to AutoMapper.
            private const string AutoMapper = "AutoMapper error:";

            public const string UnableToMapCategoryOutputServiceModelsToCategoryOutputViewModels
                = $"{AutoMapper}Unable to map CategoryOutputServiceModels to CategoryOutputViewModels.";
        }
    }
}
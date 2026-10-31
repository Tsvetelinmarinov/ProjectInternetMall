namespace EMall.Core.Common
{
    /// <summary>
    ///  Provides constants for exceptions, validation etc. for Service layer types.
    /// </summary>
    public static class ServiceConstants
    {
        /// <summary>
        ///  Provides constants for classes related to Category entity.
        /// </summary>
        public static class CategoryRelated
        {
            // Default prefix for all error related to AutoMapper.
            private const string AutoMapper = "AutoMapper error: ";

            public const string UnableToMapCategoryToCategoryServiceOutputModel
                = $"{AutoMapper}Unable to map Category to CategoryServiceOutputModel.";
        }
    }
}
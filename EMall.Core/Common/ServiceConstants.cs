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

        /// <summary>
        ///  Provides constants for classes related to Order entity.
        /// </summary>
        public static class OrderRelated
        {
            public const string UnableToCastIAsyncEnumerableToIEnumerable
                = "Unable to cast IAsyncEnumerable to IEnumerable!";

            public const string UnableToMapOrdersToOrderOutputServiceModels
                = "Unable to map Orders to OrderOutputServiceModels!";

            public const string SortingKeyCannotBeNull = "Sorting keyword can not be null!";
            public const string SortingDirectionKeyCannotBeNull = "Sorting direction keyword can not be null!";
            public const string InvalidSortingKey = "Unrecognized sorting keyword!";
            public const string InvalidSortingDirectoryKey = "Unrecognized sorting directory keyword!";

            public const string UnableToMapOrderToOrderOutputServiceModel
                = "Unable to map Order to OrderOutputServiceModel!";

            public const string InvalidOrderId = "Invalid order ID! Something went wrong!";
        }
    }
}
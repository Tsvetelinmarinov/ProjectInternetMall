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

        /// <summary>
        ///  Provides common constants that are not related 
        ///  to any entity, service, controller or DTO.
        /// </summary>
        public static class Common
        {
            public const string NoDbConnectionKey = "Connection string 'DefaultConnection' not found.";

            public const string NoAdminEmail 
                = "Unable to retrieve the administrator email for setting up the administrator profile!";

            public const string NoAdminPassword 
                = "Unable to retrieve the administrator password for setting up the administrator profile!";

            public const string SuccessAdminCreate
                = "Successfully created administrator profile with email {0}!";

            public const string UnableToMigrateOnStartUp 
                = "Unsuccessful startup migration! Error message: {0}\n At: {1}\n";

            public const string UnableToCreateAdminRole 
                = "Unable to create role {0} in table AspNetRoles, because of system error.";

            public const string SuccessfullyCreateRole = "Successfully created role {0}!";
            public const string NoConfiguredDeveloperNameForXDeveloper = "No configured developer name!";
        }

        /// <summary>
        ///  Provides classes with entities names that provides validation constants for it.
        /// </summary>
        public static class Validation
        {
            /// <summary>
            ///  Provides validation constants for OrderOutputViewModel.
            /// </summary>
            public static class OrderOutputViewModel
            {
                public const int CustomerNameMinLen = 2;
                public const int CustomerNameMaxLen = 100;

                public const string CustomerNameIsRequired = "Customer`s name is required!";
                public const string InvalidCustomerNameLength = "Customer`s name should be between {2} and {1} characters!";
            }
        }
    }
}
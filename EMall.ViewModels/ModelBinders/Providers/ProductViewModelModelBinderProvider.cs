using Microsoft.AspNetCore.Mvc.ModelBinding;
using static EMall.ViewModels.ValidationConstants.WebValidationConstants.ProductViewModelModelBinderProviderRelated;

namespace EMall.ViewModels.ModelBinders.Providers
{
    /// <summary>
    ///  Provides the ProductViewModelModelBinder when 
    ///  ASP.NET needs to bind ProductViewModel.
    /// </summary>
    public class ProductViewModelModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context is null)
            {
                throw new SystemException(NullProviderContext);
            }

            // Check if we bind ProductViewModel ?
            // If not - returns null, so ASP.NET continue to the next provider in the list.
            if (context.Metadata.UnderlyingOrModelType != typeof(ProductViewModel))
            {
                return null;
            }

            return new ProductViewModelModelBinder();
        }
    }
}
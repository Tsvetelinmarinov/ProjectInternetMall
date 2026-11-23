using EMall.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static EMall.ViewModels.ValidationConstants.WebValidationConstants.ProductViewModelModelBinderRelated;

namespace EMall.ViewModels.ModelBinders
{
    /// <summary>
    ///  Binds ProductViewModel correctly from HTTP request.
    /// </summary>
    /*
     * Налага се да направя този ModelBinder, защото е невъзможно този да .NET да се справи с 16 полета
     * за клейма да ги свърже в ProductClaimViewModel-и, и да ги сложи и в колекция.
     */
    public class ProductViewModelModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Check the context with the HTTP request first.
            if (bindingContext is null)
            {
                throw new WebException(NullBindingContext);
            }

            // Get the value provider that reads the stream of the request body.
            var valueProvider = bindingContext.ValueProvider;

            // Get the raw data from the request:
            // Product name + description + price.
            var productNameRaw = valueProvider.GetValue("ProductName").FirstValue;
            var productDescriptionRaw = valueProvider.GetValue("ProductDescription").FirstValue;
            var productPriceRaw = valueProvider.GetValue("ProductPrice").FirstValue;

            // First claim.
            var claim1Key = valueProvider.GetValue("claim-name-0").FirstValue;
            var claim1Value = valueProvider.GetValue("claim-value-0").FirstValue;

            // Second claim.
            var claim2Key = valueProvider.GetValue("claim-name-1").FirstValue;
            var claim2Value = valueProvider.GetValue("claim-value-1").FirstValue;

            // Third claim.
            var claim3Key = valueProvider.GetValue("claim-name-2").FirstValue;
            var claim3Value = valueProvider.GetValue("claim-value-2").FirstValue;

            // Fourth claim.
            var claim4Key = valueProvider.GetValue("claim-name-3").FirstValue;
            var claim4Value = valueProvider.GetValue("claim-value-3").FirstValue;

            // Fifth claim.
            var claim5Key = valueProvider.GetValue("claim-name-4").FirstValue;
            var claim5Value = valueProvider.GetValue("claim-value-4").FirstValue;

            // Sixst claim.
            var claim6Key = valueProvider.GetValue("claim-name-5").FirstValue;
            var claim6Value = valueProvider.GetValue("claim-value-5").FirstValue;

            // Seventh claim.
            var claim7Key = valueProvider.GetValue("claim-name-6").FirstValue;
            var claim7Value = valueProvider.GetValue("claim-value-6").FirstValue;

            // Eight claim.
            var claim8Key = valueProvider.GetValue("claim-name-7").FirstValue;
            var claim8Value = valueProvider.GetValue("claim-value-7").FirstValue;

            // Collect all claims keys and value and check them all for null or white spaces.
            List<string> rawClaimsMixed = [
                claim1Key, claim2Key, claim3Key, claim4Key,
                claim5Key, claim6Key, claim7Key, claim8Key,
                claim1Value, claim2Value, claim3Value, claim4Value,
                claim5Value, claim6Value, claim7Value, claim8Value
            ];
        }
    }
}
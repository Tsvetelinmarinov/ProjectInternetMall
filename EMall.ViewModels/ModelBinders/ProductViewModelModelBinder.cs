using EMall.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static EMall.ViewModels.ValidationConstants.WebValidationConstants.ProductViewModelModelBinderRelated;

namespace EMall.ViewModels.ModelBinders
{
    /// <summary>
    ///  Binds ProductViewModel correctly from HTTP request.
    /// </summary>
    /*
     * Налага се да направя този ModelBinder, защото е невъзможно този на .NET да се справи с 16 полета
     * за клейма да ги свърже в ProductClaimViewModel-и по двойки, и да ги сложи и в колекция.
     * Не може също така и да създаде цяла категория(CategoryViewModel) по името, което идва от заявката.
     */
    public class ProductViewModelModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Get the value provider that reads the stream of the request body.
            var valueProvider = this.GetCurrentRawValuesProvider(bindingContext);

            // Get the raw data from the request:
            (string RawName, string RawDescription, string RawPrice, string CategoryName) productTrio
                = this.GetProductNameWithDescriptionAndPrice(bindingContext);

            var claims = this.CreateClaimModelsFromRawClaimsData(bindingContext);

            // Convert the data types to needed ones.
            // The request data(except claims) now is raw - as string.
            string name = productTrio.RawName;
            string description = productTrio.RawDescription;
            string categoryName = productTrio.CategoryName;
            bool successfullParsingOfPrice = double.TryParse(productTrio.RawPrice, out double price);
            
            if (successfullParsingOfPrice is false)
            {
                bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                    productTrio.RawPrice,
                    UnsuccessfullParsingOfPrice
                );

                if (successErrorAdding is false)
                {
                    Console.WriteLine(UnsuccessfullParsingOfPrice);
                } 
            }

            // Create the new ProductViewModel.
            var model = new ProductViewModel
            {
                ProductName = name,
                Price = price,
                Description = description,
                Category = new() //=> Data for here will be retrieved from the
                {                             //=> database in the controller, not here ..
                    CategoryName = categoryName,
                    CategoryDescription = null,
                    Color = null,
                    Icon = null
                },
                Claims = claims
            };

            // Set an instance of the new created model(ProductViewModel) in the Model property.
            // Later the controller will take it from there and will use it as parameter.
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        #region Private Helper Methods

        // Gets the value provider for the current request.
        private IValueProvider GetCurrentRawValuesProvider(ModelBindingContext bindingContext)
        {
            // Check the context with the HTTP request first.
            if (bindingContext is null)
            {
                throw new WebException(NullBindingContext);
            }

            // Get the value provider that reads the stream of the request body.
            return bindingContext.ValueProvider;
        }

        // Get the product name, description and price as raw string values from 
        // the HTTP request body.
        private (string Name, string Description, string Price, string CategoryName) GetProductNameWithDescriptionAndPrice(
            ModelBindingContext bindingContext
        ){
            // Product name + description + price.
            var productNameRaw = bindingContext.ValueProvider.GetValue("ProductName").FirstValue;
            var productDescriptionRaw = bindingContext.ValueProvider.GetValue("ProductDescription").FirstValue;
            var productPriceRaw = bindingContext.ValueProvider.GetValue("ProductPrice").FirstValue;
            var productCategoryRaw = bindingContext.ValueProvider.GetValue("ProductCategory").FirstValue;

            // Check the raw product name, desctiption and price for null.
            if (string.IsNullOrWhiteSpace(productNameRaw))
            {
                bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                        productNameRaw,
                        InvalidProductName
                );

                // If the error has not been added, that's means we reach
                // maximum limit of added errors.
                if (successErrorAdding is false)
                {
                    Console.WriteLine(InvalidProductName);
                }
            }

            if (string.IsNullOrWhiteSpace(productDescriptionRaw))
            {
                bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                        productDescriptionRaw,
                        InvalidProductDescription
                );

                // If the error has not been added, that's means we reach
                // maximum limit of added errors.
                if (successErrorAdding is false)
                {
                    Console.WriteLine(InvalidProductDescription);
                }
            }

            if (string.IsNullOrWhiteSpace(productPriceRaw))
            {
                bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                       productPriceRaw,
                       InvalidProductPrice
                );

                // If the error has not been added, that's means we reach
                // maximum limit of added errors.
                if (successErrorAdding is false)
                {
                    Console.WriteLine(InvalidProductPrice);
                }
            }

            if (string.IsNullOrWhiteSpace(productCategoryRaw))
            {
                bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                        productCategoryRaw,
                        InvalidProductName
                );

                // If the error has not been added, that's means we reach
                // maximum limit of added errors.
                if (successErrorAdding is false)
                {
                    Console.WriteLine(InvalidProductCategory);
                }
            }

            return (
                Name: productNameRaw,  
                Description: productDescriptionRaw,
                Price: productPriceRaw,
                CategoryName: productCategoryRaw
            );
        }

        // Gpoups the claim keys and values in ProductClaimViewModels.
        private IEnumerable<ProductClaimViewModel> CreateClaimModelsFromRawClaimsData(ModelBindingContext bindingContext)
        {
            var rawClaims = (string[])this.GetRawClaimsKeysAndValues(bindingContext);

            var claim1 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[0],
                ClaimValue = rawClaims[1]
            };

            var claim2 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[2],
                ClaimValue = rawClaims[3]
            };

            var claim3 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[4],
                ClaimValue = rawClaims[5]
            };

            var claim4 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[6],
                ClaimValue = rawClaims[7]
            };

            var claim5 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[8],
                ClaimValue = rawClaims[9]
            };

            var claim6 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[10],
                ClaimValue = rawClaims[11]
            };

            var claim7 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[12],
                ClaimValue = rawClaims[13]
            };

            var claim8 = new ProductClaimViewModel
            {
                ClaimName = rawClaims[14],
                ClaimValue = rawClaims[15]
            };

            return [claim1, claim2, claim3, claim4, claim5, claim6, claim7, claim8];
        }

        // Extracts the raw claim keys and values from the request body.
        private IEnumerable<string> GetRawClaimsKeysAndValues(ModelBindingContext bindingContext)
        {
            // First claim.
            var claim1Key = bindingContext.ValueProvider.GetValue("claim-name-0").FirstValue;
            var claim1Value = bindingContext.ValueProvider.GetValue("claim-value-0").FirstValue;

            // Second claim.
            var claim2Key = bindingContext.ValueProvider.GetValue("claim-name-1").FirstValue;
            var claim2Value = bindingContext.ValueProvider.GetValue("claim-value-1").FirstValue;

            // Third claim.
            var claim3Key = bindingContext.ValueProvider.GetValue("claim-name-2").FirstValue;
            var claim3Value = bindingContext.ValueProvider.GetValue("claim-value-2").FirstValue;

            // Fourth claim.
            var claim4Key = bindingContext.ValueProvider.GetValue("claim-name-3").FirstValue;
            var claim4Value = bindingContext.ValueProvider.GetValue("claim-value-3").FirstValue;

            // Fifth claim.
            var claim5Key = bindingContext.ValueProvider.GetValue("claim-name-4").FirstValue;
            var claim5Value = bindingContext.ValueProvider.GetValue("claim-value-4").FirstValue;

            // Sixst claim.
            var claim6Key = bindingContext.ValueProvider.GetValue("claim-name-5").FirstValue;
            var claim6Value = bindingContext.ValueProvider.GetValue("claim-value-5").FirstValue;

            // Seventh claim.
            var claim7Key = bindingContext.ValueProvider.GetValue("claim-name-6").FirstValue;
            var claim7Value = bindingContext.ValueProvider.GetValue("claim-value-6").FirstValue;

            // Eight claim.
            var claim8Key = bindingContext.ValueProvider.GetValue("claim-name-7").FirstValue;
            var claim8Value = bindingContext.ValueProvider.GetValue("claim-value-7").FirstValue;

            // Collect all claims keys and value and check them all for null or white spaces.
            List<string> rawClaimsMixed = [
                claim1Key, claim2Key, claim3Key, claim4Key,
                claim5Key, claim6Key, claim7Key, claim8Key,
                claim1Value, claim2Value, claim3Value, claim4Value,
                claim5Value, claim6Value, claim7Value, claim8Value
            ];

            this.CheckRawClaimsForNullOrWhiteSpace(rawClaimsMixed, bindingContext);

            return rawClaimsMixed;
        }

        // Iterate over an array with raw claim keys and values as strings and
        // checks them for null, emptyness or white space. Add general model error 
        // if some claim value is not valid.
        private void CheckRawClaimsForNullOrWhiteSpace(IEnumerable<string> claims, ModelBindingContext bindingContext)
        {
            foreach (var claimPart in claims)
            {
                if (string.IsNullOrWhiteSpace(claimPart))
                {
                    bool successErrorAdding = bindingContext.ModelState.TryAddModelError(
                        claimPart,
                        InvalidClaim
                    );

                    // If the error has not been added, that's means we reach
                    // maximum limit of added errors.
                    if (successErrorAdding is false)
                    {
                        Console.WriteLine(InvalidClaim); //=> At least write it to the console.
                        continue;
                    }
                }
            }
        }

        #endregion
    }
}
using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Data.EntityModels;

namespace EMall.Core.AutoMapper
{
    // Maps Product entity to Product service output model.
    public class ProductClaimMappingProfile : Profile
    {
        public ProductClaimMappingProfile()
        {
            // Map ProductClaim -> ProductClaimOutputServiceModel.
            this.CreateMap<ProductClaim, ProductClaimServiceModel>()
                .ReverseMap();
        }
    }
}
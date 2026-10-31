using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.ViewModels.Output;

namespace EMall.ViewModels.AutoMapper
{
    public class ProductClaimServiceMappingProfile : Profile
    {
        public ProductClaimServiceMappingProfile()
        {
            // Create mapping for ProductClaimOutputServiceModel -> ProductClaimOutputViewModel.
            this.CreateMap<ProductClaimOutputServiceModel, ProductClaimOutputViewModel>()
                .ReverseMap();
        }
    }
}
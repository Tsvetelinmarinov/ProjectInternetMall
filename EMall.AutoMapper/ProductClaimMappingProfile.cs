using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.ViewModels;

namespace EMall.AutoMapper
{
    public class ProductClaimServiceMappingProfile : Profile
    {
        public ProductClaimServiceMappingProfile()
        {
            // Create mapping for ProductClaimOutputServiceModel -> ProductClaimOutputViewModel.
            this.CreateMap<ProductClaimServiceModel, ProductClaimViewModel>()
                .ReverseMap();
        }
    }
}
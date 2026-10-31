using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.ViewModels.Output;

namespace EMall.ViewModels.AutoMapper
{
    public class ProductServiceMappingProfile : Profile
    {
        public ProductServiceMappingProfile()
        {
            // Create mapping for ProductClaimOutputServiceModel -> ProductCalimOutputViewModel.
            this.CreateMap<ProductClaimOutputServiceModel, ProductOutputViewModel>()
                .ReverseMap();
        }
    }
}
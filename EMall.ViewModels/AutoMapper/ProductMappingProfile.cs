using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.ViewModels.Output;

namespace EMall.ViewModels.AutoMapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Create mapping for ProductClaimOutputServiceModel -> ProductCalimOutputViewModel.
            this.CreateMap<ProductClaimOutputServiceModel, ProductOutputViewModel>()
                .ReverseMap();
        }
    }
}
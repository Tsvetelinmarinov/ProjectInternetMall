using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.ViewModels.Output;

namespace EMall.AutoMapper
{
    public class ProductServiceMappingProfile : Profile
    {
        public ProductServiceMappingProfile()
        {
            // Create mapping for ProductOutputServiceModel -> ProductOutputViewModel.
            this.CreateMap<ProductOutputServiceModel, ProductOutputViewModel>()
                .ReverseMap();
        }
    }
}
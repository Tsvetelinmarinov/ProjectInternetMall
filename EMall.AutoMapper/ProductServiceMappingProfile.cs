using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.ViewModels;

namespace EMall.AutoMapper
{
    public class ProductServiceMappingProfile : Profile
    {
        public ProductServiceMappingProfile()
        {
            // Create mapping for ProductOutputServiceModel -> ProductOutputViewModel.
            this.CreateMap<ProductServiceModel, ProductViewModel>()
                .ReverseMap();
        }
    }
}
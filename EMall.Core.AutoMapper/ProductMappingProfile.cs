using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Data.EntityModels;

namespace EMall.Core.AutoMapper
{
    // Maps Product entity to Product service output model.
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Map Product -> ProductOutputServiceModel.
            this.CreateMap<Product, ProductServiceModel>()
                .ReverseMap();
        }
    }
}
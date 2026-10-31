using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.Data.EntityModels;

namespace EMall.Core.ServiceModels.AutoMapper
{
    // Maps Product entity to Product service output model.
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Map Product -> ProductOutputServiceModel.
            this.CreateMap<Product, ProductOutputServiceModel>()
                .ReverseMap();
        }
    }
}
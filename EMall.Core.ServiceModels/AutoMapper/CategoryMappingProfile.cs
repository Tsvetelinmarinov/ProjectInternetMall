using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.Data.EntityModels;

namespace EMall.Core.ServiceModels.AutoMapper
{
    internal class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Create mapping for Category -> CategoryServiceOutputModel
            this.CreateMap<Category, CategoryServiceOutputModel>()
                .ReverseMap();
        }
    }
}
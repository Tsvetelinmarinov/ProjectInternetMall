using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.ViewModels;

namespace EMall.AutoMapper
{
    public class CategoryServiceMappingProfile : Profile
    {
        public CategoryServiceMappingProfile()
        {
            // Create mapping for CategoryServiceOutputModel -> CategoryOutputViewModel.
            this.CreateMap<CategoryServiceModel, CategoryViewModel>()
                .ReverseMap();
        }
    }
}
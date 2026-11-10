using AutoMapper;
using EMall.Core.ServiceModels.Output;
using EMall.ViewModels.Output;

namespace EMall.AutoMapper
{
    public class CategoryServiceMappingProfile : Profile
    {
        public CategoryServiceMappingProfile()
        {
            // Create mapping for CategoryServiceOutputModel -> CategoryOutputViewModel.
            this.CreateMap<CategoryOutputServiceModel, CategoryOutputViewModel>()
                .ReverseMap();
        }
    }
}
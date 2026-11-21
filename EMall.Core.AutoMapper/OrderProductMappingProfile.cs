using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Data.EntityModels;

namespace EMall.Core.AutoMapper
{
    /// <summary>
    ///  Provides mappings for OrderProduct -> OrderProductOutputServiceModel.
    /// </summary>
    public class OrderProductMappingProfile : Profile
    {
        public OrderProductMappingProfile()
        {
            this.CreateMap<OrderProduct, OrderProductServiceModel>()
                .ReverseMap();
        }
    }
}
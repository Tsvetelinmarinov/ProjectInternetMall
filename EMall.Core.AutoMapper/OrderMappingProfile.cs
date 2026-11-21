using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Data.EntityModels;

namespace EMall.Core.AutoMapper
{
    /// <summary>
    ///  Mapping profile for mapping Order entity -> OrderOutputServiceModel.
    /// </summary>
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            _ = this.CreateMap<Order, OrderServiceModel>()
                .ForMember(
                    destination => destination.Products,
                    options => options.MapFrom(source => source.OrderProducts)
                )
                .ReverseMap();
        }
    }
}
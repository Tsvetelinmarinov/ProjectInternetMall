using AutoMapper;
using EMall.Core.ServiceModels.Output;
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
            _ = this.CreateMap<Order, OrderOutputServiceModel>()
                .ForMember(
                    destination => destination.Products,
                    options => options.MapFrom(source => source.OrderProducts)
                )
                .ReverseMap();
        }
    }
}
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
            this.CreateMap<Order, OrderOutputServiceModel>()
                .ReverseMap();
        }
    }
}
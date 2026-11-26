using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Core.Services.Interfaces;
using EMall.Data.Repositories.Interfaces;

namespace EMall.Core.Services
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) 
        : IProductService
    {
        public Task<bool> AddAsync(ProductServiceModel? productServiceModel)
        {
            throw new NotImplementedException();
        }
    }
}
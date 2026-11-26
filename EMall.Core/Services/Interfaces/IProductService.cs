using AutoMapper;
using EMall.Core.ServiceModels;
using EMall.Data.Repositories.Interfaces;

namespace EMall.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddAsync(ProductServiceModel? productServiceModel);
    }
}
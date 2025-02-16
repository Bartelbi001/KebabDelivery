using KebabDelivery.Application.DTOs;

namespace KebabDelivery.Application.Interfaces.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse?> GetByIdAsync(Guid id);
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> CreateAsync(ProductRequest request);
        Task<ProductResponse> UpdateAsync(Guid id, ProductRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
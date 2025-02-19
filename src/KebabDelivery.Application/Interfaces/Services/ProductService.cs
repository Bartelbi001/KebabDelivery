using KebabDelivery.Application.DTOs;
using KebabDelivery.Application.Interfaces.Services.Interfaces;
using KebabDelivery.Application.Mapping;
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;

namespace KebabDelivery.Application.Interfaces.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest request)
    {
        var result = Product.Create(request.Name, request.Description, request.ImageUrl, request.IsComposite, request.IsVisible);
        if (result.IsFailed)
            throw new Exception(result.Errors.First().Message);

        var product = result.Value;
        await _productRepository.AddAsync(product);
        return product.ToResponse();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return false;

        await _productRepository.DeleteAsync(id);
        return true;
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products
            .Where(p => p.IsVisible)
            .ToResponceList();
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product?.ToResponse();
    }

    public async Task<ProductResponse> UpdateAsync(Guid id, ProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return null;

        product.Update(request.Name, request.Description, request.ImageUrl, request.IsComposite, request.IsVisible);

        await _productRepository.UpdateAsync(product);

        return product.ToResponse();
    }
}
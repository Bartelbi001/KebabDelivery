using FluentValidation;
using KebabDelivery.Application.DTOs;
using KebabDelivery.Application.Interfaces.Services.Interfaces;
using KebabDelivery.Application.Mapping;
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;

namespace KebabDelivery.Application.Interfaces.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<Product> _validator;

    public ProductService(IProductRepository productRepository, IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _validator = validator;
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest request)
    {
        var product = Product.Create(request.Name, request.Description, request.ImageUrl, request.IsComposite, request.IsVisible);

        // Проверяем через FluentValidation
        var validationResult = await _validator.ValidateAsync(product);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _productRepository.AddAsync(product);
        return product.ToResponse();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new KeyNotFoundException($"Продукт с ID {id} не найден.");
        }

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

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new KeyNotFoundException($"Продукт с ID {id} не найден.");
        }

        return product.ToResponse();
    }

    public async Task<ProductResponse> UpdateAsync(Guid id, ProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new KeyNotFoundException($"Продукт с ID {id} не найден.");
        }

        product.Update(request.Name, request.Description, request.ImageUrl, request.IsComposite, request.IsVisible);

        var validationResult = await _validator.ValidateAsync(product);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _productRepository.UpdateAsync(product);

        return product.ToResponse();
    }
}
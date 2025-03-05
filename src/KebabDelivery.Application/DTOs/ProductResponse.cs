using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.DTOs;

public record class ProductResponse(
    Guid Id,
    string Name,
    string Description,
    string ImageUrl,
    bool IsComposite,
    bool IsVisible,
    List<ProductSize> ProductSizes,
    List<ProductIngredient> ProductIngredients);
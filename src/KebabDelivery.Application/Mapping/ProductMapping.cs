using KebabDelivery.Application.DTOs;
using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.Mapping;

public static class ProductMapping
{
    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.ImageUrl,
            product.IsComposite,
            product.IsVisible,
            product.ProductSizes,
            product.ProductIngredients);
    }

    public static List<ProductResponse> ToResponseList(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToResponse()).ToList();
    }
}
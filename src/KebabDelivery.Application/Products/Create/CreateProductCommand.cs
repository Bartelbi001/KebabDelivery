using MediatR;

namespace KebabDelivery.Application.Products;

public record CreateProductCommand(
    string Name,
    int PriceInCents,
    string Currency,
    decimal Calories,
    decimal Proteins,
    decimal Fats,
    decimal Carbohydrates,
    bool IsAlcoholic,
    bool ContainsLactose,
    string? Description,
    string? ImageUrl
) : IRequest<Guid>;
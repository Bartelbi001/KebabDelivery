using KebabDelivery.Domain.Entities;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Application.Products;

public class ProductFactory : IProductFactory
{
    public Product Create(CreateProductCommand command)
    {
        var price = new Price(command.PriceInCents, command.Currency);

        var nutrition = new Nutrition(
            command.Calories,
            command.Proteins,
            command.Fats,
            command.Carbohydrates
        );

        return Product.Create(
            command.Name,
            nutrition,
            price,
            command.IsAlcoholic,
            command.ContainsLactose,
            command.Description,
            command.ImageUrl
        );
    }
}
namespace KebabDelivery.Application.DTOs;

public record IngredientResponse(
    Guid Id,
    string Name,
    decimal Calories,
    decimal Proteins,
    decimal Fats,
    decimal Carbohydrates,
    bool? IsAlcoholic, // NULL = Не применимо
    bool? ContainsLactose); // NULL = Не применимо
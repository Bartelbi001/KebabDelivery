using FluentValidation;
using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.Validators;

public class IngredientValidator : AbstractValidator<Ingredient>
{
    public IngredientValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Поле 'Name' не может быть пустым.")
            .MaximumLength(100).WithMessage("Поле 'Name' не должно превышать 100 символов.");

        RuleFor(i => i.Calories)
            .GreaterThanOrEqualTo(0).WithMessage("Калории не могут быть отрицательными");

        RuleFor(i => i.Proteins)
            .GreaterThanOrEqualTo(0).WithMessage("Белки не могут быть отрицательными");

        RuleFor(i => i.Fats)
            .GreaterThanOrEqualTo(0).WithMessage("Жиры не могут быть отрицательными");

        RuleFor(i => i.Carbohydrates)
            .GreaterThanOrEqualTo(0).WithMessage("Углеводы не могут быть отрицательными");
    }
}
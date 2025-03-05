using FluentValidation;
using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Поле 'Name' не может быть пустым.")
            .MaximumLength(100).WithMessage("Поле 'Name' не должно превышать 100 символов.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Поле 'Description' не может быть пустым.")
            .MaximumLength(500).WithMessage("Поле 'Description' не должно превышать 500 символов.");

        RuleFor(p => p.ImageUrl)
            .NotEmpty().WithMessage("Поле 'ImageUrl' не может быть пустым.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Поле 'ImageUrl' должно содержать корректный URL.");
    }
}
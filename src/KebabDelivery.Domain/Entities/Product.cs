using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; }
    public bool IsComposite { get; private set; }
    public bool IsVisible { get; private set; } = true;
    public List<ProductIngredient> ProductIngredients { get; private set; } = new();
    public List<ProductSize> ProductSizes { get; private set; } = new();


    private Product() { }

    public static Result<Product> Create(string name, string description, string? imageUrl, bool isComposite, bool isVisible)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Название продукта не может быть пустым.");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Fail("Описание продукта не может быть пустым.");

        if (string.IsNullOrWhiteSpace(imageUrl))
            return Result.Fail("URL изображения не может быть пустым.");

        return Result.Ok(new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ImageUrl = imageUrl,
            IsComposite = isComposite,
            IsVisible = isVisible
        });
    }

    public void Update(string name, string description, string imageUrl, bool isComrosite, bool isVisible)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название продукта не может быть пустым.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание продукта не может быть пустым.");

        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("URL изображения не может быть пустым.");

        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        IsComposite = isComrosite;
        IsVisible = isVisible;
    }
}
namespace KebabDelivery.Application.DTOs;

public record class ProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public bool IsComposite { get; init; }
    public bool IsVisible { get; init; }

    // Пустой конструктор (нужен для JSON-десериализации)
    public ProductRequest() { }

    // Основной конструктор (чтобы вручную создавать объект в коде)
    public ProductRequest(string name, string description, string imageUrl, bool isComposite, bool isVisible)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        IsComposite = isComposite;
        IsVisible = isVisible;
    }
}
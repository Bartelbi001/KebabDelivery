namespace KebabDelivery.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public bool IsComposite { get; private set; }
    public bool IsVisible { get; private set; } = true;
    public List<ProductIngredient> ProductIngredients { get; private set; } = new();
    public List<ProductSize> ProductSizes { get; private set; } = new();

    private Product() { }

    private Product(Guid id, string name, string description, string imageUrl, bool isComposite, bool isVisible)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        IsComposite = isComposite;
        IsVisible = isVisible;
    }

    public static Product Create(string name, string description, string imageUrl, bool isComposite, bool isVisible)
    {
        return new Product(Guid.NewGuid(), name, description, imageUrl, isComposite, isVisible);
    }

    public void Update(string name, string description, string imageUrl, bool isComrosite, bool isVisible)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        IsComposite = isComrosite;
        IsVisible = isVisible;
    }
}
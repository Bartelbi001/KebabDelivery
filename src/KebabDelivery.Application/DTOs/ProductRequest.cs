namespace KebabDelivery.Application.DTOs;

public record class ProductRequest(string Name, string Description, string ImageUrl, bool IsComposite, bool IsVisible);
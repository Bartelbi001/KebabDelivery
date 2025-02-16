namespace KebabDelivery.Application.DTOs;

public record class ProductRequest(string Name, decimal Price, string Description);
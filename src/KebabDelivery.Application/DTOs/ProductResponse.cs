namespace KebabDelivery.Application.DTOs;

public record class ProductResponse(Guid Id, string Name, decimal Price, string Description);
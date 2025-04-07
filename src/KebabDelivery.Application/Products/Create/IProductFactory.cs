using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.Products;

public interface IProductFactory
{
    Product Create(CreateProductCommand command);
}
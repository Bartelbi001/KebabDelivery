﻿using KebabDelivery.Domain.Common;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class ProductSize : EntityBase<Guid>
{
    private ProductSize()
    {
    }

    public ProductSize(Guid productId, string name, Measurement size, Price price)
        : base(Guid.NewGuid())
    {
        Guard.AgainstNullOrWhiteSpace(name, "Size name is required.");
        Guard.AgainstNull(size, "Measurement is required.");
        Guard.AgainstNull(price, "Price is required.");

        ProductId = productId;
        Name = name.Trim();
        Size = size;
        Price = price;
    }

    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public Measurement Size { get; private set; }
    public Price Price { get; private set; }

    public void UpdatePrice(Price newPrice)
    {
        Guard.AgainstNull(newPrice, "Price is required.");
        Price = newPrice;
    }
}
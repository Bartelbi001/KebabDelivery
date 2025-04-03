﻿using KebabDelivery.Domain.Base;
using KebabDelivery.Domain.Exceptions;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class Product : Consumable
{
    public Price Price { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsDeleted { get; private set; }

    public List<ProductSize> Sizes { get; private set; } = new();
    public List<ProductIngredient> Ingredients { get; private set; } = new();
    
    private Product() { }

    public Product(string name, Nutrition nutrition, Price price, bool isAlcoholic, bool containsLactose,
        string? description = null, string? imageUrl = null)
        : base(name, isAlcoholic, containsLactose, nutrition)
    {
        Guard.AgainstNull(price, "Price is required.");
        
        Price = price;
        Description = description?.Trim() ?? string.Empty;
        ImageUrl = imageUrl?.Trim();
        IsAvailable = true;
        IsDeleted = false;
    }

    public void SetPrice(Price newPrice)
    {
        Guard.AgainstNull(newPrice, "Price is required.");
        
        Price = newPrice;
        SetUpdatedNow();
    }

    public void ToggleAvailability()
    {
        IsAvailable = !IsAvailable;
        SetUpdatedNow();
    }

    public void Delete()
    {
        IsDeleted = true;
        SetUpdatedNow();
    }

    public void AddSize(ProductSize size)
    {
        Guard.AgainstNull(size, "Size is required.");
        
        Sizes.Add(size);
        SetUpdatedNow();
    }

    public void RemoveSize(Guid sizeId)
    {
        var size = Sizes.FirstOrDefault(s => s.Id == sizeId);
        if (size is null) return;
        
        Sizes.Remove(size);
        SetUpdatedNow();
    }

    public void AddIngredient(ProductIngredient ingredient)
    {
        Guard.AgainstNull(ingredient, "Ingredient is required.");
        if (Ingredients.Any(i => i.IngredientId == ingredient.IngredientId))
            throw new DomainValidationException("Ingredient already added.");
        
        Ingredients.Add(ingredient);
        SetUpdatedNow();
    }

    public void RemoveIngredient(Guid ingredientId)
    {
        var item = Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
        if (item is null) return;
        
        Ingredients.Remove(item);
        SetUpdatedNow();
    }

    public void UpdateIngredients(List<ProductIngredient> newIngredients)
    {
        Guard.AgainstNull(newIngredients, "Ingredient list is required.");
        
        Ingredients = newIngredients;
        SetUpdatedNow();
    }
}
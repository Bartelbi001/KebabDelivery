using FluentValidation;
using KebabDelivery.Application.DTOs;
using KebabDelivery.Application.Interfaces.Services.Interfaces;
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;

namespace KebabDelivery.Application.Interfaces.Services;

public class IngredientService : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IValidator<Ingredient> _validator;

    public IngredientService(IIngredientRepository ingredientRepository, IValidator<Ingredient> validator)
    {
        _ingredientRepository = ingredientRepository;
        _validator = validator;
    }

    public Task<IngredientResponse> CreateAsync(IngredientRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<IngredientResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IngredientResponse?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IngredientResponse> UpdateAsync(Guid id, IngredientRequest ingredientRequest)
    {
        throw new NotImplementedException();
    }
}
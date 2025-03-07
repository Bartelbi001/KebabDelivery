using KebabDelivery.Application.DTOs;

namespace KebabDelivery.Application.Interfaces.Services.Interfaces;

public interface IIngredientService
{
    Task<IngredientResponse?> GetByIdAsync(Guid id);
    Task<List<IngredientResponse>> GetAllAsync();
    Task<IngredientResponse> CreateAsync(IngredientRequest ingredientRequest);
    Task<IngredientResponse> UpdateAsync(Guid id, IngredientRequest ingredientRequest);
    Task<bool> DeleteAsync(Guid id);
}
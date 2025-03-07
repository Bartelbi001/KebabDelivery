using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Infrastructure.Data.Repositories.Interfaces;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(Guid id);
    Task<List<Ingredient>> GetAllAsync();
    Task AddAsync(Ingredient ingredient);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Ingredient ingredient);
}
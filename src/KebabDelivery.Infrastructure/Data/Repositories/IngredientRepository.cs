using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KebabDelivery.Infrastructure.Data.Repositories;

class IngredientRepository : IIngredientRepository
{
    private readonly ApplicationDbContext _context;

    public IngredientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient is null) return;

        _context.Remove(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Ingredient>> GetAllAsync()
    {
        return await _context.Ingredients
            .Include(i => i.ProductIngredients)
            .ToListAsync();
    }

    public async Task<Ingredient?> GetByIdAsync(Guid id)
    {
        return await _context.Ingredients
            .Include(i => i.ProductIngredients)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task UpdateAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }
}
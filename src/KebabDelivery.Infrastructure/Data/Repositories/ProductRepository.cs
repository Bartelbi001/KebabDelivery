using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KebabDelivery.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task UpdateAsync(Product product)
    {
        if (_context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null) return;

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
        }
        else
        {
            _context.Products.Update(product);
        }

        await _context.SaveChangesAsync();
    }


    //public async Task UpdateAsync(Product product)
    //{
    //    _context.Products.Update(product);
    //    await _context.SaveChangesAsync();
    //}
}
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data;
using KebabDelivery.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KebabDelivery.Tests.UnitTests.Repositories;

public class ProductRepositoryTests : IDisposable
{
    public readonly ApplicationDbContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Уникальная БД для каждого теста
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ProductRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose(); // Очищаем контекст после каждого теста
    }

    [Fact]
    public async Task CreateAsync_ValidProduct_SavesToDatabase()
    {
        // Arrange
        var product = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;

        // Act
        await _repository.AddAsync(product);
        await _context.SaveChangesAsync(); // InMemoryDatabase требует явного сохранения

        // Assert
        var savedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        Assert.NotNull(savedProduct);
        Assert.Equal(product.Name, savedProduct.Name);
        Assert.Equal(product.Description, savedProduct.Description);
        Assert.Equal(product.ImageUrl, savedProduct.ImageUrl);
    }

    [Fact]
    public async Task GetByIdAsync_ProductExists_ReturnsProduct()
    {
        // Arrange
        var product = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;
        _context.Products.Add(product);
        await _context.SaveChangesAsync(); // Сохраняем в InMemoryDatabase

        // Act
        var retrievedProduct = await _repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(retrievedProduct);
        Assert.Equal(product.Id, retrievedProduct.Id);
        Assert.Equal(product.Name, retrievedProduct.Name);
        Assert.Equal(product.Description, retrievedProduct.Description);
        Assert.Equal(product.ImageUrl, retrievedProduct.ImageUrl);
    }

    [Fact]
    public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        var productId = Guid.NewGuid(); // ID, которого нет в БД

        // Act
        var retrievedProduct = await _repository.GetByIdAsync(productId);

        // Assert
        Assert.Null(retrievedProduct);
    }

    [Fact]
    public async Task GetAllAsync_ProductsExists_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>()
        {
            Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value,
            Product.Create("Кебаб", "Описание", "https://example.com/kebab.jpg", true, true).Value
        };

        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();

        // Act
        var retrievedProducts = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(retrievedProducts);
        Assert.Equal(2, retrievedProducts.Count);
        Assert.Contains(retrievedProducts, p => p.Name == "Шаурма");
        Assert.Contains(retrievedProducts, p => p.Name == "Кебаб");
    }

    [Fact]
    public async Task GetAllAsync_NoProducts_ReturnsEmptyList()
    {
        // Arrange -- оставляем базу пустой

        // Act
        var retrievedProducts = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(retrievedProducts);
        Assert.Empty(retrievedProducts);
    }

    [Fact]
    public async Task UpdateAsync_ProductExists_UpdatesProduct()
    {
        // Arrange
        var product = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Обновляем данные
        product.Update("Лаваш", "Обновленное описание", "https://example.com/lavash.jpg", false, false);

        // Act
        await _repository.UpdateAsync(product);
        var updatedProduct = await _context.Products.FindAsync(product.Id);

        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal("Лаваш", updatedProduct.Name);
        Assert.Equal("Обновленное описание", updatedProduct.Description);
        Assert.Equal("https://example.com/lavash.jpg", updatedProduct.ImageUrl);
        Assert.False(updatedProduct.IsComposite);
        Assert.False(updatedProduct.IsVisible);
    }

    [Fact]
    public async Task UpdateAsync_ProductDoesNotExist_DoesNothing()
    {
        // Arrange
        var product = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;

        // Act
        await _repository.UpdateAsync(product);
        var retrievedProduct = await _context.Products.FindAsync(product.Id);

        // Assert
        Assert.Null(retrievedProduct);
    }
}
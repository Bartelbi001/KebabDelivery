using KebabDelivery.Application.DTOs;
using KebabDelivery.Application.Interfaces.Services;
using KebabDelivery.Application.Interfaces.Services.Interfaces;
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using Moq;
using Xunit;

namespace KebabDelivery.Tests.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidProduct_ReturnsProductResponse()
    {
        // Arrange
        var request = new ProductRequest("Шаурма", "Острая шаурма с курицей", "https://example.com/shawarma.jpg", true, true);

        var product = Product.Create(request.Name, request.Description, request.ImageUrl, request.IsComposite, request.IsVisible).Value;

        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);

        // Act
        var response = await _productService.CreateAsync(request);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Description, response.Description);
        Assert.Equal(request.ImageUrl, response.ImageUrl);
    }
}
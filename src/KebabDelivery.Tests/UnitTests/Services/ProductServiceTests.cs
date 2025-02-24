using KebabDelivery.Application.DTOs;
using KebabDelivery.Application.Interfaces.Services;
using KebabDelivery.Application.Interfaces.Services.Interfaces;
using KebabDelivery.Domain.Entities;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using Moq;
using System.ComponentModel.DataAnnotations;

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

    public class CreateAsyncTests : ProductServiceTests
    {
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

        [Fact]
        public async Task CreateAsync_InvalidProduct_ThrowValidationException()
        {
            // Arrange
            var request = new ProductRequest("    ", "", "", true, true);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _productService.CreateAsync(request));
        }
    }

    public class GetByIdAsyncTests : ProductServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ProductExists_ReturnsProductResponse()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var response = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(product.Name, response.Name);
            Assert.Equal(product.Description, response.Description);
            Assert.Equal(product.ImageUrl, response.ImageUrl);
        }

        [Fact]
        public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var response = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.Null(response);
        }
    }

    public class GetAllAsyncTests : ProductServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ProductsExist_ReturnsProductList()
        {
            // Arrange
            var products = new List<Product>
            {
                Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value,
                Product.Create("Кебаб", "Описание", "https://example.com/kebab.jpg", true, true).Value
            };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var response = await _productService.GetAllAsync();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
            Assert.Equal("Шаурма", response[0].Name);
            Assert.Equal("Кебаб", response[1].Name);
        }

        [Fact]
        public async Task GetAllAsync_NoProducts_ReturnsEmptyList()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Product>());

            // Act
            var response = await _productService.GetAllAsync();

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }

    public class UpdateAsyncTests : ProductServiceTests
    {
        [Fact]
        public async Task UpdateAsync_ProductExists_UpdatesProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;
            var updatedRequest = new ProductRequest("Лаваш", "Обновленный", "https://example.com/lavash.jpg", true, true);

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _productService.UpdateAsync(productId, updatedRequest);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(updatedRequest.Name, response.Name);
            Assert.Equal(updatedRequest.Description, response.Description);
            Assert.Equal(updatedRequest.ImageUrl, response.ImageUrl);
        }

        [Fact]
        public async Task UpdateAsync_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updatedRequest = new ProductRequest("Лаваш", "Обновленный", "https://example.com/lavash.jpg", true, true);

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var response = await _productService.UpdateAsync(productId, updatedRequest);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task UpdateAsync_InvalidData_ThrowsValidationException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var invalidRequest = new ProductRequest("      ", "", "", true, true);

            var existingProduct = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _productService.UpdateAsync(productId, invalidRequest));
        }
    }

    public class DeleteAsyncTests : ProductServiceTests
    {
        [Fact]
        public async Task DeleteAsync_ProductExists_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = Product.Create("Шаурма", "Описание", "https://example.com/shawarma.jpg", true, true).Value;

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock.Setup(repo => repo.DeleteAsync(productId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _productService.DeleteAsync(productId);

            // Assert
            Assert.True(result);

            // Проверяем, что метод `DeleteAsync` был вызван ровно 1 раз
            _productRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // ACT
            var result = await _productService.DeleteAsync(productId);

            // Assert
            Assert.False(result);

            // Проверяем, что `DeleteAsync(id)` вообще не вызывался
            _productRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
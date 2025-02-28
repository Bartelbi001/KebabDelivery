using FluentAssertions;
using KebabDelivery.Application.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace KebabDelivery.Tests.IntegrationTests.Controllers;

public class ProductControllerTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // Act
        var response = await Client.GetAsync("/api/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenValidRequest()
    {
        // Arrange: создаем валидный запрос
        var request = new ProductRequest
        {
            Name = "Test Kebab",
            Description = "Delicious kebab",
            ImageUrl = "https://example.com/kebab.jpg",
            IsComposite = false,
            IsVisible = true
        };

        // Act: отправляем POST-запрос в API
        var response = await Client.PostAsJsonAsync("/api/products", request);

        // Assert: проверяем, что вернулся статус 201 Created
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Дополнительно: проверяем, что ответ содержит корректные данные
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        createdProduct.Should().NotBeNull();
        createdProduct!.Name.Should().Be(request.Name);
        createdProduct.Description.Should().Be(request.Description);
        createdProduct.ImageUrl.Should().Be(request.ImageUrl);
        createdProduct.IsComposite.Should().Be(request.IsComposite);
        //createdProduct.IsVisible.Should().Be(request.IsVisible);
    }
}
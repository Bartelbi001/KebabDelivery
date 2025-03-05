using FluentAssertions;
using KebabDelivery.Application.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace KebabDelivery.Tests.IntegrationTests.Controllers;

public class ProductControllerTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_ShouldReturnProducts_WhenProductsExist()
    {
        // Arrange
        var request1 = new ProductRequest("Kebab", "Tasty kebab", "https://example.com/kebab.jpg", false, true);
        var request2 = new ProductRequest("Shawarma", "Tasty shawarma", "https://example.com/shawarma.jpg", false, true);

        await Client.PostAsJsonAsync("/api/products", request1);
        await Client.PostAsJsonAsync("/api/products", request2);

        // Act
        var response = await Client.GetAsync("/api/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products.Should().Contain(p => p.Name == "Kebab" && p.Description == "Tasty kebab");
        products.Should().Contain(p => p.Name == "Shawarma" && p.Description == "Tasty shawarma");
    }

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
        var request = new ProductRequest("Test Kebab", "Delicious kebab", "https://example.com/kebab.jpg", false, true);

        // Act: отправляем POST-запрос в API
        var response = await Client.PostAsJsonAsync("/api/products", request);

        // Assert: проверяем, что вернулся статус 201 Created
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Дополнительно: проверяем, что ответ содержит корректные данные
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        createdProduct.Should().NotBeNull();
        createdProduct.Name.Should().Be(request.Name);
        createdProduct.Description.Should().Be(request.Description);
        createdProduct.ImageUrl.Should().Be(request.ImageUrl);
        createdProduct.IsComposite.Should().Be(request.IsComposite);
        createdProduct.IsVisible.Should().Be(request.IsVisible);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        var request = new ProductRequest("", "", "", false, true);

        // Act
        var response = await Client.PostAsJsonAsync("/api/products", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var request = new ProductRequest()
        {
            Name = "Kebab",
            Description = "Delicious kebab",
            ImageUrl = "https://example.com/kebab.jpg",
            IsComposite = false,
            IsVisible = true
        };

        var createResponse = await Client.PostAsJsonAsync("/api/products", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        createdProduct.Should().NotBeNull();

        // Act: отправляем GET-запрос, чтобы получить продукт по id
        var response = await Client.GetAsync($"/api/products/{createdProduct!.Id}");

        // Assert: проверяем, что вернулся статус 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Проверяем, что полученные данные совпадают
        var retrievedProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        retrievedProduct.Should().NotBeNull();
        retrievedProduct.Id.Should().Be(createdProduct.Id);
        retrievedProduct.Name.Should().Be(request.Name);
        retrievedProduct.Description.Should().Be(request.Description);
        retrievedProduct.ImageUrl.Should().Be(request.ImageUrl);
        retrievedProduct.IsComposite.Should().Be(request.IsComposite);
        retrievedProduct.IsVisible.Should().Be(request.IsVisible);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var nonExistentProductId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/api/products/{nonExistentProductId}");

        // Addert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenUpdatedSuccessfully()
    {
        // Arrange
        var request = new ProductRequest("Kebab", "Tasty kebab", "https://example.com/kebab.jpg", false, true);
        var createResponse = await Client.PostAsJsonAsync("/api/products", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        createdProduct.Should().NotBeNull();

        var updatedRequest = new ProductRequest("Kebab", "Tasty kebab", "https://example.com/kebab.jpg", false, true);

        // Act
        var updateResponse = await Client.PutAsJsonAsync($"/api/products/{createdProduct!.Id}", updatedRequest);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Проверяем, что продукт действительно обновился
        var response = await Client.GetAsync($"/api/products/{createdProduct.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var retrievedProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        retrievedProduct.Should().NotBeNull();
        retrievedProduct.Name.Should().Be(updatedRequest.Name);
        retrievedProduct.Description.Should().Be(updatedRequest.Description);
        retrievedProduct.ImageUrl.Should().Be(updatedRequest.ImageUrl);
        retrievedProduct.IsComposite.Should().Be(updatedRequest.IsComposite);
        retrievedProduct.IsVisible.Should().Be(updatedRequest.IsVisible);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var nonExistentProductId = Guid.NewGuid();
        var updatedRequest = new ProductRequest("Updated Kebab", "Even tastier kebab", "https://example.com/updated-kebab.jpg", false, true);

        // Act
        var updateResponse = await Client.PutAsJsonAsync($"/api/products/{nonExistentProductId}", updatedRequest);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenDeletedSuccessfully()
    {
        // Arrange
        var request = new ProductRequest("Kebab", "Tasty kebab", "https://example.com/kebab.jpg", false, true);
        var createResponse = await Client.PostAsJsonAsync("/api/products", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        createdProduct.Should().NotBeNull();

        // Act
        var deleteResponse = await Client.DeleteAsync($"/api/products/{createdProduct!.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Дополнительно: проверяем, что продукт больше не существует
        var getResponse = await Client.GetAsync($"/api/products/{createdProduct.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        // Дополнительно: проверяем, что повторный DELETE выдаёт 404
        var secondDeleteResponse = await Client.DeleteAsync($"/api/products/{createdProduct.Id}");
        secondDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
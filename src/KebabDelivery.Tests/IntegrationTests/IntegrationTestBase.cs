using KebabDelivery.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace KebabDelivery.Tests.IntegrationTests;

public class IntegrationTestBase : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    protected readonly HttpClient Client;
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTestBase()
    {
        // Настраиваем контейнер с PostgreSQL
        _dbContainer = new PostgreSqlBuilder()
            .WithDatabase("TestDb")
            .WithUsername("testuser")
            .WithPassword("testpassword")
            .Build();

        // Создаём тестовый сервер
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Удаляем стандартный DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Добавляем новый DbContext с Testcontainers
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(_dbContainer.GetConnectionString()));
                });
            });

        Client = _factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        // Запускаем контейнер с PostgreSQL
        await _dbContainer.StartAsync();

        // Создаём БД без накатывания миграций
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated(); // Создаёт схему БД без миграций
        }
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
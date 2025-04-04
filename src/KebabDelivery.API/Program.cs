using FluentValidation;
using KebabDelivery.API.Middlewares;
using KebabDelivery.Infrastructure.Data;
using KebabDelivery.Infrastructure.Data.Repositories;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ✅ Настраиваем Serilog из appsettings.json
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

// ✅ Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kebab API",
        Version = "v1",
        Description = "API для управления заказами и меню Kebab кафе"
    });
});

// ✅ Подключаем FluentValidation
// (Автоматически регистрируем все валидаторы из текущей сборки)
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

// ✅ Настраиваем базу данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Добавляем зависимости для DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// ✅ Включаем Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Логирование HTTP-запросов через Serilog
app.UseSerilogRequestLogging();

// ✅ Глобальная обработка ошибок через Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Позволяет тестировать приложение
public partial class Program
{
}
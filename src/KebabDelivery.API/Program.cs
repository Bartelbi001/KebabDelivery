using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kebab API",
        Version = "v1",
        Description = "API ��� ���������� �������� � ���� Kebab ����"
    });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
using KebabDelivery.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

app.UsePresentation();
app.Run();

public partial class Program
{
}
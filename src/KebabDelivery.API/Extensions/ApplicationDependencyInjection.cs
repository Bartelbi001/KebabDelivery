using KebabDelivery.Application.Products;

namespace KebabDelivery.API.Extensions;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<CreateProductCommand>(); });

        return services;
    }
}
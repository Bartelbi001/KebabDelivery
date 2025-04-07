using KebabDelivery.Domain.Entities;
using KebabDelivery.Domain.ValueObjects;
using KebabDelivery.Infrastructure.Data.Repositories.Interfaces;
using MediatR;

namespace KebabDelivery.Application.Products;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var price = new Price(request.Price);

        var product = Product.Create()
    }
}
using Ardalis.Result;
using MediatR;
using Microservice.Example.Core.Abstractions;

namespace Microservice.Example.UseCases.Queries;

public class GetOrderQuery : IRequest<Result<Order>>
{
    public GetOrderQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

internal sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<Order>>
{
    private readonly IReadRepository<Microservice.Example.Core.Aggregates.OrderAggregate.Order> _repository;

    public GetOrderQueryHandler(IReadRepository<Microservice.Example.Core.Aggregates.OrderAggregate.Order> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetOrderSpecification(request.Id);
        
        var order = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        
        return order == null ? Result.NotFound() : Result.Success(order);
    }
}
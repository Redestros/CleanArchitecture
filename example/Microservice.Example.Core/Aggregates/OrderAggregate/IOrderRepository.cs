using Microservice.Example.Core.Abstractions;

namespace Microservice.Example.Core.Aggregates.OrderAggregate;

public interface IOrderRepository : IRepository<Order>
{
    Order Add(Order order);

    void Update(Order order);

    Task<Order?> GetAsync(int orderId);
}
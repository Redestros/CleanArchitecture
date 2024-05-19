using MediatR;
using Microservice.Example.Core.Aggregates.OrderAggregate;

namespace Microservice.Example.Core.Events;

public class OrderShippedDomainEvent : INotification
{
    public OrderShippedDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
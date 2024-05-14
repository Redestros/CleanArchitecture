using MediatR;
using Microservice.Core.Aggregates.OrderAggregate;

namespace Microservice.Core.Events;

public class OrderShippedDomainEvent : INotification
{
    public OrderShippedDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
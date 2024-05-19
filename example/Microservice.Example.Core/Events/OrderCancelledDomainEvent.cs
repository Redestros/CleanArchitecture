using MediatR;
using Microservice.Example.Core.Aggregates.OrderAggregate;

namespace Microservice.Example.Core.Events;

public class OrderCancelledDomainEvent : INotification
{
    public OrderCancelledDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
using MediatR;
using Microservice.Core.Aggregates.OrderAggregate;

namespace Microservice.Core.Events;

public class OrderCancelledDomainEvent : INotification
{
    public OrderCancelledDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
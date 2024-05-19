using MediatR;
using Ordering.Core.Aggregates.OrderAggregate;

namespace Ordering.Core.Events;

public class OrderShippedDomainEvent : INotification
{
    public OrderShippedDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
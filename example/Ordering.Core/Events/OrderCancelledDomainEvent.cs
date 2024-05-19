using MediatR;
using Ordering.Core.Aggregates.OrderAggregate;

namespace Ordering.Core.Events;

public class OrderCancelledDomainEvent : INotification
{
    public OrderCancelledDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
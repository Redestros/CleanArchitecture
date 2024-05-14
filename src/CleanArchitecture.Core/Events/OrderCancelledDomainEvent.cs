using CleanArchitecture.Core.Aggregates.OrderAggregate;
using MediatR;

namespace CleanArchitecture.Core.Events;

public class OrderCancelledDomainEvent : INotification
{
    public OrderCancelledDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
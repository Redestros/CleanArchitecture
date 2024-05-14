using CleanArchitecture.Core.Aggregates.OrderAggregate;
using MediatR;

namespace CleanArchitecture.Core.Events;

public class OrderShippedDomainEvent : INotification
{
    public OrderShippedDomainEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
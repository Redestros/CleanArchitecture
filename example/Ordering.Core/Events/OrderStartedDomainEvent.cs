using MediatR;
using Ordering.Core.Aggregates.OrderAggregate;

namespace Ordering.Core.Events;

public record class OrderStartedDomainEvent(
    Order Order,
    string UserId,
    string UserName,
    int CardTypeId,
    string CardNumber,
    string CardSecurityNumber,
    string CardHolderName,
    DateTime CardExpiration) : INotification;
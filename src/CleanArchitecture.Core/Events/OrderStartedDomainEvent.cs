using CleanArchitecture.Core.Aggregates.OrderAggregate;
using MediatR;

namespace CleanArchitecture.Core.Events;

public record class OrderStartedDomainEvent(
    Order Order,
    string UserId,
    string UserName,
    int CardTypeId,
    string CardNumber,
    string CardSecurityNumber,
    string CardHolderName,
    DateTime CardExpiration) : INotification;
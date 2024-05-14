using MediatR;
using Microservice.Core.Aggregates.OrderAggregate;

namespace Microservice.Core.Events;

public record class OrderStartedDomainEvent(
    Order Order,
    string UserId,
    string UserName,
    int CardTypeId,
    string CardNumber,
    string CardSecurityNumber,
    string CardHolderName,
    DateTime CardExpiration) : INotification;
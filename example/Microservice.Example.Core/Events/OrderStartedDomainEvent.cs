using MediatR;
using Microservice.Example.Core.Aggregates.OrderAggregate;

namespace Microservice.Example.Core.Events;

public record class OrderStartedDomainEvent(
    Order Order,
    string UserId,
    string UserName,
    int CardTypeId,
    string CardNumber,
    string CardSecurityNumber,
    string CardHolderName,
    DateTime CardExpiration) : INotification;
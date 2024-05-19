#region

using MediatR;
using Ordering.Core.Aggregates.BuyerAggregate;

#endregion

namespace Ordering.Core.Events;

public class BuyerAndPaymentMethodVerifiedDomainEvent : INotification
{
    public BuyerAndPaymentMethodVerifiedDomainEvent(Buyer buyer, PaymentMethod payment, int orderId)
    {
        Buyer = buyer;
        Payment = payment;
        OrderId = orderId;
    }

    public Buyer Buyer { get; private set; }
    public PaymentMethod Payment { get; private set; }
    public int OrderId { get; private set; }
}
using Microservice.Example.Core.Abstractions;
using Microservice.Example.Core.Events;

namespace Microservice.Example.Core.Aggregates.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string IdentityGuid { get; private set; }

    private readonly List<PaymentMethod> _paymentMethods;
    public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    #pragma warning disable CS8618
    public Buyer()
    {
    }

    public Buyer(string name)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        _paymentMethods = [];
    }

    public PaymentMethod VerifyOrAddPaymentMethod(
        int cardTypeId, string alias, string cardNumber,
        string securityNumber, string cardHolderName, DateTime expiration, int orderId)
    {
        var existingPayment = _paymentMethods
            .SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));

        if (existingPayment != null)
        {
            AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, existingPayment, orderId));

            return existingPayment;
        }

        var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);

        _paymentMethods.Add(payment);

        AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, payment, orderId));

        return payment;
    }
}
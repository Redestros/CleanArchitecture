using CleanArchitecture.Core.Aggregates.BuyerAggregate;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Exceptions;

namespace CleanArchitecture.Core.Aggregates.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public Address Address { get; private set; }
    public int? BuyerId { get; set; }

    public Buyer Buyer { get; }

    private readonly List<OrderItem> _items;

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    
    public OrderStatus Status { get; private set; }
    
    public int? PaymentId { get; private set; }
    

    protected Order()
    {
        _items = new List<OrderItem>();
    }

    public Order(string userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
            string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
    {
        BuyerId = buyerId;
        PaymentId = paymentMethodId;
        Status = OrderStatus.Submitted;
        Date = DateTime.UtcNow;
        Address = address;
        
        AddOrderStartedDomainEvent(userId, userName, cardTypeId, cardNumber,
                                    cardSecurityNumber, cardHolderName, cardExpiration);
    }
    
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
    {
        var existingOrderForProduct = _items.SingleOrDefault(o => o.ProductId == productId);

        if (existingOrderForProduct != null)
        {
            //if previous line exist modify it with higher discount  and units..
            if (discount > existingOrderForProduct.Discount)
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }

            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            //add validated new order item
            var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
            _items.Add(orderItem);
        }
    }

    public void SetPaymentMethodVerified(int buyerId, int paymentId)
    {
        BuyerId = buyerId;
        PaymentId = paymentId;
    }
    
    public void SetAwaitingValidationStatus()
    {
        if (Status == OrderStatus.Submitted)
        {
            AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _items));
            Status = OrderStatus.AwaitingValidation;
        }
    }

    public void SetStockConfirmedStatus()
    {
        if (Status == OrderStatus.AwaitingValidation)
        {
            AddDomainEvent(new OrderStatusChangedToStockConfirmedDomainEvent(Id));

            Status = OrderStatus.StockConfirmed;
            Description = "All the items were confirmed with available stock.";
        }
    }

    public void SetPaidStatus()
    {
        if (Status == OrderStatus.StockConfirmed)
        {
            AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, Items));

            Status = OrderStatus.Paid;
            Description = "The payment was performed at a simulated \"American Bank checking bank account ending on XX35071\"";
        }
    }

    public void SetShippedStatus()
    {
        if (Status != OrderStatus.Paid)
        {
            StatusChangeException(OrderStatus.Shipped);
        }

        Status = OrderStatus.Shipped;
        Description = "The order was shipped.";
        AddDomainEvent(new OrderShippedDomainEvent(this));
    }

    public void SetCancelledStatus()
    {
        if (Status is OrderStatus.Paid or OrderStatus.Shipped)
        {
            StatusChangeException(OrderStatus.Cancelled);
        }

        Status = OrderStatus.Cancelled;
        Description = $"The order was cancelled.";
        AddDomainEvent(new OrderCancelledDomainEvent(this));
    }

    public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
    {
        if (Status == OrderStatus.AwaitingValidation)
        {
            Status = OrderStatus.Cancelled;

            var itemsStockRejectedProductNames = Items
                .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                .Select(c => c.ProductName);

            var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
            Description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
        }
    }

    private void AddOrderStartedDomainEvent(string userId, string userName, int cardTypeId, string cardNumber,
            string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId,
                                                                    cardNumber, cardSecurityNumber,
                                                                    cardHolderName, cardExpiration);

        this.AddDomainEvent(orderStartedDomainEvent);
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderingDomainException($"Is not possible to change the order status from {Status} to {orderStatusToChange}.");
    }

    public Decimal GetTotal() => _items.Sum(x => x.Units * x.UnitPrice);
}
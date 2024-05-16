using MediatR;
using Microservice.UseCases.Extensions;

namespace Microservice.UseCases.Commands;

public class CreateOrderCommand : IRequest<bool>
{
    private readonly List<OrderItemDto> _orderItems = [];

    public string UserId { get; private set; }

    public string UserName { get; private set; }

    public string City { get; private set; }
    
    public string Street { get; private set; }

    public string State { get; private set; }

    public string Country { get; private set; }

    public string ZipCode { get; private set; }

    public string CardNumber { get; private set; }

    public string CardHolderName { get; private set; }

    public DateTime CardExpiration { get; private set; }

    public string CardSecurityNumber { get; private set; }

    public int CardTypeId { get; private set; }

    public IEnumerable<OrderItemDto> OrderItems => _orderItems;

    #pragma warning disable
    public CreateOrderCommand()
    {
    }

    public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName, string city, string street, string state, string country, string zipcode,
        string cardNumber, string cardHolderName, DateTime cardExpiration,
        string cardSecurityNumber, int cardTypeId)
    {
        _orderItems = basketItems.ToOrderItemsDto().ToList();
        UserId = userId;
        UserName = userName;
        City = city;
        Street = street;
        State = state;
        Country = country;
        ZipCode = zipcode;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        CardTypeId = cardTypeId;
    }
}
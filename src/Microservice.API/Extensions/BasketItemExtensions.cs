using Microservice.API.Application.Commands;

namespace Microservice.API.Extensions;

public static class BasketItemExtensions
{
    public static IEnumerable<OrderItemDto> ToOrderItemsDto(this IEnumerable<BasketItem> basketItems)
    {
        return basketItems.Select(item => item.ToOrderItemDto());
    }

    private static OrderItemDto ToOrderItemDto(this BasketItem item)
    {
        return new OrderItemDto
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            PictureUrl = item.PictureUrl,
            UnitPrice = item.UnitPrice,
            Units = item.Quantity
        };
    }
}
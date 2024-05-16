using Ardalis.Specification;

namespace Microservice.UseCases.Queries;

public sealed class GetOrderSpecification : Specification<Core.Aggregates.OrderAggregate.Order, Order>
{
    public GetOrderSpecification(int id)
    {
        Query.Where(x => x.Id == id);
        
        Query.Select(order => new Order
        {
            OrderNumber = order.Id,
            Date = order.Date,
            Description = order.Description,
            City = order.Address.City,
            Country = order.Address.Country,
            State = order.Address.State,
            Street = order.Address.Street,
            Zipcode = order.Address.ZipCode,
            Status = order.Status.ToString(),
            Total = order.GetTotal(),
            OrderItems = order.Items.Select(oi => new OrderItem
            {
                ProductName = oi.ProductName,
                Units = oi.Units,
                UnitPrice = (double)oi.UnitPrice,
                PictureUrl = oi.PictureUrl
            }).ToList()
        });
    }
}
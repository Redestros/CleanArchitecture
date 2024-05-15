using Microservice.Core;
using Microservice.Core.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderingContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public OrderRepository(OrderingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Order Add(Order order)
    {
        return _context.Set<Order>().Add(order).Entity;
    }

    public async Task<Order?> GetAsync(int orderId)
    {
        var order = await _context.Set<Order>().FindAsync(orderId);

        if (order != null)
        {
            await _context.Entry(order)
                .Collection(i => i.Items)
                .LoadAsync();
        }

        return order;
    }

    public void Update(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
    }
}
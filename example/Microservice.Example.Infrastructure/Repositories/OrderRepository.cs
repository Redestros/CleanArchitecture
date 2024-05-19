using Microservice.Example.Core.Abstractions;
using Microservice.Example.Core.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Example.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public IUnitOfWork UnitOfWork => _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Order Add(Order order)
    {
        return _dbContext.Set<Order>().Add(order).Entity;
    }

    public async Task<Order?> GetAsync(int orderId)
    {
        var order = await _dbContext.Set<Order>().FindAsync(orderId);

        if (order != null)
        {
            await _dbContext.Entry(order)
                .Collection(i => i.Items)
                .LoadAsync();
        }

        return order;
    }

    public void Update(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Modified;
    }
}
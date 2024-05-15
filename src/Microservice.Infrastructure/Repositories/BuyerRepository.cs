using Microservice.Core;
using Microservice.Core.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly OrderingContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public BuyerRepository(OrderingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Buyer Add(Buyer buyer)
    {
        return _context.Set<Buyer>()
            .Add(buyer)
            .Entity;
    }

    public Buyer Update(Buyer buyer)
    {
        return _context.Set<Buyer>()
            .Update(buyer)
            .Entity;
    }

    public async Task<Buyer> FindAsync(string identity)
    {
        var buyer = await _context.Set<Buyer>()
            .Include(b => b.PaymentMethods)
            .Where(b => b.IdentityGuid == identity)
            .SingleOrDefaultAsync();

        return buyer;
    }

    public async Task<Buyer> FindByIdAsync(int id)
    {
        var buyer = await _context.Set<Buyer>()
            .Include(b => b.PaymentMethods)
            .Where(b => b.Id == id)
            .SingleOrDefaultAsync();

        return buyer;
    }
}
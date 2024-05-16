using Microservice.Core.Abstractions;
using Microservice.Core.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly AppDbContext _dbContext;
    public IUnitOfWork UnitOfWork => _dbContext;

    public BuyerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Buyer Add(Buyer buyer)
    {
        return _dbContext.Set<Buyer>()
            .Add(buyer)
            .Entity;
    }

    public Buyer Update(Buyer buyer)
    {
        return _dbContext.Set<Buyer>()
            .Update(buyer)
            .Entity;
    }

    public async Task<Buyer> FindAsync(string identity)
    {
        var buyer = await _dbContext.Set<Buyer>()
            .Include(b => b.PaymentMethods)
            .Where(b => b.IdentityGuid == identity)
            .SingleOrDefaultAsync();

        return buyer;
    }

    public async Task<Buyer> FindByIdAsync(int id)
    {
        var buyer = await _dbContext.Set<Buyer>()
            .Include(b => b.PaymentMethods)
            .Where(b => b.Id == id)
            .SingleOrDefaultAsync();

        return buyer;
    }
}
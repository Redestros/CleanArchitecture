using Microservice.Core.Abstractions;

namespace Microservice.Core.Aggregates.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Task<Buyer?> FindAsync(string buyerIdentityGuid);
    Task<Buyer?> FindByIdAsync(int id);
    Buyer Add(Buyer buyer);
    Buyer Update(Buyer buyer);
}
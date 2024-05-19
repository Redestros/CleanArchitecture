using Microservice.Example.Core.Abstractions;

namespace Microservice.Example.Core.Aggregates.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Task<Buyer> FindAsync(string buyerIdentityGuid);
    Task<Buyer> FindByIdAsync(int id);
    Buyer Add(Buyer buyer);
    Buyer Update(Buyer buyer);
}
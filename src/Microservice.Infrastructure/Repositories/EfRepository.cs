using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Core.Abstractions;

namespace Microservice.Infrastructure.Repositories;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    // This implementation will be used for IReadRepository
    // So unit of work won't make sense
    public IUnitOfWork UnitOfWork { get; } = null!;
}
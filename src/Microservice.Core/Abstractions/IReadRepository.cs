using Ardalis.Specification;

namespace Microservice.Core.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T: class, IAggregateRoot
{
    
}
using Ardalis.Specification;

namespace Microservice.Example.Core.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T: class, IAggregateRoot
{
    
}
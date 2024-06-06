using Microservice.Example.Core.Abstractions;

namespace Microservice.Example.Core.Aggregates.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string IdentityGuid { get; private set; }

    #pragma warning disable CS8618
    public Buyer()
    {
    }

    public Buyer(string name)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }
}
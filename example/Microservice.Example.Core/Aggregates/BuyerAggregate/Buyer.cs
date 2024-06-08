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

    public Buyer(string name, string identityGuid)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(identityGuid))
        {
            throw new ArgumentNullException(nameof(identityGuid));
        }

        if (Guid.TryParse(identityGuid, out _) == false)
        {
            throw new ArgumentException("Should be a valid Guid", nameof(identityGuid));
        }

        IdentityGuid = name;
    }
}
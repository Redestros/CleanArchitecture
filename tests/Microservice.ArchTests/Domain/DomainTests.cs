using MediatR;

namespace Microservice.ArchTests.Domain;

public class DomainTests : ArchUnitBaseTest
{

    [Fact]
    public void DomainEventsShouldBeSealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(INotification))
            .Should()
            .BeSealed()
            .Check(Architecture);
    }
    
    [Fact]
    public void DomainEventsShouldHaveDomainEventPostfix()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(INotification))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .Check(Architecture);
    }
}
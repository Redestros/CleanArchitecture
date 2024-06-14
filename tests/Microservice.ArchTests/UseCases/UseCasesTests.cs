using MediatR;

namespace Microservice.ArchTests.UseCases;

public class UseCasesTests : ArchUnitBaseTest
{
    [Fact]
    public void HandlersShouldHaveNameEndingWithHandler()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler")
            .Check(Architecture);
    }
}
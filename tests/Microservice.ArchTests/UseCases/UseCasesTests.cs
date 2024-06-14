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
    
    [Fact]
    public void HandlersShouldNotPublic()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .NotBePublic();
    }
}
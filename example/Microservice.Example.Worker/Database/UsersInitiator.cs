using Microservice.Example.Core.Aggregates.BuyerAggregate;
using Microservice.Example.Infrastructure;
using Microservice.Example.Worker.Queue;

namespace Microservice.Example.Worker.Database;

public class UsersInitiator : IWorkItem
{
    private IServiceProvider _services;

    public UsersInitiator(IServiceProvider services)
    {
        _services = services;
    }
    
    public async ValueTask Execute(CancellationToken token)
    {
        var scope = _services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (context.Set<Buyer>().Any() == false)
        {
            context.Set<Buyer>().AddRange(GetDefaultBuyers());
            await context.SaveChangesAsync(token);
        }
    }

    protected virtual IEnumerable<Buyer> GetDefaultBuyers()
    {
        return new[]
        {
            new Buyer("Mike", "36af39b6-ff8d-4cce-bccc-532d0d751137"),
            new Buyer("Jake", "11f173a3-a87c-4895-9a1b-7a778703a261")
        };
    }
}
using MediatR;
using Microservice.Example.Core.Abstractions;

namespace Microservice.Example.Infrastructure.Extensions;

internal static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, AppDbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
        
        domainEntities.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());
        
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
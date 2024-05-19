using MediatR;
using Microservice.Example.Core.Abstractions;
using Microservice.Example.Core.Aggregates.BuyerAggregate;
using Microservice.Example.Core.Aggregates.OrderAggregate;
using Microservice.Example.Infrastructure.Behaviors;
using Microservice.Example.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.Example.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingDb"));
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
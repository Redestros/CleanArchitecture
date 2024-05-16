using MediatR;
using Microservice.Core.Aggregates.BuyerAggregate;
using Microservice.Core.Aggregates.OrderAggregate;
using Microservice.Infrastructure.Behaviors;
using Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.AddDbContext<AppContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingDb"));
        });
        
        services.AddTransient(typeof(IPipelineBehavior<,> ), typeof(TransactionBehavior<,> ));
        
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
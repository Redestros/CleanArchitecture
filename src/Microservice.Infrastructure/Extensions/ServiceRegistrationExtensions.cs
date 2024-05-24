using MediatR;
using Microservice.Core.Abstractions;
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

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
    }
}
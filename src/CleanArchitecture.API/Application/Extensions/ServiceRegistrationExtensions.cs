using CleanArchitecture.API.Application.Behaviors;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.API.Application.Extensions;

internal static class ServiceRegistrationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.AddDbContext<OrderingContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingDb"));
        });
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
    }
}
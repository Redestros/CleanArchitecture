using Microsoft.EntityFrameworkCore;

namespace Microservice.API.Application.Extensions;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.API.Extensions;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
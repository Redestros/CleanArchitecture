using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.API.Application.Extensions;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
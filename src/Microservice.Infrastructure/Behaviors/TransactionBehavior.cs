using MediatR;
using Microservice.UseCases.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microservice.Infrastructure.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly AppDbContext _dbDbContext;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, AppDbContext dbDbContext)
    {
        _logger = logger;
        _dbDbContext = dbDbContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbDbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {

                await using var transaction = await _dbDbContext.BeginTransactionAsync();
                using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                {
                    _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await _dbDbContext.CommitTransactionAsync(transaction);

                }

            });

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}
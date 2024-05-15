using MediatR;
using Microservice.API.Application.Commands;
using Microservice.API.Application.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Apis;

[ApiController]
[Route("[controller]")]
public class OrderApis : ControllerBase
{
    private readonly ILogger<OrderApis> _logger;
    private readonly IMediator _mediator;

    public OrderApis(ILogger<OrderApis> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Results<Ok, BadRequest<string>>> Create(CreateOrderRequest request)
    {
        var createOrderCommand = new CreateOrderCommand(request.Items, request.UserId, request.UserName, request.City, request.Street,
            request.State, request.Country, request.ZipCode,
            request.CardNumber, request.CardHolderName, request.CardExpiration,
            request.CardSecurityNumber, request.CardTypeId);


        _logger.LogInformation(
            "Sending command: {CommandName} : ({@Command})",
            request.GetGenericTypeName(),
            request);

        var result = await _mediator.Send(createOrderCommand);

        if (result)
        {
            _logger.LogInformation("CreateOrderCommand succeeded");
        }
        else
        {
            _logger.LogWarning("CreateOrderCommand failed");
        }

        return TypedResults.Ok();
    }
}

public record CreateOrderRequest(
    string UserId,
    string UserName,
    string City,
    string Street,
    string State,
    string Country,
    string ZipCode,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    int CardTypeId,
    List<BasketItem> Items);
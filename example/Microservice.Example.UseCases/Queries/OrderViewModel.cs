namespace Microservice.Example.UseCases.Queries;

public record OrderItem
{
    public required string ProductName { get; init; }
    public required int Units { get; init; }
    public required double UnitPrice { get; init; }
    public required string PictureUrl { get; init; }
}

public record Order
{
    public int OrderNumber { get; init; }
    public DateTime Date { get; init; }
    public required string Status { get; init; }
    public required string Description { get; init; }
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string Zipcode { get; init; }
    public required string Country { get; init; }
    public decimal Total { get; set; }
    public required List<OrderItem> OrderItems { get; set; }
}

public record OrderSummary
{
    public int OrderNumber { get; init; }
    public DateTime Date { get; init; }
    public string Status { get; init; }
    public double Total { get; init; }
}

public record CardType(int Id, string Name);
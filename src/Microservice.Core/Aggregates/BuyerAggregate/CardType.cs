namespace Microservice.Core.Aggregates.BuyerAggregate;

public class CardType
{
    public static readonly CardType Amex = new(1, nameof(Amex));
    public static readonly CardType Visa = new(2, nameof(Visa));
    public static readonly CardType MasterCard = new(3, nameof(MasterCard));
    
    public CardType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
}
using Microservice.Core.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Infrastructure.EntityConfigurations;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Id)
            .UseHiLo();

        builder.Property<int>("BuyerId");

        builder.Property("_cardHolderName")
            .HasColumnName("CardHolderName")
            .HasMaxLength(200);
        
        builder.Property("_alias")
            .HasColumnName("Alias")
            .HasMaxLength(200);

        builder.Property("_cardNumber")
            .HasColumnName("CardNumber")
            .HasMaxLength(200);

        builder.Property("_cardTypeId")
            .HasColumnName("CardTypeId");

        builder.HasOne(p => p.CardType)
            .WithMany()
            .HasForeignKey("_cardTypeId");
    }
}
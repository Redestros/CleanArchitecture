using Microservice.Core.Aggregates.BuyerAggregate;
using Microservice.Core.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Infrastructure.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.Ignore(b => b.DomainEvents);

        builder.Property(x => x.Id)
            .UseHiLo("orders-sequence");

        builder.OwnsOne(x => x.Address);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.PaymentId)
            .HasColumnName("PaymentMethodId");

        builder.HasOne<PaymentMethod>()
            .WithMany()
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Buyer)
            .WithMany()
            .HasForeignKey(x => x.BuyerId);
    }
}
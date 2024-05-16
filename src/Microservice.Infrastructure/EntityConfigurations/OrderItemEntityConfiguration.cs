using Microservice.Core.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Infrastructure.EntityConfigurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Id)
            .UseHiLo("order-items-sequence");

        builder.Property<int>("OrderId");
    }
}
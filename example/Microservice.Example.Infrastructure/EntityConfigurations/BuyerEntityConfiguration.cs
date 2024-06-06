using Microservice.Example.Core.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Example.Infrastructure.EntityConfigurations;

public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyers");

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Id)
            .UseHiLo("buyers-sequence");

        builder.Property(x => x.IdentityGuid)
            .HasMaxLength(200);

        builder.HasIndex(x => x.IdentityGuid)
            .IsUnique();
    }
}
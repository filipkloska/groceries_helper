using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace groceries_helper.Persistence.Models;

public class UnitOfMeasure : BaseTrackingEntity
{
    public required string Name { get; set; }
}

public class UnitOfMeasureConfiguration : BaseTrackingEntityConfiguration<UnitOfMeasure>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        base.Configure(builder);
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
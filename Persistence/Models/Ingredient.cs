using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace groceries_helper.Persistence.Models;

public class Ingredient : BaseTrackingEntity
{
    public required string Name { get; set; }

}
public class IngredientConfiguration : BaseTrackingEntityConfiguration<Ingredient>
{
    public override void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        base.Configure(builder);
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}

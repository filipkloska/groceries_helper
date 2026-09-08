using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace groceries_helper.Persistence.Models;

public class Recipe : BaseTrackingEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public int Calories { get; set; }
    public MealType MealType { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}
public class RecipeConfiguration : BaseTrackingEntityConfiguration<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(i => i.Description)
            .HasMaxLength(1024);
    }
}



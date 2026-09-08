using groceries_helper.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace groceries_helper.Persistence;

public class GroceryHelperDbContext : DbContext
{
    public GroceryHelperDbContext(DbContextOptions<GroceryHelperDbContext> options) : base(options)
    {
    }
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<UnitOfMeasure>  UnitOfMeasures => Set<UnitOfMeasure>();
    public DbSet<Ingredient>  Ingredients => Set<Ingredient>();
    public DbSet<RecipeIngredient>  RecipeIngredients => Set<RecipeIngredient>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(GroceryHelperDbContext).Assembly);
        
        base.OnModelCreating(builder);

        builder.Entity<Recipe>()
            .HasMany(r => r.RecipeIngredients);
        builder.Entity<RecipeIngredient>()
            .HasOne(r => r.Recipe);

    }
}
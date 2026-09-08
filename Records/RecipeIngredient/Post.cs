using groceries_helper.Persistence.Models;

namespace groceries_helper.Records.RecipeIngredient;

public record RecipeIngredientDto
{
    public int IngredientId { get; set; }
    public required string IngredientName { get; set; }
    public decimal Amount { get; set; }
    public int UnitOfMeasureId  { get; set; }
}

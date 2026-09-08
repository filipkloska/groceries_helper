using groceries_helper.Persistence.Models;
using groceries_helper.Records.RecipeIngredient;

namespace groceries_helper.Records.Recipe;

public record UpdateRecipeRequest
{
    public required string Name { get; init; }
    public string Description { get; init; }
    public MealType MealType { get; set; }
    public int Calories { get; set; }
    public List<RecipeIngredientDto> RecipeIngredients { get; set; }
}
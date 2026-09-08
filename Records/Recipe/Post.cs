using groceries_helper.Persistence.Models;
using groceries_helper.Records.RecipeIngredient;

namespace groceries_helper.Records.Recipe;

public record AddRecipeRequest
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public MealType MealType { get; set; }
    public int Calories { get; set; }
    public List<RecipeIngredientDto> RecipeIngredients { get; set; }
}


using groceries_helper.Persistence.Models;
using groceries_helper.Records.RecipeIngredient;

namespace groceries_helper.Records.Recipe;

public record GetRecipeResponse
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Calories { get; set; }
    public MealType MealType { get; set; }
    public List<RecipeIngredientDto>? RecipeIngredients { get; set; }
}


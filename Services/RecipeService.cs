using FluentResults;
using groceries_helper.Records;
using groceries_helper.Records.Recipe;
using groceries_helper.Records.RecipeIngredient;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace groceries_helper.Services;
using groceries_helper.Persistence;
using groceries_helper.Persistence.Models;

public class RecipeService(GroceryHelperDbContext dbContext)
{
    public async Task<Result<GetRecipeResponse>> GetAsync(int id, CancellationToken ct)
    {
        var recipe = await dbContext.Recipes
            .Select(i => new GetRecipeResponse
            {
                Id = i.Id,
                Name = i.Name,
                Calories = i.Calories,
                MealType = i.MealType,
                Description = i.Description,
                RecipeIngredients = i.RecipeIngredients.Select(ri => new RecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    UnitOfMeasureId = ri.UnitOfMeasureId,
                }).ToList()
            }).FirstOrDefaultAsync(c => c.Id == id, ct);
        
        if (recipe is null)
        {
            return Result.Fail("Recipe not found");
        }

        return Result.Ok(recipe);
    }
    
    public async Task<Result<PaginationResponse<GetRecipeResponse>>> GetAllAsync(CancellationToken ct)
    {
        var recipe = await dbContext.Recipes
            .Select(i => new GetRecipeResponse
            {
                Id = i.Id,
                Name = i.Name,
                Calories = i.Calories,
                MealType = i.MealType,
                Description = i.Description,
                RecipeIngredients = i.RecipeIngredients.Select(ri => new RecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    UnitOfMeasureId = ri.UnitOfMeasureId,
                }).ToList()
            }).ToListAsync(ct);
        return Result.Ok(new PaginationResponse<GetRecipeResponse>(recipe, recipe.Count));
    }

    public async Task<Result> AddAsync(AddRecipeRequest request, CancellationToken ct)
    {
        var ingredients = new List<RecipeIngredient>();
        foreach (var ingredient in request.RecipeIngredients)
        {
            ingredients.Add(new RecipeIngredient
            {
                IngredientId = ingredient.IngredientId,
                UnitOfMeasureId = ingredient.UnitOfMeasureId,
                Amount = ingredient.Amount,
            });
        }
        await dbContext.Recipes.AddAsync(new Recipe
        {
            Name = request.Name,
            Calories = request.Calories,
            MealType = request.MealType,
            Description = request.Description,
            RecipeIngredients = ingredients,
            IsActive = true
        }, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(int id, UpdateRecipeRequest request, CancellationToken ct)
    {
        var recipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        if (recipe is null)
        {
            return Result.Fail("Recipe not found");
        }
        
        var ingredients = new List<RecipeIngredient>();
        foreach (var ingredient in request.RecipeIngredients)
        {
            ingredients.Add(new RecipeIngredient
            {
                IngredientId = ingredient.IngredientId,
                UnitOfMeasureId = ingredient.UnitOfMeasureId,
                Amount = ingredient.Amount,
            });
        }
        recipe.RecipeIngredients.Clear();
        
        recipe.RecipeIngredients = ingredients;
        recipe.Name = request.Name;
        recipe.Calories = request.Calories;
        recipe.Description = request.Description;
        recipe.MealType = request.MealType;
        
        await dbContext.SaveChangesAsync(ct);
        return Result.Ok();
        
    }

    public async Task<Result> RemoveAsync(int id, CancellationToken ct)
    {
        var recipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        if (recipe is null)
        {
            return Result.Fail("Recipe not found");
        }
        recipe.IsActive = false;
        await dbContext.SaveChangesAsync(ct);
        return Result.Ok();
    }
}
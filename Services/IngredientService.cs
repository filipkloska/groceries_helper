using FluentResults;
using groceries_helper.Persistence;
using groceries_helper.Persistence.Models;
using groceries_helper.Records;
using Microsoft.EntityFrameworkCore;

namespace groceries_helper.Services;

public class IngredientService(GroceryHelperDbContext dbContext)
{
    public async Task<Result<GetIngredientsResponse>> GetAsync(int id, CancellationToken ct)
    {
        var ingredient = await dbContext.Ingredients
            .Select(i => new GetIngredientsResponse
            {
                Id = i.Id,
                Name = i.Name,
            }).FirstOrDefaultAsync(c => c.Id == id, ct);
        
        if (ingredient is null)
        {
            return Result.Fail("Ingredient not found");
        }

        return Result.Ok(ingredient);
    }
    
    public async Task<Result<PaginationResponse<GetIngredientsResponse>>> GetAllAsync(CancellationToken ct)
    {
        var ingredients = await dbContext.Ingredients.Select(i => new GetIngredientsResponse
        {
            Id = i.Id,
            Name = i.Name,
        }).ToListAsync(ct);
        return Result.Ok(new PaginationResponse<GetIngredientsResponse>(ingredients, ingredients.Count));
    }
    
    public async Task<Result> AddAsync(AddIngredientRequest request, CancellationToken ct)
    {
        await dbContext.Ingredients.AddAsync(new Ingredient
        {
            Name = request.Name,
            IsActive = true
        }, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(int id, UpdateIngredientRequest request, CancellationToken ct)
    {
        var ingredient =  await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);
        if (ingredient is null)
        {
            return Result.Fail("Ingredient not found");
        }
        
        ingredient.Name = request.Name;
        await dbContext.SaveChangesAsync(ct);
        
        return Result.Ok();
    }

    public async Task<Result> RemoveAsync(int id, CancellationToken ct)
    {
        var ingredient =  await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);
        if (ingredient is null)
        {
            return Result.Fail("Ingredient not found");
        }
        
        ingredient.IsActive = false;
        await dbContext.SaveChangesAsync(ct);
        
        return Result.Ok();
    }
}
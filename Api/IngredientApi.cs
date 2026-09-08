using groceries_helper.Extensions;
using groceries_helper.Persistence.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using groceries_helper.Records;
using groceries_helper.Services;

namespace groceries_helper.Api;

public static class IngredientApi
{
    public static IEndpointRouteBuilder MapIngredients(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ingredients");

        group.MapGet("/{id:int}", GetIngredientAsync);
        group.MapGet("/", GetAllIngredientsAsync);
        group.MapPost("/", AddIngredientAsync);
        group.MapPut("/{id:int}", UpdateIngredientAsync);
        group.MapDelete("/{id:int}", RemoveIngredientAsync);
    
        return routes;
    }
    
    private static async Task<Results<Ok<Response<GetIngredientsResponse>>, 
            BadRequest<Response<GetIngredientsResponse>>>>
        GetIngredientAsync(int id, IngredientService service, CancellationToken ct)
    {
        var result = await service.GetAsync(id, ct);
        return result.ToHttpResult();
    }
    
    private static async Task<Results<Ok<Response<PaginationResponse<GetIngredientsResponse>>>, 
            BadRequest<Response<PaginationResponse<GetIngredientsResponse>>>>>
        GetAllIngredientsAsync(IngredientService service, CancellationToken ct)
    {
        var result = await service.GetAllAsync(ct);
        return result.ToHttpResult();
    }

    private static async Task<Results<Ok<Response>, BadRequest<Response>>> 
        AddIngredientAsync(AddIngredientRequest request, IngredientService service, CancellationToken ct)
    {
        var result = await service.AddAsync(request, ct);
        return result.ToHttpResult();
    }

    private static async Task<Results<Ok<Response>, BadRequest<Response>>>
        UpdateIngredientAsync(int id, UpdateIngredientRequest request, IngredientService service, CancellationToken ct)
    {
        var result = await service.UpdateAsync(id, request, ct);
        return result.ToHttpResult();
    }

    private static async Task<Results<Ok<Response>, BadRequest<Response>>>
        RemoveIngredientAsync(int id, IngredientService service, CancellationToken ct)
    {
        var result = await service.RemoveAsync(id, ct);
        return result.ToHttpResult();
    }
    
    
}
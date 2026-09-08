using FluentResults;
using groceries_helper.Api;
using Microsoft.AspNetCore.Http.HttpResults;

namespace groceries_helper.Extensions;

public static class ResultExtensions
{
    public static Response ToResponse(this Result result)
    {
        return new Response
        {
            Validation = new ValidationResult
            {
                Errors = result.Errors.Select(e => e.Message).ToList()
            }
        };
    }

    public static Response<T> ToResponse<T>(this Result<T> result)
    {
        var response = new Response<T>
        {
            Validation = new ValidationResult
            {
                Errors = result.Errors.Select(e => e.Message).ToList()
            }
        };
        
        if (result.IsSuccess)
        {
            response.Data = result.Value;
        }
        return response;
    }

    public static Results<Ok<Response>, BadRequest<Response>> ToHttpResult(this Result result)
    {
        return result.IsSuccess ? TypedResults.Ok(result.ToResponse()) : TypedResults.BadRequest(result.ToResponse());
    }

    public static Results<Ok<Response<T>>, BadRequest<Response<T>>> ToHttpResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? TypedResults.Ok(result.ToResponse()) : TypedResults.BadRequest(result.ToResponse()); 
    }
    
}
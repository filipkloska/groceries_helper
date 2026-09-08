namespace groceries_helper.Api;

public static class RegisterEndpointsExtension
{
    public static void RegisterApiEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapIngredients();
    }
}
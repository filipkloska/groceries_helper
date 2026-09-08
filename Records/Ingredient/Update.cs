namespace groceries_helper.Records;

public record UpdateIngredientRequest
{
    public required string Name { get; set; }
}

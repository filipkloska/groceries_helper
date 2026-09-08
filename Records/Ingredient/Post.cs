namespace groceries_helper.Records;

public record AddIngredientRequest
{
    public required string Name { get; set; }
}
namespace groceries_helper.Records;

public record GetIngredientsResponse
{
    public required int Id { get; init; }
    public required string Name { get; set; }
}
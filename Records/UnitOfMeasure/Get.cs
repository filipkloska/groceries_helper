namespace groceries_helper.Records.UnitOfMeasure;

public record GetUnitOfMeasureRequest
{
    
}

public record GetUnitOfMeasureResponse
{
    public required int Id { get; init; }
    public string Name { get; init; }
}
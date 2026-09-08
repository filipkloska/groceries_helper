namespace groceries_helper.Records.UnitOfMeasure;

public record AddUnitOfMeasureRequest
{
    public required string Name { get; init; }
}
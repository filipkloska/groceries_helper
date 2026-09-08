namespace groceries_helper.Records;

public record PaginationResponse<T>(List<T> Items, int TotalCount);
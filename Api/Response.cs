namespace groceries_helper.Api;

public class Response
{
    public ValidationResult Validation { get; set; } = new();
    
    public bool IsSuccess => Validation.Errors.Count == 0;
    
    public void AddError(string message)
    {
        Validation.Errors.Add(message);
    }
    
    public Response WithError(string message)
    {
        AddError(message);
        return this;
    }
    
    public static Response Fail(string message)
    {
        return new Response().WithError(message);
    }
}

public class Response<T> : Response
{
    public T Data { get; set; } = default!;
    
    public Response() { }
    
    public Response(T data)
    {
        Data = data;
    }
    
    public Response<T> WithData(T data)
    {
        Data = data;
        return this;
    }
    
    public static Response<T> Ok(T data)
    {
        return new Response<T>(data);
    }
}
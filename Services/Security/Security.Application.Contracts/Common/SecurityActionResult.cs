namespace Security.Application.Contracts.Common;

public class SecurityActionResult<T> : SecurityActionResult where T : class 
{
    public T Data { get; set; } 
    
}
public class SecurityActionResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}
public class SecurityActionResultWithPaging<T> : SecurityActionResult where T : class
{
    public T Data { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int Count { get; set; } = 5;
}

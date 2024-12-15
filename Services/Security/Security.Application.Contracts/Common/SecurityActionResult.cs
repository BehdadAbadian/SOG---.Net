namespace Security.Application.Contracts.Common
{
    public class SecurityActionResult<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public int Page { get; set; } =1;
        public int PageSize { get; set; } = 5;
        public int Count { get; set; } = 5;
    }
}

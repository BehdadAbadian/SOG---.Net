namespace Catalog.Appllication.Contract.Common;

public class CatalogActionResult<T> where T : class
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

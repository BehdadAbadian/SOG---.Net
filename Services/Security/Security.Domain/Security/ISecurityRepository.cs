namespace Security.Domain.Security;

public interface ISecurityRepository<T> where T : class
{
    Task Add(T entity);
    Task Delete(T entity);
    Task<T> GetById(int id);

}

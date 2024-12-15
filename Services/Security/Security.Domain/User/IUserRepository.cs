namespace Security.Domain.User;

public interface IUserRepository
{
    public Task Add(User entity);
    public void Update(User entity);
    public void Delete(User entity);
    public Task<List<User>> GetAll(int page = 0, int size = 0);
    public Task<long> Count();
    public Task<User> GetById(Guid id);
    public User Search(string search = "");
    public Task<bool> Exits(string name);
}

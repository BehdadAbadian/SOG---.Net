using Microsoft.EntityFrameworkCore;
using Security.Domain.User;
using Security.Infrastructure.Database;

namespace Security.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly SecurityContext _context;

    public UserRepository(SecurityContext context)
    {
        _context = context;
    }
    public async Task Add(User entity)
    {
        await _context.users.AddAsync(entity);
    }

    public async Task<long> Count()
    {
        return await _context.users.CountAsync();
    }

    public void Delete(User entity)
    {
        _context.users.Remove(entity);
    }

    public async Task<bool> Exits(string name)
    {
        return await _context.users.AnyAsync(x => x.Name == name);
    }

    public async Task<List<User>> GetAll(int page = 0, int size = 0)
    {
        if (page == 0 || size == 0) 
        {
            return await _context.users.AsNoTracking().ToListAsync();
        }

        return await _context.users.OrderBy(x => x.Id)
            .Skip((page - 1)*size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _context.users.FirstAsync(x => x.Id == id);
    }

    public User Search(string search = "")
    {
        return _context.users.Where(e => e.Name.Contains(search) || e.Email.Contains(search)).FirstOrDefault();

    }

    public void Update(User entity)
    {
        _context.Update(entity);
    }
}

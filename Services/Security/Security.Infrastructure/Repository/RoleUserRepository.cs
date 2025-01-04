using Microsoft.EntityFrameworkCore;
using Security.Domain.Security;
using Security.Infrastructure.Database;

namespace Security.Infrastructure.Repository;

public class RoleUserRepository : ISecurityRepository<RoleUser>
{
    private readonly SecurityContext _context;

    public RoleUserRepository(SecurityContext context)
    {
        _context = context;
    }
    public Task Add(RoleUser entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(RoleUser entity)
    {
        throw new NotImplementedException();
    }

    public Task<RoleUser> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public  List<int> GetRolesOfUser(Guid userId) 
    {
        var roles = _context.roleUsers.Where(x => x.UserId == userId);
        var r = roles.Select(x => x.RoleId).ToList();
        return r;
    }
}

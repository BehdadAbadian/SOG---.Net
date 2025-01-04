using Microsoft.EntityFrameworkCore;
using Security.Domain.Security;
using Security.Infrastructure.Database;

namespace Security.Infrastructure.Repository;

public class PermissionRepository : ISecurityRepository<Permission>
{
    private readonly SecurityContext _context;

    public PermissionRepository(SecurityContext context)
    {
        _context = context;
    }
    public Task Add(Permission entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Permission entity)
    {
        throw new NotImplementedException();
    }

    public Task<Permission> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public List<string> GetPermissionFlags(List<int> permissionId)
    {
        var flags = _context.permissions.Where(x => permissionId.Contains(x.Id)).Select(x => x.PermissionFlag).ToList();
        return flags;
    }
}

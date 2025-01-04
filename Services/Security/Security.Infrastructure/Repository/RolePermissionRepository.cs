using Microsoft.EntityFrameworkCore;
using Security.Domain.Security;
using Security.Infrastructure.Database;

namespace Security.Infrastructure.Repository;

public class RolePermissionRepository : ISecurityRepository<RolePermission>
{
    private readonly SecurityContext _context;

    public RolePermissionRepository(SecurityContext context)
    {
        _context = context;
    }
    public Task Add(RolePermission entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(RolePermission entity)
    {
        throw new NotImplementedException();
    }

    public Task<RolePermission> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<int> GetPermissionId(List<int> rolesId)
    {
        var permissonId =  _context.rolesPermission.Where(x => rolesId.Contains(x.RoleId)).Select(x => x.PermissionId).ToList();
        return permissonId;
    }
}

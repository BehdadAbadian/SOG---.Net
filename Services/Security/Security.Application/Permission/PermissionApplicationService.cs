using Security.Application.Contracts.Interface;
using Security.Infrastructure.Database.Migrations;
using Security.Infrastructure.Repository;

namespace Security.Application.Permission;

public class PermissionApplicationService : IPermissionApplicationService
{
    private readonly PermissionRepository _permission;
    private readonly RolePermissionRepository _rolePermission;
    private readonly RoleUserRepository _roleUserRepository;

    public PermissionApplicationService(PermissionRepository permission, RolePermissionRepository rolePermission, RoleUserRepository roleUserRepository)
    {
        _permission = permission;
        _rolePermission = rolePermission;
        _roleUserRepository = roleUserRepository;
    }
    public bool CheckPermission(Guid userId, string permissionName)
    {
        var roles = _roleUserRepository.GetRolesOfUser(userId);
        var permissionId = _rolePermission.GetPermissionId(roles);
        var permissionFlags = _permission.GetPermissionFlags(permissionId);
        return permissionFlags.Contains(permissionName);
    }
}

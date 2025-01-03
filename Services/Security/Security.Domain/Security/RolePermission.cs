namespace Security.Domain.Security;

public class RolePermission
{
    public int Id { get; private set; }
    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }


    private RolePermission()
    {
        
    }

    private RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
    public static RolePermission CreateNew(int roleId, int permissionId)
    {
        return new RolePermission(roleId, permissionId);
    }
}

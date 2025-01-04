namespace Security.Application.Contracts.Interface;

public interface IPermissionApplicationService
{
    public bool CheckPermission(Guid userId, string permissionName);
}

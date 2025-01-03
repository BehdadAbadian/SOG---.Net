namespace Security.Domain.Security;

public class Permission
{
    public int Id { get; private set; }
    public string PermissionFlag { get; private set; }
    public string Title { get; private set; }

    private Permission()
    {
        
    }

    private Permission(string permissionFlag, string title)
    {
        
        PermissionFlag = permissionFlag;
        Title = title;
    }

    public static Permission CreateNew(string permissionFlag, string title)
    {
        return new Permission(permissionFlag, title);
    }
}

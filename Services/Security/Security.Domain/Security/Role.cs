namespace Security.Domain.Security;

public class Role
{
    public int Id { get; private set; }
    public string RoleName { get; private set; }
    public bool IsActive { get; private set; }


    private Role()
    {
        
    }

    private Role(string roleName, bool isActive)
    {
        RoleName = roleName;
        IsActive = isActive;
    }
    public static Role CreateNew(string roleName, bool isActive)
    {
        return new Role(roleName, isActive);
    }
}

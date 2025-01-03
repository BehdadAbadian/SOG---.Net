namespace Security.Domain.Security;

public class RoleUser
{
    public int Id { get; private set; }
    public int RoleId { get; private set; }
    public Guid UserId { get; private set; }

    private RoleUser()
    {
        
    }

    private RoleUser(int roleId, Guid userId)
    {
        RoleId = roleId;
        UserId = userId;
    }

    public static RoleUser CreateNew(int roleId, Guid userId)
    {
        return new RoleUser(roleId, userId);
    }
}

//namespace Security.Domain.Authentication;

//public class UserRefreshToken
//{
//    public long Id { get; private set; }
//    public Guid UserID { get; private set; }
//    public Security.Domain.User.User User { get; private set; } 
//    public string RefreshToken { get; private set; }
//    public int RefreshTokenTimeout { get; private set; }
//    public DateTime CreationDate { get; private set; }
//    public bool IsValid { get; private set; }

//    private UserRefreshToken()
//    {
        
//    }
//    private UserRefreshToken(Guid userId,Security.Domain.User.User user, string refreshToken, int refreshTokenTimeOut)
//    {
//        UserID = userId;
//        User = user;
        
//        IsValid = true;
//        CreationDate = DateTime.Now;

//    }
//    public static UserRefreshToken CreateNew(Guid userId, Security.Domain.User.User user, string refreshToken, int refreshTokenTimeOut)
//    {
//        return new UserRefreshToken(userId, user, refreshToken, refreshTokenTimeOut);
//    }
    //public static UserRefreshToken UpdateToken(string refreshToken, int refreshTokenTimeOut)
    //{
    //    RefreshToken = refreshToken;
    //    RefreshTokenTimeout = refreshTokenTimeOut;
    //    return
    //}
//}

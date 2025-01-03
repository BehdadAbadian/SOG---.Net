using MediatR;
using Security.Domain.User;
using Security.Infrastructure.Pattern;
using Security.Infrastructure.Utility.Encryption;

namespace Security.Application.Login.Command;

public class LoginCommand : IRequest<LoginCommandRespond>
{
    public string Name { get; set; }
    public string Password { get; set; }
}

public class LoginCommandRespond
{
    public string Token { get; set; }
    public Guid Id { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public LoginCommandRespond(string token, Guid id, string message, bool success)
    {
        Token = token;
        Id = id;
        Message = message;
        IsSuccess = success;
    }
    public LoginCommandRespond(string message)
    {
        Message = message;
        IsSuccess = false;
    }
    public static LoginCommandRespond Success(string token,Guid id)
    {
        return new LoginCommandRespond(token, id, "You are in", true);
    }
    public static LoginCommandRespond Fail(string message)
    {
        return new LoginCommandRespond(message);
    }
}

public class Handler : IRequestHandler<LoginCommand, LoginCommandRespond>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _repository;
    private readonly EncryptionUtility _encryption;
    private readonly IUnitOfWork _unitOfWork;

    public Handler(IMediator mediator,IUserRepository repository, EncryptionUtility encryption, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _repository = repository;
        _encryption = encryption;
        _unitOfWork = unitOfWork;
    }
    public async Task<LoginCommandRespond> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
       if(await _repository.Exits(request.Name))
        {
            
            var user = await _repository.GetByName(request.Name);
            
            var pass = _encryption.GetSHA256(request.Password, user.PasswordSalt);
            if(user.Password == pass)
            {
                var token = _encryption.GetNewToken(user.Id);
                
                user.UpdateLastlogin();
                await _unitOfWork.SaveChangesAsync();
                return LoginCommandRespond.Success(token, user.Id);
            }
            else
            {
                return LoginCommandRespond.Fail("Wrong password");

            }
        }
        else
        {
            return LoginCommandRespond.Fail("Can not Find This User");

        }
    }
}

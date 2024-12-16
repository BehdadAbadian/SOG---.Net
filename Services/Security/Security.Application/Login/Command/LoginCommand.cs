using MediatR;
using Security.Domain.User;
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
}

public class Handler : IRequestHandler<LoginCommand, LoginCommandRespond>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _repository;
    private readonly EncryptionUtility _encryption;

    public Handler(IMediator mediator,IUserRepository repository, EncryptionUtility encryption)
    {
        _mediator = mediator;
        _repository = repository;
        _encryption = encryption;
    }
    public async Task<LoginCommandRespond> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = new LoginCommandRespond();
       if(await _repository.Exits(request.Name))
        {
            var user = await _repository.GetByName(request.Name);
            var pass = _encryption.GetSHA256(request.Password, user.PasswordSalt);
            if(user.Password == pass)
            {
                var token = _encryption.GetNewToken(user.Id);
                result.Token = token;
                result.Id= user.Id;
                result.IsSuccess = true;
                result.Message = "You are in";
            }
            else
            {
                result.Message = "Wrong password";
                result.IsSuccess= false;
            }
        }
        else
        {
            result.Message = "Can not Find This User";
            result.IsSuccess= false;
        }
       return result;
    }
}

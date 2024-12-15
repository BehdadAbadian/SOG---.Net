using MediatR;
using Security.Domain.User;
using Serilog;

namespace Security.Application.User.Query;

public class GetAllQuery : IRequest<List<GetAllQueryRespond>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
public class GetAllQueryRespond
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? RegisterDate { get; set; }
    public string? LastLoggin { get; set; }
}

public class Handler : IRequestHandler<GetAllQuery, List<GetAllQueryRespond>>
{
    private readonly IUserRepository _repository;

    public Handler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<GetAllQueryRespond>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAll(request.Page, request.PageSize);
        List<GetAllQueryRespond> result = new List<GetAllQueryRespond>();
        users.ForEach(user =>
        {
            var u = new GetAllQueryRespond
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RegisterDate = user.CreationDate.ToString(),
                LastLoggin = user.LastLogin.ToString(),
                
            };
            result.Add(u);
        });
        Log.Information("Create list of All user");
        return result;

    }
}

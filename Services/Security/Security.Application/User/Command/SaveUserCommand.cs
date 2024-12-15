﻿using MediatR;
using Security.Domain.User;
using Security.Infrastructure.Pattern;
using Serilog;

namespace Security.Application.User.Command;

public class SaveUserCommand :IRequest<SaveUserCommandRespond>
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}
public class SaveUserCommandRespond
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
}

public class Handler : IRequestHandler<SaveUserCommand, SaveUserCommandRespond>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public Handler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<SaveUserCommandRespond> Handle(SaveUserCommand request, CancellationToken cancellationToken)
    {
        if(await _repository.Exits(request.Name))
        {
            Log.Warning("Duplicated Name for User, Name : {0}", request.Name);
            return new SaveUserCommandRespond { Message = "Name is duplicated!" };
        }
        var user = Security.Domain.User.User.CreateNew(request.Name,request.Email,request.Password);
        await _repository.Add(user);
        await _unitOfWork.SaveChangesAsync();
        Log.Information("new User Insert to Database, user name : {0}", request.Name);
        return new SaveUserCommandRespond {Id = user.Id, Message = "user add successfully!"};

    }
}
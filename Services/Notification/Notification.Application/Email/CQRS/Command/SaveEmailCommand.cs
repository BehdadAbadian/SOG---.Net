using MediatR;
using Notification.Domain.Email;
using Notification.Infrastructure.Pattern;
using Serilog;

namespace Notification.Application.Email.CQRS.Command;

public class SaveEmailCommand : IRequest<SaveEmailCommandRespond>
{
    public string Sender { get; set; }
    public string EmailAddress { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
public class SaveEmailCommandRespond
{
    public long Id { get; set; }
}

public class EmailHandler : IRequestHandler<SaveEmailCommand, SaveEmailCommandRespond>
{
    private readonly IEmailRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EmailHandler(IEmailRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<SaveEmailCommandRespond> Handle(SaveEmailCommand request, CancellationToken cancellationToken)
    {
        var entity = Domain.Email.Email.CreateNew(request.Sender, request.EmailAddress, request.Subject, request.Body);
        await _repository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        Log.Information("New Email Insert To DB");
        return new SaveEmailCommandRespond { Id = entity.Id };
    }
}

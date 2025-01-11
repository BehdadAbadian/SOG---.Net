using MediatR;
using Notification.Domain.Email;
using Notification.Domain.Share;
using Notification.Infrastructure.Pattern;
using Serilog;

namespace Notification.Application.Email.CQRS.Command;

public class UpdateStatusCommand : IRequest
{
    public long Id { get; set; }
    public Status EmailStatus { get; set; }
}

public class StatusHandler : IRequestHandler<UpdateStatusCommand>
{
    private readonly IEmailRepository _repository;
    private readonly IUnitOfWork _unitOf;

    public StatusHandler(IEmailRepository repository, IUnitOfWork unitOf)
    {
        _repository = repository;
        _unitOf = unitOf;
    }
    public async Task Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        if(await _repository.Exits(request.Id))
        {
            var email = await _repository.GetByIdAsync(request.Id);
            email.ChangeStatus(request.EmailStatus);
            await _unitOf.SaveChangesAsync();
            Log.Information("Status of Email With Id : {0} Change to : {1}", request.Id, request.EmailStatus.ToString());
        }
            

    }
}

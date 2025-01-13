using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notification.Domain.Share;
using Notification.Infrastructure.Database;
using Serilog;

namespace Notification.Application.Email.CQRS.Command;

public class UpdateStatusCommand : IRequest
{
    public long Id { get; set; }
    public Status EmailStatus { get; set; }
}

public class StatusHandler : IRequestHandler<UpdateStatusCommand>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public StatusHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            if(await _context.Emails.AnyAsync(x => x.Id == request.Id))
            {
                var email = await _context.Emails.FirstOrDefaultAsync(x => x.Id == request.Id);
                email.ChangeStatus(request.EmailStatus);
                await _context.SaveChangesAsync();
                Log.Information("Status of Email With Id : {0} Change to : {1}", request.Id, request.EmailStatus.ToString());
            }
            
        }
    }
}

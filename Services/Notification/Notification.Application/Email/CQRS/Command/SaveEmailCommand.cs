using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Contracts.Interface;
using Notification.Application.Contracts.Share;
using Notification.Domain.Email;
using Notification.Infrastructure.Database;
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
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly EmailConfiguration _emailConfiguration;

    public EmailHandler(IServiceScopeFactory scopeFactory, IOptions<EmailConfiguration> options)
    {
        _scopeFactory = scopeFactory;
        _emailConfiguration = options.Value;
    }
    public async Task<SaveEmailCommandRespond> Handle(SaveEmailCommand request, CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            var entity = Domain.Email.Email.CreateNew(request.Sender, request.EmailAddress, request.Subject, request.Body,_emailConfiguration.TryCount);
            await _context.Emails.AddAsync(entity);
            await _context.SaveChangesAsync();
            Log.Information("New Email Insert To DB");
            return new SaveEmailCommandRespond { Id = entity.Id };
        }        
    }
}

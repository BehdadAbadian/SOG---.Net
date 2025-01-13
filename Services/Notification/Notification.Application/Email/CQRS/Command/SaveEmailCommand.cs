﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contracts.Interface;
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

    public EmailHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task<SaveEmailCommandRespond> Handle(SaveEmailCommand request, CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            var entity = Domain.Email.Email.CreateNew(request.Sender, request.EmailAddress, request.Subject, request.Body,5);
            await _context.Emails.AddAsync(entity);
            await _context.SaveChangesAsync();
            Log.Information("New Email Insert To DB");
            return new SaveEmailCommandRespond { Id = entity.Id };
        }        
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notification.Application.Contracts.Interface;
using Notification.Application.Contracts.Share;
using Notification.Application.Email.CQRS.Command;
using Notification.Domain.Email;
using Notification.Infrastructure.Database;
using Notification.Infrastructure.Pattern;
using Serilog;

namespace Notification.API.BackgroundServices;

public class ReSendFailedEmailHostedService : IHostedService
{
    //private readonly IEmailRepository _repository;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMediator _mediator;
    private readonly EmailConfiguration _emailConfiguration;
    private readonly IUnitOfWork _unit;
    private readonly IEmailSender _emailSender;
    private Timer _timer;

    public ReSendFailedEmailHostedService(IEmailSender emailSender, IServiceScopeFactory scopeFactory,IMediator mediator,IOptions<EmailConfiguration> options)
    {
        //_repository = repository;
        //_unit = unit;
        _emailSender = emailSender;
        _scopeFactory = scopeFactory;
        _mediator = mediator;
        _emailConfiguration = options.Value;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SendFailedEmail, null, TimeSpan.Zero, TimeSpan.FromSeconds(_emailConfiguration.ReSendTime));
        return Task.CompletedTask;
    }

    private async void SendFailedEmail(object? state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            var Emails = await _context.Emails.Where(x => x.EmailStatus == Domain.Share.Status.Failed).ToListAsync();
            if (Emails.Count() > 0)
            {
                foreach (var Email in Emails)
                {
                    if(Email.TryCount > 0)
                    {
                        var IsSuccess = await _emailSender.SendEmail(Email.EmailAddress, Email.Sender, Email.Subject, Email.Body);
                        if (IsSuccess)
                        {
                            await _mediator.Send(new UpdateStatusCommand { Id = Email.Id, EmailStatus = Domain.Share.Status.Success });
                        }
                        else
                        {

                            Email.ChangeTryCount();
                            await _context.SaveChangesAsync();
                            Log.Information("TryCount of Email with ID : {0} is : {1}", Email.Id, Email.TryCount);
                        }
                    }
                    else
                    {
                        await _mediator.Send(new UpdateStatusCommand {Id = Email.Id, EmailStatus = Domain.Share.Status.Expire });
                        Log.Information("After {0} failed try for Resend Email with ID : {1} and Destination : {2}, Email Status Changed to {3}",_emailConfiguration.TryCount, Email.Id,Email.EmailAddress, Domain.Share.Status.Expire);
                    }
                    
                }
            }

        }
        //var Emails =await _repository.GetByStatusAsync(Domain.Share.Status.Failed);
        //if (Emails.Count() > 0)
        //{
        //    foreach (var Email in Emails)
        //    {
        //        var IsSuccess = _emailSender.SendEmail(Email.EmailAddress, Email.Sender, Email.Subject, Email.Body);
        //        if (IsSuccess) 
        //        {
        //            var email = await _repository.GetByIdAsync(Email.Id);
        //            email.ChangeStatus(Domain.Share.Status.Success);
        //            await _unit.SaveChangesAsync();
        //        }
        //    }
        //}
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}

namespace Notification.Application.Contracts.Interface;

public interface IEmailSender
{
    public Task<bool> SendEmail(string email,string sender, string subject, string messageBody);
}

namespace Notification.Application.Contracts.Interface;

public interface IEmailSender
{
    public bool SendEmail(string email,string sender, string subject, string messageBody);
}

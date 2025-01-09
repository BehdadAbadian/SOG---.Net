namespace Notification.Application.Contracts.Interface;

public interface IEmailSender
{
    public void SendEmail(string email,string sender, string subject, string messageBody);
}

using Notification.Domain.Share;

namespace Notification.Domain.Email;

public class Email
{
    public long Id { get; private set; }
    public string Sender { get; private set; }
    public string EmailAddress { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime SendDate { get; private set; }
    public Status EmailStatus { get; private set; }

    private Email()
    {
        
    }
    private Email(string sender, string emailAddress, string subject, string body)
    {
        Sender = sender;
        EmailAddress = emailAddress;
        Subject = subject;
        Body = body;
        CreationDate = DateTime.Now;
        EmailStatus = Status.InProgress;
    }
    public static Email CreateNew(string sender, string emailAddress, string subject, string body)
    {
        return new Email(sender, emailAddress, subject, body);
    }
    public void ChangeStatus(Status status)
    {
        EmailStatus = status;
        if(status == Status.Success)
        {
            SendDate = DateTime.Now;
        }
        
    }

}

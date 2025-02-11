﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Application.Contracts.Interface;
using Notification.Application.Contracts.Share;
using Serilog;

namespace Notification.Application.Email.Service;

public class EmailSenderService : IEmailSender
{
    private readonly EmailConfiguration _configuration;

    public EmailSenderService(IOptions<EmailConfiguration> options)
    {
        _configuration = options.Value;
    }
    public async Task<bool> SendEmail(string email,string sender, string subject, string messageBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(sender, _configuration.From));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = messageBody };
        try
        {
            using (var client = new SmtpClient())
            {

                client.Connect(_configuration.SmtpServer, _configuration.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_configuration.UserName, _configuration.Password);
                client.Send(message);
                client.Disconnect(true);
            }
            return true;

        }
        catch (Exception ex) 
        {
            Log.Error("SendEmail Failed  Error : " + ex.ToString());
            return false; 
        }
        
    }
}

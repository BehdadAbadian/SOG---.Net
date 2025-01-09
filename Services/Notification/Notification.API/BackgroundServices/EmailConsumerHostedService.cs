using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Notification.Application.Contracts.Dto;
using MediatR;
using Notification.Application.Email.CQRS.Command;
using Notification.Application.Contracts.Interface;

namespace Notification.API.BackgroundServices;

public class EmailConsumerHostedService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly IEmailSender _emailSender;

    public EmailConsumerHostedService(IMediator mediator, IEmailSender emailSender)
    {
        _mediator = mediator;
        _emailSender = emailSender;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderQueueName = "email-queue";
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: orderQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var email = JsonSerializer.Deserialize<EmailDto>(message);

            //_mediator.Send(new SaveEmailCommand{Sender = email.Sender, EmailAddress = email.EmailAddress, Subject = email.Subject, Body = email.Body});

            _emailSender.SendEmail(email.EmailAddress, email.Sender, email.Subject, email.Body);
            return Task.CompletedTask;
            //call another service
        };

        await channel.BasicConsumeAsync(queue: orderQueueName, autoAck: true, consumer: consumer);

        Console.WriteLine("Waiting for feedback. Press [enter] to exit.");
        Console.ReadLine();


    }
}

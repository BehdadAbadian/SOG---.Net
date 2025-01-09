namespace Security.Application.Contracts.Interface;

public interface IMessageBrokerService
{
    Task SendMessage<T>(string queueName, T message, string exchange = "");
}

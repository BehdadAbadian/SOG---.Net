namespace Security.Application.Contracts.Interface;

public interface IMessageBrokerConnectionService
{
    Task CreateChannelAsync(string queueName, byte[] body, string exchange);
}

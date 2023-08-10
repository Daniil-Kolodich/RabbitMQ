using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ_Test.Consumers;

public class UserRegisteredConsumer : AsyncDefaultBasicConsumer
{
    private readonly ILogger<UserRegisteredConsumer> _logger;
    public UserRegisteredConsumer(IModel model, ILogger<UserRegisteredConsumer> logger) : base(model)
    {
        _logger = logger;
    }

    public override Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey,
        IBasicProperties properties, ReadOnlyMemory<byte> body)
    {
        _logger.LogInformation($"Message received for '{routingKey}' by '{exchange}' with payload : {Encoding.UTF8.GetString(body.ToArray())}");
        Model.BasicAck(deliveryTag, false);
        
        return Task.CompletedTask;
    }
}
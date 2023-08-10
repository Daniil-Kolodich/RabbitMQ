using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ_Test.RabbitMQ.Concrete;

public class PublisherService : IPublisherService
{
    private readonly IModel _channel;

    public PublisherService(IModel channel)
    {
        _channel = channel;
    }

    public void Publish<T>(T message, string route, string exchange)
    {
        _channel.BasicPublish(exchange: exchange, routingKey: route, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }
}
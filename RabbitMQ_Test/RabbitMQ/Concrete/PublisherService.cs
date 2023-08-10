using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ_Test.RabbitMQ.Concrete;

public class PublisherService : IPublisherService
{
    private readonly IConnection _connection;

    public PublisherService(IConnection connection)
    {
        _connection = connection;
    }

    public void Publish<T>(T message, string route, string exchange)
    {
        // instead of this i can make true Singleton and handle everything there, and just reuse same channel across application
        // so, may be singleton is not that good because this will require manual implementation of it
        // i think it would be better to AddScoped<IModel> and provide factory method that will use singleton IConnection from DI
        // and this should take care about Disposable resources as well i believe
        // this will ensure that if multiple publishes are made during one session they will use same channel 
        using var channel = _connection.CreateModel();
        
        ArgumentNullException.ThrowIfNull(channel);

        // channel.QueueDeclare(route, exclusive: false, autoDelete: false, durable: true);
        channel.BasicPublish(exchange: exchange, routingKey: route, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }
}
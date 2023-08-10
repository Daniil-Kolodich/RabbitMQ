namespace RabbitMQ_Test.RabbitMQ;

public interface IPublisherService
{
    public void Publish<T>(T message, string route, string exchange = "");
}
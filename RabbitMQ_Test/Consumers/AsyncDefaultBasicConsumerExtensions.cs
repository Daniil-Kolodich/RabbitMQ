using RabbitMQ.Client;

namespace RabbitMQ_Test.Consumers;

public static class AsyncDefaultBasicConsumerExtensions
{
    public static void Consume(this AsyncDefaultBasicConsumer consumer, string queue)
    {
        consumer.Model.BasicConsume(queue, false, consumer);
    }
}
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ_Test.Consumers;

public class ConsumersBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsumersBackgroundService> _logger;
    
    public ConsumersBackgroundService(IServiceProvider serviceProvider, ILogger<ConsumersBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        using var scope = _serviceProvider.CreateAsyncScope();
        var factory = scope.ServiceProvider.GetRequiredService<IConnection>();
        var channel = factory.CreateModel();

        var userRegistration = new UserRegisteredConsumer(channel, scope.ServiceProvider.GetRequiredService<ILogger<UserRegisteredConsumer>>());
        
        userRegistration.Consume("user.ping");
    }
}
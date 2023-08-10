using RabbitMQ_Test.Consumers;
using RabbitMQ_Test.RabbitMQ;
using RabbitMQ_Test.RabbitMQ.Concrete;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(cfg => cfg.AddConsole());
builder.Services.Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information);

builder.Services.AddTransient<IPublisherService, PublisherService>();

builder.Services.AddSingleton<IConnection>(_ =>
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        Password = "1111",
        UserName = "danon",
        Port = 5672,
        DispatchConsumersAsync = true
    };
    
    return factory.CreateConnection();
});
builder.Services.AddScoped<IModel>(services =>
{
    var factory = services.GetRequiredService<IConnection>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("New channel created");
    
    return factory.CreateModel();
});

builder.Services.AddHostedService<ConsumersBackgroundService>();
builder.Services.AddScoped<UserRegisteredConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


using Microsoft.AspNetCore.Mvc;
using RabbitMQ_Test.RabbitMQ;

namespace RabbitMQ_Test.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private readonly IPublisherService _publisherService;
    private readonly ILogger<ProducerController> _logger;
    
    public ProducerController(ILogger<ProducerController> logger, IPublisherService publisherService)
    {
        _logger = logger;
        _publisherService = publisherService;
    }

    [HttpPost]
    public IActionResult Produce(string message)
    {
        _publisherService.Publish(message, "user.ping");
        _logger.LogInformation($"Message was sent, payload : {message}");
        return Ok();
    }
}
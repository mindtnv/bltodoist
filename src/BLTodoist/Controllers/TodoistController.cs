using BLStimulator.Contracts.Commands;
using BLTodoist.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BLTodoist.Controllers;

[ApiController]
public class TodoistController : ControllerBase
{
    private readonly ILogger<TodoistController> _logger;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public TodoistController(ILogger<TodoistController> logger, ISendEndpointProvider sendEndpointProvider)
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
    }

    [Route("/")]
    [HttpPost]
    public async Task<IActionResult> Post(TodoistItemRequest request)
    {
        _logger.LogInformation("[{RequestEventName}] {EventDataContent}", request.EventName, request.EventData.Content);
        if (request.EventName == "item:completed")
        {
            var s = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:Stimulation"));
            await s.Send<StimulateUser>(new
            {
                UserId = 1,
                Count = Random.Shared.Next(1, 3),
            });
        }

        return Ok();
    }
}
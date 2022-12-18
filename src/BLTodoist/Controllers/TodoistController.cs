using BLStimulator.Contracts.Commands;
using BLTodoist.Contracts;
using BLTodoist.Contracts.Events;
using BLTodoist.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BLTodoist.Controllers;

[ApiController]
public class TodoistController : ControllerBase
{
    private readonly ILogger<TodoistController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public TodoistController(ILogger<TodoistController> logger, ISendEndpointProvider sendEndpointProvider,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
        _publishEndpoint = publishEndpoint;
    }

    [Route("/")]
    [HttpPost]
    public async Task<IActionResult> Post(TodoistItemRequest request)
    {
        if (string.IsNullOrEmpty(request.EventName))
        {
            _logger.LogError("EventName from {UserId} is null", request.UserId);
            return BadRequest();
        }

        _logger.LogInformation("[{RequestEventName}] {EventDataContent}", request.EventName, request.EventData.Content);
        var item = request.EventData;
        var todo = new TodoDto
        {
            Content = item.Content ?? throw new ArgumentNullException(nameof(item.Content)),
            Difficult = (int) (item.Priority ?? throw new ArgumentNullException(nameof(item.Priority))),
        };

        if (request.EventName == "item:completed")
        {
            _logger.LogInformation("[{RequestEventName}] Send StimulateUser command...", request.EventName);
            var s = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:Stimulation"));
            await s.Send<StimulateUser>(new
            {
                UserId = 1,
                Count = Math.Max(todo.Difficult, 4),
            });
        }

        _logger.LogInformation("[{RequestEventName}] Publishing event...", request.EventName);
        await _publishEndpoint.Publish(request.EventName switch
        {
            "item:added" => new TodoAdded
            {
                Todo = todo,
                TimeStamp = DateTime.UtcNow,
            },
            "item:completed" => new TodoCompleted
            {
                Todo = todo,
                TimeStamp = DateTime.UtcNow,
            },
            "item:deleted" => new TodoDeleted
            {
                Todo = todo,
                TimeStamp = DateTime.UtcNow,
            },
            "item:updated" => new TodoUpdated
            {
                Todo = todo,
                TimeStamp = DateTime.UtcNow,
            },
            "item:uncompleted" => new TodoUncompleted
            {
                Todo = todo,
                TimeStamp = DateTime.UtcNow,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(request.EventName), request.EventName, null),
        });

        return Ok();
    }
}
using System.Text.Json.Serialization;

namespace BLTodoist.Models;

public class TodoistItemRequest
{
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }
    [JsonPropertyName("event_data")]
    public TodoistItemEventData EventData { get; set; }
}
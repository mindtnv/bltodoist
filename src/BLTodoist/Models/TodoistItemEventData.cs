using System.Text.Json.Serialization;
using Todoist.Net.Models;

namespace BLTodoist.Models;

public class TodoistItemEventData
{
    [JsonPropertyName("added_by_uid")]
    public string? AddedByUid { get; set; }
    [JsonPropertyName("assigned_by_uid")]
    public string? AssignedByUid { get; set; }
    [JsonPropertyName("child_order")]
    public int? ChildOrder { get; set; }
    [JsonPropertyName("collapsed")]
    public bool? Collapsed { get; set; }
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("added_at")]
    public DateTime? AddedAt { get; internal set; }
    [JsonPropertyName("day_order")]
    public int? DayOrder { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("due")]
    public TodoistDueDate? DueDate { get; set; }
    [JsonPropertyName("checked")]
    public bool? IsChecked { get; internal set; }
    [JsonPropertyName("is_deleted")]
    public bool? IsDeleted { get; internal set; }
    [JsonPropertyName("labels")]
    public ICollection<string>? Labels { get; internal set; }
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }
    [JsonPropertyName("priority")]
    public Priority? Priority { get; set; }
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }
    [JsonPropertyName("responsible_uid")]
    public string? ResponsibleUid { get; set; }
    [JsonPropertyName("section_id")]
    public string? Section { get; set; }
    [JsonPropertyName("user_id")]
    public string? UserId { get; internal set; }
}

public class TodoistDueDate : DueDate
{
    public TodoistDueDate() : this(null)
    {
    }

    public TodoistDueDate(DateTime date, bool isFullDay = false, string timezone = null) : base(date, isFullDay,
        timezone)
    {
    }

    public TodoistDueDate(string text, string timezone = null, Language language = null) : base(text, timezone,
        language)
    {
    }
}
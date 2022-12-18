namespace BLTodoist.Contracts.Events;

public class TodoCompleted
{
    public TodoDto Todo { get; set; }
    public DateTime TimeStamp { get; set; }
}
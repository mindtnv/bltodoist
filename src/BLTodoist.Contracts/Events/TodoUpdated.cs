namespace BLTodoist.Contracts.Events;

public class TodoUpdated
{
    public TodoDto Todo { get; set; }
    public DateTime TimeStamp { get; set; }
}
namespace BLTodoist.Contracts.Events;

public class TodoDeleted
{
    public TodoDto Todo { get; set; }
    public DateTime TimeStamp { get; set; }
}
namespace BLTodoist.Contracts.Events;

public class TodoAdded
{
    public TodoDto Todo { get; set; }
    public DateTime TimeStamp { get; set; }
}
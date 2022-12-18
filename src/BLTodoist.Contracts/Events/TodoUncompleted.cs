namespace BLTodoist.Contracts.Events;

public class TodoUncompleted
{
    public TodoDto Todo { get; set; }
    public DateTime TimeStamp { get; set; }
}
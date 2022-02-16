namespace Common;

public class HourlyChatRoomEvent
{
    public int EventTypeId { get; set; }
    public string Name { get; set; }
    public int CountType { get; set; }
    public int UserInAction { get; set; }
    public int TargetUser { get; set; }
    public DateTime HourPart { get; set; }
}
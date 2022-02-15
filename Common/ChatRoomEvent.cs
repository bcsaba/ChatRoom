namespace Common;

public class ChatRoomEvent
{
    public int? Id { get; set; }
    public RoomEventType RoomEventType { get; set; }
    public DateTime EventTime { get; set; }
    public User User { get; set; }
    public string Post { get; set; }
    public User? TargetUser { get; set; }

}
namespace Common;

public class ChatRoom
{
    public int? ChatRoomId { get; set; }
    public string Name { get; set; }
    public int CreatedById { get; set; }
    public User? CreateBy { get; set; }
    public DateTime? CreatoinTime { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Repository;

public class ChatRoom
{
    [Key]
    public int ChatRoomId { get; set; }
    public string Name { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatoinTime { get; set; }

    public virtual List<RoomEvent> RoomEvents { get; set; }
}
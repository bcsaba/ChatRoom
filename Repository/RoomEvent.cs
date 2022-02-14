using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class RoomEvent
{
    [Key]
    public int Id { get; set; }
    public RoomEventType EventType { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("TargetUser")]
    public int? TargetUserId { get; set; }
    public User TargetUser { get; set; }

    public DateTime EventTime { get; set; }
    public ChatRoom ChatRoom { get; set; }
}
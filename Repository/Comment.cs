using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public string CommentString { get; set; }

    [ForeignKey("RoomPosted")]
    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }

    [ForeignKey("PostingUser")]
    public User User { get; set; }

    [ForeignKey("PostEvent")] 
    public RoomEvent RoomEvent { get; set; }
}

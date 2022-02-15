using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class Post
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; }

    [ForeignKey("RoomPosted")]
    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }

    [ForeignKey("PostingUser")]
    public User User { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class ChatRoom
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreationTime { get; set; }

    [InverseProperty("ChatRoom")]
    public ICollection<RoomEvent> RoomEvents  { get; set; }
    //[InverseProperty("RoomPosted")]
    public ICollection<Post> Posts { get; set; }
}
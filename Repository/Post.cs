using System.ComponentModel.DataAnnotations;

namespace Repository;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Content { get; set; }
    public ChatRoom ChatRoom { get; set; }
    public User User { get; set; }
}

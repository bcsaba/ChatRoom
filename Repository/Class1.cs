using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
    :base(options)
    {
        
    }

    public DbSet<User> Blogs { get; set; }
    public DbSet<RoomEventType> RoomEventTypes { get; set; }
    public DbSet<ChatRoom> ChatRooms{ get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=192.168.0.111;Database=chatroom;Username=chatroom1;Password=Chat11Room");
}

public class User
{
    [Key]
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}

public class ChatRoom
{
    [Key]
    public int ChatRoomId { get; set; }
    public string Name { get; set; }

    public List<RoomEvent> RoomEvents { get; set; }
}

public class RoomEvent
{
    [Key]
    public int RoomEventId { get; set; }
    public User User { get; set; }
    public RoomEventType EventType { get; set; }
}

public class RoomEventType
{
    [Key]
    public int RoomEventTypeId { get; set; }
    public string Name { get; set; }
}

public class Post
{
    [Key]
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public User User { get; set; }
}

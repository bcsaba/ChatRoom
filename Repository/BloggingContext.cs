using Microsoft.EntityFrameworkCore;

namespace Repository;

public interface IBloggingContext
{
    DbSet<User> Users { get; set; }
    DbSet<RoomEvent> RoomEvents { get; set; }
    DbSet<RoomEventType> RoomEventTypes { get; set; }
    DbSet<ChatRoom> ChatRooms { get; set; }
    DbSet<Post> Posts { get; set; }
}

public class BloggingContext : DbContext, IBloggingContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
        :base(options)
    {
        
    }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<RoomEvent> RoomEvents { get; set; }
    public DbSet<RoomEventType> RoomEventTypes { get; set; }
    public DbSet<ChatRoom> ChatRooms{ get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=192.168.0.111;Database=chatroom;Username=chatroom1;Password=Chat11Room");
}
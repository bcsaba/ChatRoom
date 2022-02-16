using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ChatRoomContext : DbContext, IChatRoomContext
{
    public ChatRoomContext(DbContextOptions<ChatRoomContext> options)
        :base(options)
    {
        
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<RoomEvent> RoomEvents { get; set; }
    public virtual DbSet<RoomEventType> RoomEventTypes { get; set; }
    public virtual DbSet<ChatRoom> ChatRooms{ get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<HourlyChatRoomEvent> HourlyChatRoomEvent { get; set; }

    //public IEnumerable<HourluChatRoomEvent> GetHourlyChatRoomDataFunc()
    //    => throw new NotSupportedException();
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=192.168.0.111;Database=chatroom;Username=chatroom1;Password=Chat11Room");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HourlyChatRoomEvent>().HasNoKey().ToFunction("select * from gethourlychatroomdatafunc()");

        //modelBuilder.HasDbFunction(typeof(ChatRoomContext).GetMethod(nameof(GetHourlyChatRoomDataFunc)))
        //    .HasName("getHourlyChatRoomDataFunc");
    }
}
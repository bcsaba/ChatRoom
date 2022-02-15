using Microsoft.EntityFrameworkCore;

namespace Repository;

public interface IChatRoomContext
{
    DbSet<User> Users { get; set; }
    DbSet<RoomEvent> RoomEvents { get; set; }
    DbSet<RoomEventType> RoomEventTypes { get; set; }
    DbSet<ChatRoom> ChatRooms { get; set; }
    DbSet<Comment> Comments { get; set; }


    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Repository;

public interface IChatRoomContext
{
    DbSet<User> Users { get; set; }
    DbSet<RoomEvent> RoomEvents { get; set; }
    DbSet<RoomEventType> RoomEventTypes { get; set; }
    DbSet<ChatRoom> ChatRooms { get; set; }
    DbSet<Comment> Comments { get; set; }
    DbSet<HourlyChatRoomEvent> HourlyChatRoomEvent { get; set; }

    DatabaseFacade Database { get; }

    //IEnumerable<HourluChatRoomEvent> GetHourlyChatRoomDataFunc();

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
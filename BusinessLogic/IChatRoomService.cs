using Common;

namespace BusinessLogic;

public interface IChatRoomService
{
    Task<ChatRoom> AddChatRoom(ChatRoom chatRoom);
    Task<ChatRoom> UpdateChatRoom(ChatRoom chatRoom);
    Task<IEnumerable<ChatRoom>> GetChatRooms();
}
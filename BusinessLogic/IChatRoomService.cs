using Common;
using ChatRoom = Common.ChatRoom;

namespace BusinessLogic;

public interface IChatRoomService
{
    Task<ChatRoom> AddChatRoom(ChatRoom chatRoom);
    Task<ChatRoom> UpdateChatRoom(ChatRoom chatRoom);
    Task<IEnumerable<ChatRoom>> GetChatRooms();
    Task<ChatRoomEvent> AddAction(int chatRoomId, int userId, int eventTypeId, ChatRoomEventInfo eventInfo);
}

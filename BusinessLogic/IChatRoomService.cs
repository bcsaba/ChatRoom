using Common;
using Repository;
using ChatRoom = Common.ChatRoom;

namespace BusinessLogic;

public interface IChatRoomService
{
    Task<ChatRoom> AddChatRoom(ChatRoom chatRoom);
    Task<ChatRoom> UpdateChatRoom(ChatRoom chatRoom);
    Task<IEnumerable<ChatRoom>> GetChatRooms();
    Task<ChatRoomEvent> AddAction(int chatRoomId, int userId, int eventTypeId, ChatRoomEventInfo eventInfo);
    Task<IEnumerable<ChatRoomEvent>> GetMinuteByMinuteEvents(int chatRoomId);
    Task<IEnumerable<Common.HourlyChatRoomEvent>> GetHourlyEvents(int chatRoomId);
}

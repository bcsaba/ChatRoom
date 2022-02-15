using System.Security.Claims;
using Common;
using Microsoft.EntityFrameworkCore;
using Repository;
using ChatRoom = Common.ChatRoom;
using RoomEventType = Common.RoomEventType;
using User = Repository.User;

namespace BusinessLogic;

public class ChatRoomService : IChatRoomService
{
    private readonly IChatRoomContext _chatRoomContext;

    public ChatRoomService(IChatRoomContext chatRoomContext)
    {
        _chatRoomContext = chatRoomContext;
    }

    public async Task<ChatRoom> AddChatRoom(ChatRoom chatRoom)
    {
        var repoChatRoom = ToRepositoryChatRoom(chatRoom);
        await _chatRoomContext.ChatRooms.AddAsync(repoChatRoom);
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(repoChatRoom);
    }


    public async Task<ChatRoom> UpdateChatRoom(ChatRoom chatRoom)
    {
        var repoChatRoom = _chatRoomContext.ChatRooms.Include(x => x.CreatedBy)
            .Single(x => x.Id == chatRoom.ChatRoomId);
        repoChatRoom.Name = chatRoom.Name;
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(repoChatRoom);
    }

    public Task<IEnumerable<ChatRoom>> GetChatRooms()
    {
        return ToDomainEntity(_chatRoomContext.ChatRooms);
    }

    public async Task<ChatRoomEvent> AddAction(int chatRoomId, int userId, int eventTypeId, ChatRoomEventInfo eventInfo)
    {
        var roomEvent = new RoomEvent()
        {
            ChatRoom = _chatRoomContext.ChatRooms.Single(x => x.Id == chatRoomId),
            ChatRoomId = chatRoomId,
            EventTime = DateTime.UtcNow,
            EventType = _chatRoomContext.RoomEventTypes.Single(x => x.Id == eventTypeId),
            User = _chatRoomContext.Users.Single(x => x.Id == userId),
            UserId = userId
        };
        if (eventTypeId == (int) ChatRoomEvents.HighFiveOtherUse)
        {
            roomEvent.TargetUser = _chatRoomContext.Users.Single(x => x.Id == eventInfo.HighFivedUserId);
        }

        await _chatRoomContext.RoomEvents.AddAsync(roomEvent);
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(roomEvent);
    }

    private ChatRoomEvent ToDomainEntity(RoomEvent repoChatRoom)
    {
        return new ChatRoomEvent()
        {
            Id = repoChatRoom.Id,
            EventTime = repoChatRoom.EventTime,
            // TODO: include Post here
            // Post = repoChatRoom.
            RoomEventType = ToDomainEntity(repoChatRoom.EventType),
            User = ToDomainEntity(repoChatRoom.User),
            TargetUser = repoChatRoom.EventType.Id == (int) ChatRoomEvents.HighFiveOtherUse
                ? ToDomainEntity(repoChatRoom.TargetUser)
                : null
        };
    }

    private RoomEventType ToDomainEntity(Repository.RoomEventType eventType)
    {
        return new RoomEventType()
        {
            Id = eventType.Id,
            Name = eventType.Name
        };
    }

    private async Task<IEnumerable<ChatRoom>> ToDomainEntity(DbSet<Repository.ChatRoom> chatRooms)
    {
        var chatRoomList = await chatRooms.Include(x => x.CreatedBy).ToListAsync();
        return chatRoomList.Select(x => new ChatRoom()
        {
            ChatRoomId = x.Id,
            Name = x.Name,
            CreatedById = ToDomainEntity(x.CreatedBy).Id!.Value,
            CreateBy = ToDomainEntity(x.CreatedBy),
            CreatoinTime = x.CreationTime
        });
    }

    private ChatRoom ToDomainEntity(Repository.ChatRoom repoChatRoom)
    {
        return new ChatRoom
        {
            ChatRoomId = repoChatRoom.Id,
            Name = repoChatRoom.Name,
            CreatedById = ToDomainEntity(repoChatRoom.CreatedBy).Id!.Value,
            CreatoinTime = repoChatRoom.CreationTime
        };
    }

    private Common.User ToDomainEntity(User repositoryUser)
    {
        return new Common.User
        {
            Id = repositoryUser.Id,
            FirstName = repositoryUser.FirstName,
            LastName = repositoryUser.LastName,
            NickNAme = repositoryUser.NickNAme
        };
    }
    private Repository.ChatRoom ToRepositoryChatRoom(ChatRoom chatRoom)
    {
        return new Repository.ChatRoom()
        {
            Name = chatRoom.Name,
            CreatedBy = _chatRoomContext.Users.Single(x => x.Id == chatRoom.CreatedById),
            CreationTime = DateTime.UtcNow
        };
    }
}

public enum ChatRoomEvents
{
    EnterRoom = 1,
    LeaveRoom = 2,
    Comment = 3,
    HighFiveOtherUse = 4
}
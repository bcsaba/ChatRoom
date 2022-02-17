using Common;
using Microsoft.EntityFrameworkCore;
using Repository;
using ChatRoom = Common.ChatRoom;
using Comment = Common.Comment;
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
        var chatRoom = _chatRoomContext.ChatRooms.Single(x => x.Id == chatRoomId);
        var user = _chatRoomContext.Users.Single(x => x.Id == userId);
        var roomEvent = new RoomEvent
        {
            ChatRoom = chatRoom,
            ChatRoomId = chatRoomId,
            EventTime = DateTime.UtcNow,
            EventType = _chatRoomContext.RoomEventTypes.Single(x => x.Id == eventTypeId),
            User = user,
            UserId = userId
        };
        if (eventTypeId == (int) ChatRoomEvents.HighFiveOtherUse)
        {
            roomEvent.TargetUser = _chatRoomContext.Users.Single(x => x.Id == eventInfo.HighFivedUserId);
        }

        if (eventTypeId == (int)ChatRoomEvents.Comment)
        {
            var post = new Repository.Comment
            {
                ChatRoom = chatRoom,
                ChatRoomId = chatRoom.Id,
                User = user,
                CommentString = eventInfo.Comment,
                RoomEvent = roomEvent
            };
            await _chatRoomContext.Comments.AddAsync(post);
        }

        await _chatRoomContext.RoomEvents.AddAsync(roomEvent);
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(roomEvent);
    }

    public async Task<IEnumerable<ChatRoomEvent>> GetMinuteByMinuteEvents(int chatRoomId)
    {
            var roomEvents =
                _chatRoomContext.RoomEvents.Include(x => x.EventType).Include(x => x.User)
                    .Where(x => x.ChatRoomId == chatRoomId)
                    .OrderBy(x => x.EventTime);
            var roomEventsList = await roomEvents.ToListAsync();

            return roomEventsList.Select(ToDomainEntity);
    }

    public async Task<IEnumerable<Common.HourlyChatRoomEvent>> GetHourlyEvents(int chatRoomId)
    {
            IQueryable<Repository.HourlyChatRoomEvent> chatRoomEvents = _chatRoomContext.HourlyChatRoomEvent.FromSqlRaw<Repository.HourlyChatRoomEvent>($"select * from getHourlyChatRoomDataFunc({chatRoomId})");
            var hourlyChatRoomEvents = await chatRoomEvents.ToListAsync<Repository.HourlyChatRoomEvent>();

            return hourlyChatRoomEvents.Select(ToDomainEntity);
    }

    private ChatRoomEvent ToDomainEntity(RoomEvent roomEvent)
    {
        return new ChatRoomEvent()
        {
            Id = roomEvent.Id,
            EventTime = roomEvent.EventTime,
            RoomEventType = ToDomainEntity(roomEvent.EventType),
            User = ToDomainEntity(roomEvent.User),
            TargetUser = roomEvent.EventType.Id == (int) ChatRoomEvents.HighFiveOtherUse
                ? ToDomainEntity(roomEvent.TargetUser!)
                : null,
            Comment = roomEvent.EventType.Id == (int) ChatRoomEvents.Comment
                ? ToDomainEntity(_chatRoomContext.Comments.Single(x => x.RoomEvent == roomEvent))
                : null
        };
    }

    private Common.HourlyChatRoomEvent ToDomainEntity(Repository.HourlyChatRoomEvent roomEvent)
    {
        return new Common.HourlyChatRoomEvent()
        {
            EventTypeId = roomEvent.EventTypeId,
            Name = roomEvent.Name,
            HourPart = roomEvent.HourPart,
            CountType = roomEvent.CountType,
            TargetUser = roomEvent.TargetUser,
            UserInAction = roomEvent.UserInAction
        };
    }

    private Comment ToDomainEntity(Repository.Comment comment)
    {
        return new Comment
        {
            Id = comment.Id,
            CommentString = comment.CommentString
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
            NickName = repositoryUser.NickNAme
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
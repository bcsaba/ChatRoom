using Microsoft.EntityFrameworkCore;
using Repository;
using ChatRoom = Common.ChatRoom;

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
            .Single(x => x.ChatRoomId == chatRoom.ChatRoomId);
        repoChatRoom.Name = chatRoom.Name;
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(repoChatRoom);
    }

    public Task<IEnumerable<ChatRoom>> GetChatRooms()
    {
        return ToDomainEntity(_chatRoomContext.ChatRooms);
    }

    private async Task<IEnumerable<ChatRoom>> ToDomainEntity(DbSet<Repository.ChatRoom> chatRooms)
    {
        var chatRoomList = await chatRooms.Include(x => x.CreatedBy).ToListAsync();
        return chatRoomList.Select(x => new ChatRoom()
        {
            ChatRoomId = x.ChatRoomId,
            Name = x.Name,
            CreatedById = ToDomainEntity(x.CreatedBy).Id!.Value,
            CreateBy = ToDomainEntity(x.CreatedBy),
            CreatoinTime = x.CreatoinTime
        });
    }

    private ChatRoom ToDomainEntity(Repository.ChatRoom repoChatRoom)
    {
        return new ChatRoom
        {
            ChatRoomId = repoChatRoom.ChatRoomId,
            Name = repoChatRoom.Name,
            CreatedById = ToDomainEntity(repoChatRoom.CreatedBy).Id!.Value,
            CreatoinTime = repoChatRoom.CreatoinTime
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
            CreatoinTime = DateTime.UtcNow
        };
    }
}
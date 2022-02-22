using Microsoft.EntityFrameworkCore;
using Repository;
using User = Common.User;

namespace BusinessLogic;

public class UserService : IUserService
{
    private readonly IChatRoomContext _chatRoomContext;

    public UserService(IChatRoomContext chatRoomContext)
    {
        _chatRoomContext = chatRoomContext;
    }

    public async Task<User> AddUser(User user)
    {
        var repositoryUser = ToRepositoryUser(user);
        await _chatRoomContext.Users.AddAsync(repositoryUser);
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(repositoryUser);
    }

    public async Task<User> UpdateUser(User user)
    {
        var repositoryUser = _chatRoomContext.Users.Single(x => x.Id == user.Id);
        repositoryUser.FirstName = user.FirstName;
        repositoryUser.LastName = user.LastName;
        repositoryUser.NickNAme = user.NickName;
        await _chatRoomContext.SaveChangesAsync();
        return ToDomainEntity(repositoryUser);
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        return ToDomainEntity(_chatRoomContext.Users);
    }

    private async Task<IEnumerable<User>> ToDomainEntity(DbSet<Repository.User> users)
    {
        var userList = await users.ToListAsync();
        return userList.Select(x => new User()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            NickName = x.NickNAme
        });
    }

    private User ToDomainEntity(Repository.User repositoryUser)
    {
        return new User
        {
            Id = repositoryUser.Id,
            FirstName = repositoryUser.FirstName,
            LastName = repositoryUser.LastName,
            NickName = repositoryUser.NickNAme
        };
    }

    private Repository.User ToRepositoryUser(User domainUser)
    {
        return new Repository.User()
        {
            FirstName = domainUser.FirstName,
            LastName = domainUser.LastName,
            NickNAme = domainUser.NickName
        };
    }
}
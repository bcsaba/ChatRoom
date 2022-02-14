using Microsoft.EntityFrameworkCore;
using Repository;
using User = Common.User;

namespace BusinessLogic;

public class UserService : IUserService
{
    private readonly IBloggingContext _bloggingContext;

    public UserService(IBloggingContext bloggingContext)
    {
        _bloggingContext = bloggingContext;
    }

    public async Task<User> AddUser(User user)
    {
        var repositoryUser = ToRepositoryUser(user);
        await _bloggingContext.Users.AddAsync(repositoryUser);
        await _bloggingContext.SaveChangesAsync();
        return ToDomainEntity(repositoryUser);
    }

    public async Task<User> UpdateUser(User user)
    {
        var repositoryUser = _bloggingContext.Users.Single(x => x.Id == user.Id);
        repositoryUser.FirstName = user.FirstName;
        repositoryUser.LastName = user.LastName;
        repositoryUser.NickNAme = user.NickNAme;
        await _bloggingContext.SaveChangesAsync();
        return ToDomainEntity(repositoryUser);
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        //return Task.FromResult<IEnumerable<User>>(new List<User>());
        return ToDomainEntity(_bloggingContext.Users);
    }

    private async Task<IEnumerable<User>> ToDomainEntity(DbSet<Repository.User> users)
    {
        var userList = await users.ToListAsync();
        return userList.Select(x => new User()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            NickNAme = x.NickNAme
        });
    }

    private User ToDomainEntity(Repository.User repositoryUser)
    {
        return new User
        {
            Id = repositoryUser.Id,
            FirstName = repositoryUser.FirstName,
            LastName = repositoryUser.LastName,
            NickNAme = repositoryUser.NickNAme
        };
    }

    private Repository.User ToRepositoryUser(User domainUser)
    {
        return new Repository.User()
        {
            FirstName = domainUser.FirstName,
            LastName = domainUser.LastName,
            NickNAme = domainUser.NickNAme
        };
    }
}
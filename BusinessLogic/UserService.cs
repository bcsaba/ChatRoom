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
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
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
}
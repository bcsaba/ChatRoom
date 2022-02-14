using Common;

namespace BusinessLogic;

public interface IUserService
{
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task<IEnumerable<User>> GetUsers();
}
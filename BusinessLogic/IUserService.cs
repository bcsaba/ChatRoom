using Common;

namespace BusinessLogic;

public interface IUserService
{
    Task<User> AddUser();
    Task<User> UpdateUser();
    Task<IEnumerable<User>> GetUsers();
}

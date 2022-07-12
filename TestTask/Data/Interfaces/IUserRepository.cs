using TestTask.Data.DataTransferObject;
using TestTask.Data.Models;


namespace TestTask.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> AllUsers();

        Task<bool> CreateUser(User user);

        Task<User> GetUserForLogin(string? email, string? password);

        Task<List<UserForAdminTableDto>> GetUsersForAdmin();

        Task<string> GetUserIdByEmail(string email);

        Task<string> GetUserRoleId(string id);
    }
}

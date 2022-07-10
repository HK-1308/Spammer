using TestTask.Data.DataTransferObject;
using TestTask.Models;

namespace TestTask.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> AllUsers();

        Task<User> GetUser(string? email);

        Task<List<UserForAdminTableDto>> GetUsersForAdmin();
    }
}

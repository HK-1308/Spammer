using TestTask.Data.DataTransferObject;
using TestTask.Data.Models;

namespace TestTask.Data.Interfaces
{
    public interface IAdminStatisticRepository
    {
        public Task<List<UserForAdminTableDto>> GetUsersForAdmin();

        public Task<bool> AddRecord(Job job, string status);

        Task<List<UserForUsageHistoryDto>> GetUserHistory(string userEmail);
        Task<List<UserForUsageHistoryDto>> GetHistory();
    }
}

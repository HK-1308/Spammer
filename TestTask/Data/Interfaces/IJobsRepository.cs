
using TestTask.Data.Models;

namespace TestTask.Data.Interfaces
{
    public interface IJobsRepository
    {
        Task<List<Job>> GetAllJobsAsync();
        Task<List<Job>> GetAllUsersJobsAsync(string email);

        Task<Job> GetJobAsync(string jobId);
        Task<List<Job>> GetExpiredJobs();
        Task<int> RemoveJob(Job job); 
        Task<int> AddJob(Job job);

        Task<int> UpdateJob(Job job);
    }
}

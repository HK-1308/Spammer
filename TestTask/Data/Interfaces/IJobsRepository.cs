
using TestTask.Data.DataTransferObject;
using TestTask.Data.Models;

namespace TestTask.Data.Interfaces
{
    public interface IJobsRepository
    {
        Task<List<Job>> GetAllUsersJobsAsync(string email);

        Task<Job> GetJobAsync(string jobId);
        Task<List<Job>> GetExpiredJobs();
        Task<bool> RemoveJob(string id); 
        Task<bool> AddJob(Job job);

        Task<bool> UpdateJob(Job job);
        Task<bool> UpdateJob(JobForListDto job);
    }
}

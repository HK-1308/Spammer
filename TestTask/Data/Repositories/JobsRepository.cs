using Microsoft.EntityFrameworkCore;
using TestTask.Data.Interfaces;
using TestTask.Data.Models;

namespace TestTask.Data.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private DataContext context;

        public JobsRepository(DataContext dataContext)
        {
            context = dataContext;
        }

        public async Task<List<Job>> GetAllJobsAsync() => await context.Job.ToListAsync();

        public async Task<List<Job>> GetAllUsersJobsAsync(string email) => await context.Job.Where(job=>job.UserEmail == email).ToListAsync();

        public async Task<int> AddJob(Job job)
        {
            await context.Job.AddAsync(job);
            return await context.SaveChangesAsync();
        }

        public async Task<Job> GetJobAsync(string jobId) => await context.Job.FirstOrDefaultAsync(job=>job.Id == jobId);

        public async Task<List<Job>> GetExpiredJobs() => await context.Job.Where(job => job.NextExecutionDate <= DateTime.Now).ToListAsync();
        public async Task<int> RemoveJob(Job job)
        {
            context.Job.Remove(job);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateJob(Job job)
        {
            context.Job.Update(job);
            return await context.SaveChangesAsync();
        }

    }
}

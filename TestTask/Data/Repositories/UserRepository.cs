using TestTask.Data.Interfaces;
using TestTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TestTask.Data.DataTransferObject;

namespace TestTask.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;

        public UserRepository(DataContext dataContext)
        {
            context = dataContext;
        }

        public async Task<List<User>> AllUsers() => await context.User.Include(user => user.Jobs).ToListAsync();

        public async Task<User> GetUser(string? email) => await context.User.FirstOrDefaultAsync(user => user.Email == email);

        public async Task<List<UserForAdminTableDto>> GetUsersForAdmin() 
        {
            List<UserForAdminTableDto> users = await context.User.Join(context.Job, u => u.Id, j => j.UserId, (u, j) => new 
            {
                Email = u.Email,
                JobId =  j.Id,
                LastExecutionDate = j.LastExecutionDate,
                NextExecutionDate = j.NextExecutionDate
            })
                .GroupBy(user => user.Email)
                .Select(group => new UserForAdminTableDto { Email = group.Key, NumberOfTasks = group.Count(), LastExecutionDate = group.Max(group => group.LastExecutionDate), NextExecutionDate = group.Min(group => group.NextExecutionDate) })
                .ToListAsync();

            List<UserForAdminTableDto> usersWithoutJobs = await context.User.Include(user => user.Jobs).Where(user => user.Jobs.Count == 0).Select(user => new UserForAdminTableDto { Email = user.Email, NumberOfTasks = 0, LastExecutionDate = null, NextExecutionDate = null }).ToListAsync();
            users.AddRange(usersWithoutJobs);
            return users;
        }

    }
}

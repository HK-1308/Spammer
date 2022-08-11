using Microsoft.Data.Sqlite;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Interfaces;
using TestTask.Data.Models;

namespace TestTask.Data.Repositories
{
    public class JobsRepository : IJobsRepository
    {

        public JobsRepository()
        {

        }

        public async Task<List<Job>> GetAllUsersJobsAsync(string email)
        {
            List<Job> jobs = new List<Job>();
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM Job WHERE UserEmail = @UserEmail";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@UserEmail", email);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read())  
                            {
                                var id = reader.GetString(0);
                                var name = reader.GetString(1);
                                var description = reader.GetString(2);
                                DateTime? lastExecutionDate;
                                if (reader[3].GetType() != typeof(DBNull))
                                {
                                    lastExecutionDate = DateTime.Parse(reader[3].ToString());
                                }
                                else lastExecutionDate = null;
                                var nextExecutionDate = DateTime.Parse(reader[4].ToString());
                                var period = reader.GetInt32(5);
                                var api = reader.GetString(6);
                                var param = reader.GetString(7);
                                var userId = reader.GetString(8);
                                var userEmail = reader.GetString(9);
                                var periodFormat = reader.GetString(10);
                                jobs.Add( new Job() { Id = id, Name = name, Description = description, LastExecutionDate = lastExecutionDate, PeriodFormat = periodFormat,
                                NextExecutionDate = nextExecutionDate, Period = period, UserId = userId, UserEmail = userEmail, Params = param, ApiUrlForJob = api});
                            }
                        }
                    }
                }
            });
            return jobs;
        }
        public async Task<bool> AddJob(Job job)
        {
            int number = 0;
            await Task.Run(() =>
            {
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Job (Id,Name, Description, NextExecutionDate, Period, ApiUrlForJob, Params, UserId, UserEmail, PeriodFormat) " +
                                          $"VALUES ( @JobId, @JobName, @JobDescription, @NextExecutionDate, @Period," +
                                          $" @ApiUrlForJob, @Params, @UserId, @UserEmail,@PeriodFormat)";
                    command.Parameters.AddWithValue("@JobId", job.Id);
                    command.Parameters.AddWithValue("@JobName", job.Name);
                    command.Parameters.AddWithValue("@JobDescription", job.Description);
                    command.Parameters.AddWithValue("@NextExecutionDate", job.NextExecutionDate);
                    command.Parameters.AddWithValue("@Period", job.Period);
                    command.Parameters.AddWithValue("@ApiUrlForJob", job.ApiUrlForJob);
                    command.Parameters.AddWithValue("@Params", job.Params);
                    command.Parameters.AddWithValue("@UserId", job.UserId);
                    command.Parameters.AddWithValue("@UserEmail", job.UserEmail);
                    command.Parameters.AddWithValue("@PeriodFormat", job.PeriodFormat);
                    number = command.ExecuteNonQuery();

                }
            }
            );
            if (number > 0) return true;
            return false;
        }

        public async Task<Job> GetJobAsync(string jobId)
        {
            Job Job = new Job();
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM Job WHERE Id = @JobId";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@JobId", jobId);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read())   
                            {
                                var id = reader.GetString(0);
                                var name = reader.GetString(1);
                                var description = reader.GetString(2);                             
                                var nextExecutionDate = DateTime.Parse(reader[4].ToString());
                                var period = reader.GetInt32(5);
                                var api = reader.GetString(6);
                                var param = reader.GetString(7);
                                var userId = reader.GetString(8);
                                var userEmail = reader.GetString(9);
                                var periodFormat = reader.GetString(10);
                                Job = new Job(){Id = id, Name = name, Description = description, ApiUrlForJob = api, PeriodFormat = periodFormat,
                                NextExecutionDate = nextExecutionDate, Period = period, UserId = userId, UserEmail = userEmail};
                            }
                        }
                    }
                }
            });
            return Job;
        }

        public async Task<List<Job>> GetExpiredJobs()
        {
            List<Job> jobs = new List<Job>();
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM Job";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read())  
                            {
                                var id = reader.GetString(0);
                                var name = reader.GetString(1);
                                var description = reader.GetString(2);
                                DateTime? lastExecutionDate;
                                if (reader[3].GetType() != typeof(DBNull))
                                {
                                    lastExecutionDate = DateTime.Parse(reader[3].ToString());
                                }
                                else lastExecutionDate = null;
                                var nextExecutionDate = DateTime.Parse(reader[4].ToString());
                                var period = reader.GetInt32(5);
                                var api = reader.GetString(6);
                                var param = reader.GetString(7);
                                var userId = reader.GetString(8);
                                var userEmail = reader.GetString(9);
                                var periodFormat = reader.GetString(10);
                                jobs.Add( new Job() { Id = id, Name = name, Description = description, LastExecutionDate = lastExecutionDate, PeriodFormat = periodFormat,
                                NextExecutionDate = nextExecutionDate, Period = period, UserId = userId, UserEmail = userEmail, Params = param, ApiUrlForJob = api});
                            }
                        }
                    }
                }
            });
            return jobs;
        }
        public async Task<bool> RemoveJob(string id)
        {
            int number = 0;
            await Task.Run(() =>
            {
                string sqlExpression = $"DELETE  FROM Job WHERE Id= @JobId";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@JobId", id);
                    number = command.ExecuteNonQuery();
                }
            });
            if(number > 0)
            return true;
            return false;

        }

        public async Task<bool> UpdateJob(Job job)
        {
            int number = 0;
            await Task.Run(() =>
            {
                string sqlExpression = $"UPDATE Job SET NextExecutionDate = @NextExecutionDate," +
                                       $"LastExecutionDate = @LastExecutionDate WHERE Id = @JobId";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@JobId", job.Id);
                    command.Parameters.AddWithValue("@LastExecutionDate", job.LastExecutionDate);
                    command.Parameters.AddWithValue("@NextExecutionDate", job.NextExecutionDate);
                    number = command.ExecuteNonQuery();
                }
            });
            if(number > 0)
            return true;
            return false;
        }

        public async Task<bool> UpdateJob(JobForListDto job)
        {
            int number = 0;
            await Task.Run(() =>
            {
                string sqlExpression = $"UPDATE Job SET Name= @JobName, Description= @JobDescription, NextExecutionDate = @NextExecutionDate," +
                                       $"Period = @Period, ApiUrlForJob = @ApiUrlForJob, Params = @Params, PeriodFormat = @PeriodFormat WHERE Id = @JobId";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@JobId", job.Id);
                    command.Parameters.AddWithValue("@JobName", job.Name);
                    command.Parameters.AddWithValue("@JobDescription", job.Description);
                    command.Parameters.AddWithValue("@NextExecutionDate", job.NextExecutionDate);
                    command.Parameters.AddWithValue("@Period", job.Period);
                    command.Parameters.AddWithValue("@ApiUrlForJob", job.ApiUrlForJob);
                    command.Parameters.AddWithValue("@Params", job.Params);
                    command.Parameters.AddWithValue("@PeriodFormat", job.PeriodFormat);
                    number = command.ExecuteNonQuery();
                }
            });
            if (number > 0)
                return true;
            return false;
        }
    }
}

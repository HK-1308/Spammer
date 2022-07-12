using Microsoft.Data.Sqlite;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Interfaces;
using TestTask.Data.Models;

namespace TestTask.Data.Repositories
{
    public class AdminStatisticRepository : IAdminStatisticRepository
    {
        public async Task<List<UserForAdminTableDto>> GetUsersForAdmin()
        {

            List<UserForAdminTableDto> users = new List<UserForAdminTableDto>();
            await Task.Run(() =>
            {
                string sqlExpression = "SELECT Email" +
                    $",count(Job.Id)" +
                    $",max(Job.LastExecutionDate)" +
                    $",min(Job.NextExecutionDate)" +
                $" FROM User" +
                $" LEFT JOIN Job ON Job.UserId == User.Id" +
                $" GROUP BY Email";
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
                                var email = reader.GetString(0);
                                var numberOftask = reader.GetInt32(1);
                                DateTime? lastExecutionDate;
                                if (reader[2].GetType() != typeof(DBNull))
                                {
                                    lastExecutionDate = DateTime.Parse(reader[2].ToString());
                                }
                                else lastExecutionDate = null;
                                DateTime? nextExecutionDate;
                                if (reader[3].GetType() != typeof(DBNull))
                                {
                                    nextExecutionDate = DateTime.Parse(reader[3].ToString());
                                }
                                else nextExecutionDate = null;

                                users.Add(new UserForAdminTableDto()
                                {
                                    Email = email,
                                    NumberOfTasks = numberOftask,
                                    LastExecutionDate = lastExecutionDate,
                                    NextExecutionDate = nextExecutionDate
                                });
                            }
                        }
                    }
                }
            });

            return users;
        }

        public async Task<bool> AddRecord(Job job, string status)
        {
            int number = 0;
            await Task.Run(() =>
            {
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    string id = Guid.NewGuid().ToString();
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO AdminStatistic (UserEmail, JobName, ApiUrlForJob, Status, LastExecution)" +
                    $" VALUES ('{job.UserEmail}','{job.Name}', '{job.ApiUrlForJob}', '{status}', '{job.LastExecutionDate}')";
                    number = command.ExecuteNonQuery();

                }
            }
            );
            if (number > 0) return true;
            return false;
        }

        public async Task<List<UserForUsageHistoryDto>> GetUserHistory(string userEmail)
        {

            List<UserForUsageHistoryDto> users = new List<UserForUsageHistoryDto>();
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM AdminStatistic WHERE UserEmail = '{userEmail}'";
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
                                var email = reader.GetString(1);
                                var jobName = reader.GetString(2);
                                var api = reader.GetString(3);
                                var status = reader.GetString(4);
                                DateTime? lastExecutionDate;
                                if (reader[5].GetType() != typeof(DBNull))
                                {
                                    lastExecutionDate = DateTime.Parse(reader[5].ToString());
                                }
                                else lastExecutionDate = null;
                                users.Add(new UserForUsageHistoryDto()
                                {
                                    Email = email,
                                    JobName = jobName,
                                    ApiUrlForJob = api,
                                    Status = status,
                                    LastExecution = lastExecutionDate
                                });
                            }
                        }
                    }
                }
            });
            users.Reverse();
            return users;
        }

        public async Task<List<UserForUsageHistoryDto>> GetHistory()
        {

            List<UserForUsageHistoryDto> users = new List<UserForUsageHistoryDto>();
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM AdminStatistic";
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
                                var email = reader.GetString(1);
                                var jobName = reader.GetString(2);
                                var api = reader.GetString(3);
                                var status = reader.GetString(4);
                                DateTime? lastExecutionDate;
                                if (reader[5].GetType() != typeof(DBNull))
                                {
                                    lastExecutionDate = DateTime.Parse(reader[5].ToString());
                                }
                                else lastExecutionDate = null;
                                users.Add(new UserForUsageHistoryDto()
                                {
                                    Email = email,
                                    JobName = jobName,
                                    ApiUrlForJob = api,
                                    Status = status,
                                    LastExecution = lastExecutionDate
                                });
                            }
                        }
                    }
                }
            });
            users.Reverse();
            return users;
        }
    }
}

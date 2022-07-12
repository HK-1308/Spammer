using TestTask.Data.Interfaces;
using TestTask.Data.DataTransferObject;
using Microsoft.Data.Sqlite;
using TestTask.Data.Models;

namespace TestTask.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        public UserRepository()
        {

        }

        public async Task<List<User>> AllUsers()
        {
            List<User> users = new List<User>();
            await Task.Run(() =>
            {
                string sqlExpression = "SELECT * FROM User";
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
                                var email = reader.GetString(1);
                                var password = reader.GetString(2);
                                users.Add(new User() { Email = email, Password = password, Id = id });
                            }
                        }
                    }
                }
            });

            return users;
        }

        public async Task<bool> CreateUser(User user)
        {
            bool isAlreadyExist = false;
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM User WHERE Email = '{user.Email}'";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            isAlreadyExist = true;
                        }
                    }
                }
            });
            if (isAlreadyExist) return false;

            int number = 0;
            await Task.Run(() =>
            {
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    string id = Guid.NewGuid().ToString();
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO User (Id,Email, Password, RoleId) VALUES ('{id}','{user.Email}', '{user.Password}', '2')";
                    number = command.ExecuteNonQuery(); 
                    
                }
            }
            );
            if (number > 0) return true;
            return false;
        }

        public async Task<string> GetUserIdByEmail(string email)
        {
            string userId = "";
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT Id FROM User WHERE Email = '{email}'";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                userId = reader.GetString(0);
                            }
                        }
                    }
                }
            });
            return userId;
        }

        public async Task<string> GetUserRoleId(string id)
        {
            string roleId = "";
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT RoleID FROM User WHERE Id = '{id}'";
                using (var connection = new SqliteConnection("Data Source=Tasks.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                 roleId = reader.GetString(0); 
                            }
                        }
                    }
                }
            });
            return roleId;
        }
        public async Task<User> GetUserForLogin(string? email, string? password)
        {
            User user = null;
            await Task.Run(() =>
            {
                string sqlExpression = $"SELECT * FROM User WHERE Email = '{email}' AND Password = '{password}'";
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
                                var email = reader.GetString(1);
                                var password = reader.GetString(2);
                                user = new User() { Email = email, Password = password, Id = id };
                            }
                        }
                    }
                }
            });
            return user;
        }


    }
}

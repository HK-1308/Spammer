namespace TestTask.Data.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User()
        {
            Jobs = new List<Job>();
        }
        public ICollection<Job> Jobs { get; set; }
    }
}

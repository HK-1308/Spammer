using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TestTask.Data.Models;

namespace TestTask.Models
{
    public class User: IdentityUser
    {
        public string? FirstName { get; set; }


        public User()
        {
            Jobs = new List<Job>();
        }
        public ICollection<Job> Jobs { get; set; }
    }
}

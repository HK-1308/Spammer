using System.ComponentModel.DataAnnotations;
using TestTask.Models;

namespace TestTask.Data.Models
{
    public class Job
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? LastExecutionDate { get; set; }

        public DateTime? NextExecutionDate { get; set; }

        public int Period { get; set; } 

        public string ApiUrlForJob { get; set; }

        public string? Params { get; set; }

        public virtual User? User { get; set; }

        public string? UserId { get; set; }

        public string UserEmail { get; set; }
    }
}

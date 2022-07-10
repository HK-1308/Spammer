using System.ComponentModel.DataAnnotations;

namespace TestTask.Data.DataTransferObject
{
    public class UserForAdminTableDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int? NumberOfTasks { get; set; }

        public DateTime? LastExecutionDate { get; set; }

        public DateTime? NextExecutionDate { get; set; }
    }
}

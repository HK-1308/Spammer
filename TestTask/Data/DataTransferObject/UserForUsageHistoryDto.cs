using System.ComponentModel.DataAnnotations;

namespace TestTask.Data.DataTransferObject
{
    public class UserForUsageHistoryDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string JobName { get; set; }

        [Required]
        public string ApiUrlForJob { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime? LastExecution { get; set; }
    }
}

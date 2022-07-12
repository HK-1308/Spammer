using System.ComponentModel.DataAnnotations;

namespace TestTask.Data.DataTransferObject
{
    public class JobForListDto
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Period { get; set; }

        [Required]
        public string PeriodFormat { get; set; }

        public DateTime? LastExecutionDate { get; set; }

        [Required]
        public DateTime NextExecutionDate { get; set; }

        [Required]
        public string ApiUrlForJob { get; set; }

        [Required]
        public string Params { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TestTask.Data.DataTransferObject
{
    public class JobDto
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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string ApiUrlForJob { get; set; }

        [Required]
        public string Params { get; set; }

    }
}

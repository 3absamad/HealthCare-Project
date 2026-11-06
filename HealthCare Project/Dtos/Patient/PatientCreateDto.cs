using System.ComponentModel.DataAnnotations;

namespace HealthCare_Project.Dtos.Patient
{
    public class PatientCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}

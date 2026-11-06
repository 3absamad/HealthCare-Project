using System.ComponentModel.DataAnnotations;

namespace HealthCare_Project.Dtos.Patient
{
    public class PatientUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}

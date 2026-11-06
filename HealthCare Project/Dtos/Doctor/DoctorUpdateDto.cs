using System.ComponentModel.DataAnnotations;

namespace HealthCare_Project.Dtos.Doctor
{
    public class DoctorUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; } = string.Empty;
    }
}

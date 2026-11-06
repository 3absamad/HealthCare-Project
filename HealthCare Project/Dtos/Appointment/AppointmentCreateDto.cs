using System.ComponentModel.DataAnnotations;
namespace HealthCare_Project.Dtos.Appointment
{
    public class AppointmentCreateDto
    {
        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }

        [Range(1, int.MaxValue)]
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
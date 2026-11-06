namespace HealthCare_Project.Dtos.Appointment
{
    public class AppointmentReadDto
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}

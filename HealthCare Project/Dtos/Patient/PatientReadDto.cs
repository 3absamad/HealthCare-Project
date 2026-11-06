namespace HealthCare_Project.Dtos.Patient
{
    public class PatientReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}

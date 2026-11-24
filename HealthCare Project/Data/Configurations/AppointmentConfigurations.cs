using HealthCare_Project.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCare_Project.Data.Configurations
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => new { a.DoctorId, a.PatientId });

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId);
        }
    }
}

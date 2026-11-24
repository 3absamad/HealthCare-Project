using HealthCare_Project.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCare_Project.Data.Configurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
